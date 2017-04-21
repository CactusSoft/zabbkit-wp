using System;
using System.Reflection;
using CactusSoft.Stierlitz.Common;

namespace CactusSoft.Stierlitz.Application
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string ApplicationVersion 
        {
            get { return Assembly.GetExecutingAssembly().GetVersion(); }
        }

        public string SupportMail
        {
            get { return "Support <maxim.bozbey@cactussoft.biz>; Support <andrey.pruchkovsky@cactussoft.biz>"; }
        }

        public int ReminderRecurrencePerUsageCount
        {
            get
            {
                return 10;
            }
        }

        public string FlurryApiKey
        {
            get
            {
                return "BZZ34X6QPJHFFS7XJGJX";
            }
        }
    }
}
