using System;
using CactusSoft.Stierlitz.Application.Converters;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Application.ViewModels
{
    public class DataItemViewModel
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string HostId { get; set; }
        public string HostName { get; set; }
        public string Value { get; set; }
        public int ValueType { get; set; }
        public string Units { get; set; }
        public string GroupId { get; set; }

        public string ValueFormatted
        {
            get
            {
                // bytes
                if (Units == "B" || Units == "bps")
                {
                    string[] sizes = { "B", "KB", "MB", "GB" };
                    string[] sizesBps = { "bps", "Kbps", "Mbps", "Gbps" };
                    var len = Convert.ToDouble(Value);
                    var order = 0;
                    while (len >= 1024 && order + 1 < sizes.Length)
                    {
                        order++;
                        len = len / 1024;
                    }

                    return string.Format("{0:0.##} {1}", len, 
                        Units == "B" ? sizes[order] : sizesBps[order]);
                }
                // unixtime
                if (Units == "unixtime")
                {
                    var seconds = Convert.ToInt64(Value);
                    var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    dateTime = dateTime.AddSeconds(seconds);
                    return dateTime.ToString();
                }
                if (Units == "uptime")
                {
                    var seconds = Convert.ToInt64(Value);
                    var tsConverter = new TimespanToDurationConverter();
                    var ts = TimeSpan.FromSeconds(seconds);
                    return tsConverter.Convert(ts, null, null, null).ToString();
                }
                return string.Format("{0} {1}", Value, Units);
            }
        }

        public DataItemViewModel(Item item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
            HostId = item.HostId;
            Value = item.Value;
            ValueType = item.ValueType;
            Units = item.Units;
            GroupId = item.GroupId;
        }

    }
}
