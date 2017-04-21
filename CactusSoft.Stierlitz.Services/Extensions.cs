using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CactusSoft.Stierlitz.Domain;
using CactusSoft.Stierlitz.Services.Web.ResponseBodies.Results;

namespace CactusSoft.Stierlitz.Services
{
    public static class Extensions
    {
        public static HostGroup ToHostGroup(this GetHostGroupsResult result)
        {
            if (result == null)
            {
                return null;
            }

            return new HostGroup { Id = result.Id, Name = result.Name };
        }

        public static Host ToHost(this GetHostsResult result)
        {
            if (result == null)
            {
                return null;
            }

            return new Host{Id = result.Id, Name = result.Name};
        }

        public static Trigger ToTrigger(this GetTriggersResult result)
        {
            if (result == null)
            {
                return null;
            }

            return new Trigger
                       {
                           Id = result.Id, 
                           Description = result.Description, 
                           IsOk = result.State == 0, 
                           Priority = (TriggerPriority) result.Priority,
                           Hosts = result.Hosts != null ? result.Hosts.Select(hostResult => hostResult.ToHost()).Where(h => h != null).ToList() : new List<Host>()
                       };
        }

        public static Event ToEvent(this GetEventsResult result)
        {
            if (result == null)
            {
                return null;
            }
            var newEvent = new Event
                               {
                                   Id = result.Id,
                                   Time = ((double) result.Timestamp).JsonUnixTicksToDateTime().ToLocalTime(),
                                   Hosts = result.Hosts != null ? result.Hosts.Select(hostResult => hostResult.ToHost()).Where(h => h != null).ToList() : new List<Host>(),
                                   Trigger = result.Triggers != null ? result.Triggers.Select(triggerResult => triggerResult.ToTrigger()).FirstOrDefault() : new Trigger()
                               };

            if (result.State != 2)
            {
                newEvent.IsOk = result.State == 0;
            }

            return newEvent;
        }

        public static Graph ToGraph(this GetGraphsResult result)
        {
            if (result == null)
                return null;

            return new Graph{GraphId = result.GraphId, Name = result.Name};
        }

        public static Item ToItem(this GetDataResult result)
        {
            if (result == null)
                return null;

            // resolve name
            var name = result.Name;
            if (!string.IsNullOrEmpty(result.Key))
            {
                var m = Regex.Match(result.Key,@"\[([\w\W]+?)\]");
                if (m.Success)
                {
                    var valuesString = m.Groups[1].Value;
                    if (!string.IsNullOrEmpty(valuesString))
                    {
                        var values = valuesString.Split(",".ToCharArray());
                        for (var i = 1; i <= values.Length; i++)
                            name = name.Replace("$" + i, values[i - 1]);
                    }
                }
            }

            // resolve value
            var formatProvider = new NumberFormatInfo { NumberDecimalSeparator = "." };
            var value = result.Value;
            if (!string.IsNullOrEmpty(result.Formula))
            {
                try
                {
                    if (result.ValueType == 0)
                    {
                        var formula = Convert.ToDouble(result.Formula, formatProvider);
                        var valueConverted = Convert.ToDouble(result.Value, formatProvider);
                        value = (formula * valueConverted).ToString(CultureInfo.InvariantCulture);
                    }
                    if (result.ValueType == 3)
                    {
                        var formula = Convert.ToDouble(result.Formula, formatProvider);
                        var valueConverted = Convert.ToInt64(result.Value);
                        value = (formula * valueConverted).ToString(CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error while converting values: {0}", ex);
                }
            }

            // remove 0 from float numbers
            if (result.ValueType == 0)
            {
                try
                {
                    var valueConverted = Convert.ToDouble(value, formatProvider);
                    value = valueConverted.ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error while converting values: {0}", ex);
                }
            }

            return new Item 
            { 
                ItemId = result.ItemId, 
                Name = name, 
                Value = value,
                ValueType = result.ValueType,
                Units = result.Units, 
                HostId = result.HostId
            };
        }

    }
}
