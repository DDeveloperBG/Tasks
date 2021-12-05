using SIS.HTTP.Enums;
using System;

namespace SIS.HTTP.Attributes
{
    public abstract class HttpRequestMethodAttribute : Attribute
    {
        public abstract HttpRequestMethod RequestMethod { get; }
        public string Url { get; set; }
    }
}