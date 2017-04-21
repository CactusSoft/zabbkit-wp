namespace CactusSoft.Stierlitz.Common
{
    public interface ILogService
    {
        bool LogsExists
        {
            get;
        }
        void Add(string mesage);
        void Add(string mesageFormat, params object[] args);
        void Save();
        void DeleteAll();
        string GetLastReport();
        string LogFileName { set; }
    }
}