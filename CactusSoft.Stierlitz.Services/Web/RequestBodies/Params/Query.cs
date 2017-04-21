using System.Collections.Generic;

namespace CactusSoft.Stierlitz.Services.Web.RequestBodies.Params
{
    public class Query
    {
        public QueryValues Value
        {
            get;
            set;
        }

        public List<string> Params
        {
            get;
            set;
        }

        public static Query None
        {
            get { return new Query {Value = QueryValues.None}; }
        }
        
        public static Query Shorten
        {
            get { return new Query {Value = QueryValues.Shorten}; }
        }
        
        public static Query Refer
        {
            get { return new Query {Value = QueryValues.Refer}; }
        }
        
        public static Query Extend
        {
            get { return new Query {Value = QueryValues.Extend}; }
        }
        
        public static Query Count
        {
            get { return new Query {Value = QueryValues.Count}; }
        }
    }

    public enum QueryValues
    {
        None,
        Shorten,
        Refer,
        Extend,
        Count,
    }
}
