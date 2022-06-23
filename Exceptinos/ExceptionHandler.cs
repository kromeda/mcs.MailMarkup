using MailMarkup.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MailMarkup.Exceptinos
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static object logSyncRoot = new object();
        private readonly JsonSerializerSettings jsonSettings;

        public ExceptionHandler()
        {
            jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() }
            };
        }

        public void Handle(Exception ex) => ParseException(ex);

        private void ParseException(Exception ex, ExceptionRecord parentRecord = null)
        {
            ExceptionRecord record = new ExceptionRecord(ex.Message, AdditionInfo(ex));

            if (parentRecord != null)
            {
                parentRecord.InnerExceptions.Add(record);
            }

            if (ex.InnerException != null)
            {
                ParseException(ex.InnerException, record);
            }

            if (ex is AggregateException aggregate)
            {
                foreach (Exception exception in aggregate.InnerExceptions)
                {
                    if (exception != null)
                    {
                        ParseException(exception, record);
                    }
                }
            }

            if (parentRecord == null)
            {
                SaveLogInFile(record);
            }
        }

        private void SaveLogInFile(ExceptionRecord record)
        {
            lock (logSyncRoot)
            {
                string logsDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs");
                string logs = Path.Combine(logsDirectory, DateTime.Now.ToString("d") + ".json");
                Directory.CreateDirectory(logsDirectory);

                List<ExceptionRecord> exceptionRecords = ReadExistingRecords(logs);
                exceptionRecords.Add(record);

                string content = JsonConvert.SerializeObject(exceptionRecords, jsonSettings);

                File.WriteAllTextAsync(logs, content);
            }
        }

        private static List<ExceptionRecord> ReadExistingRecords(string logFilePath)
        {
            if (!File.Exists(logFilePath))
            {
                return new List<ExceptionRecord>();
            }
            else
            {
                string content = File.ReadAllText(logFilePath);
                List<ExceptionRecord> exceptionRecords = JsonConvert.DeserializeObject<List<ExceptionRecord>>(content);
                return exceptionRecords;
            }
        }

        private static string AdditionInfo(Exception ex)
        {
            StringBuilder additinalBuilder = new StringBuilder();

            additinalBuilder.Append("[Тип исключения]: ");
            additinalBuilder.Append(ex.GetType().Name);

            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                additinalBuilder.Append(" [Трассировка стека]: ");
                additinalBuilder.Append(ex.StackTrace);
            }

            return additinalBuilder.ToString();
        }
    }
}
