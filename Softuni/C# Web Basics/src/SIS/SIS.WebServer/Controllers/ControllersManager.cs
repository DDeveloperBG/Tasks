using SIS.HTTP.Attributes;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Routing;
using System;
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
        private static readonly Type ActionFuncType = typeof(Func<IHttpRequest, IHttpResponse>);
        private static readonly BindingFlags MethodCriteria = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly;

        public static IServerRoutingTable LoadControllers(this IServerRoutingTable serverRoutingTable, Assembly caller)
        {
            var controllersTypes = caller
                .GetExportedTypes()
                .Where(x => x.BaseType == BaseControllerType)
                .ToList();

            foreach (var controllerType in controllersTypes)
            {
                ProcessController(serverRoutingTable, controllerType);
            }

            return serverRoutingTable;
        }

        private static void ProcessController(IServerRoutingTable serverRoutingTable, Type controller)
        {
            string controllerName = controller.Name.Replace(BaseControllerName, string.Empty);
            var controllerInstanse = Activator.CreateInstance(controller);

            var controllerActions = controller
                .GetMethods(MethodCriteria)
                .Where(x => x.ReturnType == BaseResponceType)
                .ToList();

            foreach (var actionInfo in controllerActions)
            {
                ProcessAction(serverRoutingTable, controllerName, actionInfo, controllerInstanse);
            }
        }

        private static void ProcessAction(IServerRoutingTable serverRoutingTable, string controllerName, MethodInfo actionInfo, object controllerInstanse)
        {
            var action = (Func<IHttpRequest, IHttpResponse>)actionInfo.CreateDelegate(ActionFuncType, controllerInstanse);

            HttpRequestMethod requestMethod = GetActionRequestMethod(actionInfo);

            string path = GeneratePath(controllerName, actionInfo.Name);

            serverRoutingTable.Add(requestMethod, path, action);
        }

        private static HttpRequestMethod GetActionRequestMethod(MethodInfo actionInfo)
        {
            var requestMetodAttribute = actionInfo.GetCustomAttribute(BaseRequestMethodAttributeType, false);

            if (requestMetodAttribute != null)
            {
                return (HttpRequestMethod)requestMetodAttribute
                    .GetType()
                    .GetProperty(nameof(HttpRequestMethodAttribute.RequestMethod))
                    .GetValue(requestMetodAttribute);
            }

            return HttpRequestMethod.GET;
        }

        private static string GeneratePath(string controllerName, string methodName)
        {
            if (methodName == "Index")
            {
                if (controllerName == "Home")
                {
                    return "/";
                }

                return $"/{controllerName}";
            }

            return $"/{controllerName}/{methodName}";
        }
    }
}