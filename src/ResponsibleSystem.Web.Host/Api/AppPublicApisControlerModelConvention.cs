using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace ResponsibleSystem.Web.Host.Api
{
    public class AppPublicApisControlerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.ApiExplorer.GroupName = GetControllerApiGroupName(controller);
        }

        private string GetControllerApiGroupName(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            foreach (ApiInfo api in AppPublicApis.All)
            {
                if (api.IncludeNamespaces.Any(ns => controllerNamespace.StartsWith(ns)))
                    return api.Info.Version;
            }

            if (AppPublicApis.SharedNamespaces.Any(ns => controllerNamespace.StartsWith(ns)))
                return "v";

            return "blocked-controller"; // not registered to any particular API or shared area;
        }
    }
}
