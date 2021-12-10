using SIS.HTTP.Attributes;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Authorization;
using SIS.WebServer.DependencyInversion;
using SIS.WebServer.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SIS.WebServer.Controllers
{
    public static class ControllersManager
    {
        private static readonly string BaseControllerName = nameof(Controller);
        private static readonly Type BaseControllerType = typeof(Controller);
        private static readonly Type BaseResponceType = typeof(IHttpResponse);
        private static readonly Type BaseRequestMethodAttributeType = typeof(HttpRequestMethodAttribute);
        private static readonly Type BaseAccessAttributeType = typeof(AccessAthribute);
        private static readonly BindingFlags MethodCriteria = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly;

        public static IServerRoutingTable LoadControllers(this IServerRoutingTable serverRoutingTable, Assembly caller, IServiceCollection serviceCollection)
        {
            var controllersTypes = caller
                .GetExportedTypes()
                .Where(x => x.BaseType == BaseControllerType)
                .ToList();

            foreach (var controllerType in controllersTypes)
            {
                ProcessController(serverRoutingTable, controllerType, serviceCollection);
            }

            return serverRoutingTable;
        }

        private static void ProcessController(IServerRoutingTable serverRoutingTable, Type controller, IServiceCollection serviceCollection)
        {
            string controllerName = controller.Name.Replace(BaseControllerName, string.Empty);

            var controllerActions = controller
                .GetMethods(MethodCriteria)
                .Where(x => x.ReturnType == BaseResponceType)
                .ToList();

            foreach (var actionInfo in controllerActions)
            {
                var controllerInstanse = serviceCollection.CreateInstance(controller) as Controller;

                AddAction(serverRoutingTable, controllerName, actionInfo, controllerInstanse);
            }
        }

        private static void AddAction(IServerRoutingTable serverRoutingTable, string controllerName, MethodInfo actionInfo, Controller controllerInstanse)
        {
            var (requestMethod, url) = GetActionRequestMethod(actionInfo);

            string path = url ?? $"/{controllerName}/{actionInfo.Name}";

            serverRoutingTable.Add(requestMethod, path, (request) => ProcessAction(request, actionInfo, controllerInstanse));
        }

        private static IHttpResponse ProcessAction(IHttpRequest request, MethodInfo actionInfo, Controller controllerInstanse)
        {
            controllerInstanse
                .GetType()
                .GetProperty(nameof(Controller.Request))
                .SetValue(controllerInstanse, request);

            var parameters = actionInfo.GetParameters();
            List<object> arguments = new List<object>();

            foreach (var parameter in parameters)
            {
                var parameterType = parameter.ParameterType;
                object parameterValue = null;

                if (!parameterType.IsClass || parameterType == typeof(string))
                {
                    object value = GetValue(request, parameter.Name);

                    parameterValue = Convert.ChangeType(value, parameterType);
                }
                else 
                {
                    var properties = parameterType.GetProperties();
                    parameterValue = Activator.CreateInstance(parameterType);

                    foreach (var property in properties)
                    {
                        object value = GetValue(request, property.Name);

                        property.SetValue(parameterValue, Convert.ChangeType(value, property.PropertyType));
                    }
                }

                arguments.Add(parameterValue);
            }

            var accessAttribute = actionInfo.GetCustomAttribute(BaseAccessAttributeType, false);

            if (accessAttribute != null)
            {
                var accessAttributeType = accessAttribute.GetType();
                string redirectUrl = accessAttributeType
                    .GetProperty(nameof(AccessAthribute.RedirectUrl))
                    .GetValue(accessAttribute) as string;

                if (accessAttributeType == typeof(AuthorizeAttribute))
                {
                    if (!controllerInstanse.IsUserSignedIn())
                    {
                        return controllerInstanse.Redirect(redirectUrl);
                    }
                }
                else if(accessAttributeType == typeof(GuestOnlyAttribute))
                {
                    if (controllerInstanse.IsUserSignedIn())
                    {
                        return controllerInstanse.Redirect(redirectUrl);
                    }
                }
            }

            return actionInfo.Invoke(controllerInstanse, arguments.ToArray()) as IHttpResponse;
        }

        private static object GetValue(IHttpRequest request, string name)
        {
            object value = null;

            if (request.FormData.Any(x => string.Compare(x.Key, name, true) == 0))
            {
                value = request.FormData.First(x => string.Compare(x.Key, name, true) == 0).Value;
            }

            if (value == null)
            {
                if (request.QueryData.Any(x => string.Compare(x.Key, name, true) == 0))
                {
                    value = request.QueryData.First(x => string.Compare(x.Key, name, true) == 0).Value;
                }
            }

            return value;
        }

        private static (HttpRequestMethod requestMethod, string url) GetActionRequestMethod(MethodInfo actionInfo)
        {
            var requestMetodAttribute = actionInfo.GetCustomAttribute(BaseRequestMethodAttributeType, false);

            if (requestMetodAttribute != null)
            {
                var attributeType = requestMetodAttribute.GetType();

                HttpRequestMethod requestMethod = (HttpRequestMethod)attributeType
                    .GetProperty(nameof(HttpRequestMethodAttribute.RequestMethod))
                    .GetValue(requestMetodAttribute);

                string url = (string)attributeType
                    .GetProperty(nameof(HttpRequestMethodAttribute.Url))
                    .GetValue(requestMetodAttribute);

                return (requestMethod, url);
            }

            return (HttpRequestMethod.GET, null);
        }
    }
}