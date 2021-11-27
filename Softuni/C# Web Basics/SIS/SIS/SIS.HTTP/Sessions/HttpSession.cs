using System;
using System.Collections.Generic;
using SIS.HTTP.Common;
using System.Text;
using SIS.HTTP.Exceptions;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private readonly Dictionary<string, object> parameters;

        public HttpSession(string id)
        {
            Id = id;
            parameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            name.ThrowIfNullOrEmpty(nameof(name));
            parameter.ThrowIfNull(nameof(parameter));

            parameters.Add(name, parameter);
        }

        public void ClearParameters()
        {
            parameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            return parameters.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            if (!parameters.ContainsKey(name))
            {
                throw new BadRequestException();
            }

            return parameters[name];
        }
    }
}
