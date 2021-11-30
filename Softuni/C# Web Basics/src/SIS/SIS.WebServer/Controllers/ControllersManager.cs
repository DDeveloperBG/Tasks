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
    public class ControllersManager
    {
        private readonly string BaseControllerName = nameof(Controller);
        private readonly Type BaseControllerType = typeof(Controller);
        private readonly Type BaseResponceType = typeof(IHttpResponse);
        private readonly Type BaseRequestMethodAttributeType = typeof(HttpRequestMethodAttribute);
        private readonly Type ActionFuncType = typeof(Func<IHttpRequest, IHttpResponse>);
        private readonly BindingFlags MethodCriteria = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.DeclaredOnly;

        private readonly IServerRoutingTable serverRoutingTable;

        public ControllersManager()
        {
            this.serverRoutingTable = new ServerRoutingTable();
        }

        public IServerRoutingTable LoadControllers(Assembly caller)
        {
            var controllersTypes = caller
                .GetExportedTypes()
                .Where(x => x.BaseType == BaseControllerType)
                .ToList();

            foreach (var controllerType in controllersTypes)
            {
                ProcessController(controllerType);
            }

            return serverRoutingTable;
        }

        private void ProcessController(Type controller)
        {
            string controllerName = controller.Name.Replace(BaseControllerName, string.Empty);
            var controllerInstanse = Activator.CreateInstance(controller);

            var controllerActions = controller
                .GetMethods(MethodCriteria)
                .Where(x => x.ReturnType == BaseResponceType)
                .ToList();

            foreach (var actionInfo in controllerActions)
            {
                ProcessAction(controllerName, actionInfo, controllerInstanse);
            }
        }

        private void ProcessAction(string controllerName, MethodInfo actionInfo, object controllerInstanse)
        {
            var action = (Func<IHttpRequest, IHttpResponse>)actionInfo.CreateDelegate(ActionFuncType, controllerInstanse);

            HttpRequestMethod requestMethod = GetActionRequestMethod(actionInfo);

            string path = GeneratePath(controllerName, actionInfo.Name);

            serverRoutingTable.Add(requestMethod, path, action);
        }

        private HttpRequestMethod GetActionRequestMethod(MethodInfo actionInfo)
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

        //TODO: Upgrade path generation logic
        private string GeneratePath(string controllerName, string methodName)
        {
            if (methodName == "Index")
            {
                if (controllerName == "Home")
                {
                    return "/";
                }

                methodName = string.Empty;
            }

            return $"/{controllerName}/{methodName}";
        }
    }
}