using System.Collections.Generic;

namespace ResponsibleSystem.Web.Host.Api
{
    public class AppPublicApis
    {
        public static string AppNamespaceRoot = nameof(ResponsibleSystem);
        public static IList<ApiInfo> All { get; }
        public static IList<string> SharedNamespaces { get; }

        //sample app names
        const string App1Name = "Backoffice";
        const string App2Name = "App";

        static AppPublicApis()
        {
            // to add new swagger config add new items to collection
            All = new List<ApiInfo>
            {
                new ApiInfo(
                    title: $"{App1Name}API",
                    version: "1",
                    includeNamespaces: new List<string> {
                        $"{AppNamespaceRoot}.{App1Name}.",
                        $"{AppNamespaceRoot}.Controllers.{App1Name}",
                }),
                //new ApiInfo(
                //    title: $"{App2Name}API",
                //    version: "1",
                //    includeNamespaces: new List<string> {
                //        $"{AppNamespaceRoot}.{App2Name}.",
                //        $"{AppNamespaceRoot}.Controllers.{App2Name}",
                //}),
            };

            SharedNamespaces = new List<string>
            {
                $"{AppNamespaceRoot}"
                //$"{AppNamespaceRoot}.Shared",
                //$"{AppNamespaceRoot}.Controllers",
                //$"{AppNamespaceRoot}.Web.Core.Controllers.Shared",
                //$"{AppNamespaceRoot}.Web.Host.Controllers.Shared",
            };
        }
    }
}
