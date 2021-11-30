using SIS.HTTP.Enums;
using System;

namespace SIS.HTTP.Attributes
{
    public class HttpRequestMethodAttribute : Attribute
    {
        public HttpRequestMethod RequestMethod { get; set; }
    }
}