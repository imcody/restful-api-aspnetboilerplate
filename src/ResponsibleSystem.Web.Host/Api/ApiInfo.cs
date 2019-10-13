using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;

namespace ResponsibleSystem.Web.Host.Api
{
    public class ApiInfo
    {
        public string Title { get; }
        public string Version { get; }
        public IList<string> IncludeNamespaces { get; }
        public Info Info { get; }

        public ApiInfo(string title, string version, IList<string> includeNamespaces, Info info = null)
        {
            Title = title;
            Version = version;
            IncludeNamespaces = includeNamespaces;

            var mdPath = Path.Combine("Api", "Descriptions", $"{Title}.md");
            Info = info ?? new Info
            {
                Title = Title,
                Version = $"v{version}-{Title}",
                Description = File.Exists(mdPath) ? File.ReadAllText(mdPath) : string.Empty
            };
        }
    }
}
