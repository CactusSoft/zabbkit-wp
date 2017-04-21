using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using CactusSoft.Stierlitz.Common;

namespace CactusSoft.Stierlitz.Application.Services
{
    public class LogService : ILogService
    {
        private readonly object _sync = new object();
        private const string LOGS_FOLDER_NAME = "Logs";
        private const string FILE_NAME_FORMAT = "{0:yyyy_MM_dd_HH_mm_ss_ffff}_{1}";
        private const string MESSAGE_FORMAT = "{0:yyyy-MM-dd HH:mm:ss.ffff} - {1}";
        private const string LOG_FILE_EXTENSION = ".log";
        private readonly Queue<string> _messages = new Queue<string>();

        public LogService()
        {
            LogFileName = string.Empty;
        }

        public string LogFileName
        {
            private get;
            set;
        }

        public bool LogsExists
        {
            get
            {
                return IsLogExists();
            }
        }

        public void Add(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            lock (_sync)
            {
                _messages.Enqueue(string.Format(MESSAGE_FORMAT, DateTime.Now, message));
            }
        }

        public void Add(string mesageFormat, params object[] args)
        {
            if (mesageFormat == null)
            {
                throw new ArgumentNullException("mesageFormat");
            }

            Add(string.Format(mesageFormat, args));
        }

        public void Save()
        {
            lock(_sync)
            {
                if (_messages.Count == 0)
                {
                    return;
                }

                var logBuilder = new StringBuilder();

                while(_messages.Count > 0)
                {
                    logBuilder.AppendFormat("{0}\r\n", _messages.Dequeue());
                }

                SaveLogs(logBuilder.ToString());
            }
        }

        public void DeleteAll()
        {
            lock (_sync)
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    var files = GetFileList(store);
                    foreach (var file in files)
                    {
                        store.DeleteFile(GetFilePath(file));
                    }
                }
            }
        }

        public string GetLastReport()
        {
            lock (_sync)
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    var files = GetFileList(store);
                    var file = files.OrderByDescending(k => k).FirstOrDefault();
                    if (string.IsNullOrEmpty(file))
                    {
                        return string.Empty;
                    }

                    using (var fileStream = store.OpenFile(GetFilePath(file), FileMode.Open, FileAccess.Read))
                    {
                        using (var streamReader = new StreamReader(fileStream))
                        {
                            var fileContent = streamReader.ReadToEnd();
                            return fileContent;
                        }
                    }
                }
            }
        }

        private void SaveLogs(string logs)
        {
            var fileName = string.Format(FILE_NAME_FORMAT, DateTime.Now, string.Concat(LogFileName, LOG_FILE_EXTENSION));
            fileName = GetFilePath(fileName);

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (store.FileExists(fileName))
                {
                    store.DeleteFile(fileName);
                }

                if (!store.DirectoryExists(LOGS_FOLDER_NAME))
                {
                    store.CreateDirectory(LOGS_FOLDER_NAME);
                }

                using (var fileStream = store.OpenFile(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var buffer = Encoding.UTF8.GetBytes(logs);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private bool IsLogExists()
        {
            lock (_sync)
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!store.DirectoryExists(LOGS_FOLDER_NAME))
                    {
                        return false;
                    }

                    var files = GetFileList(store);
                    return files.Length != 0;
                }
            }
        }

        private string[] GetFileList(IsolatedStorageFile storageFile)
        {
            if (!storageFile.DirectoryExists(LOGS_FOLDER_NAME))
            {
                return new string[0];
            }

            var files = storageFile.GetFileNames(FilePattern());
            return files;
        }

        private string FilePattern()
        {
            return string.Format("{0}/*_{1}{2}", LOGS_FOLDER_NAME, LogFileName, LOG_FILE_EXTENSION);
        }

        private static string GetFilePath(string fileName)
        {
            return string.Format("{0}/{1}", LOGS_FOLDER_NAME, fileName);
        }
    }
}