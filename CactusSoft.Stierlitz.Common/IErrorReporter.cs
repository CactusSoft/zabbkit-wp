using System.Threading.Tasks;

namespace CactusSoft.Stierlitz.Common
{
    public interface IErrorReporter
    {
        void SendReport();
        bool HasReports();
        void WriteReport(string report);
        void ClearReports();
    }
}
