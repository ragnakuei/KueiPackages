using System;

namespace KueiPackages.AOP.Models
{
    public class AOPAttribute : Attribute
    {
        public string Before         { get; }
        public string After          { get; }
        public string Exception      { get; }
        public bool   IsLogParameter { get; }

        public AOPAttribute(string before         = "",
                            string after          = "",
                            string exception      = "",
                            bool   isLogParameter = false)
        {
            Before         = before;
            After          = after;
            Exception      = exception;
            IsLogParameter = isLogParameter;
        }
    }
}