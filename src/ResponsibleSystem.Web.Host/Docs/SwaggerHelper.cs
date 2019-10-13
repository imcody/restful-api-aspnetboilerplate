using Abp.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using ResponsibleSystem.Web.Host.Api;

namespace ResponsibleSystem.Web.Host.Docs
{
    internal class SwaggerHelper
    {
        internal static void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            AddSwaggerDocPerVersion(options);
            ApplyDocInclusions(options);
            AddSecurityDefinition(options);

            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var xmlFile in xmlFiles)
                options.IncludeXmlComments(xmlFile);

            options.DescribeAllEnumsAsStrings();
            options.OperationFilter<FormatXmlCommentProperties>();
        }

        internal static Action<SwaggerUIOptions> ConfigureSwaggerUI(string baseUrl)
        {
            return (options) =>
            {
                foreach (var apiVersion in AppPublicApis.All)
                {
                    options.SwaggerEndpoint(baseUrl.EnsureEndsWith('/') + $"swagger/{apiVersion.Info.Version}/swagger.json", apiVersion.Info.Title);
                }
                //options.IndexStream = () => Assembly.GetExecutingAssembly()
                //    .GetManifestResourceStream($"{AppPublicApis.AppNamespaceRoot}.Web.Host.wwwroot.swagger.ui.index.html");
            }; // URL: /swagger
        }

        private static void AddSecurityDefinition(SwaggerGenOptions options)
        {
            // Define the BearerAuth scheme that's in use
            options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = "header",
                Type = "apiKey"
            });
        }

        private static void AddSwaggerDocPerVersion(SwaggerGenOptions swaggerGenOptions)
        {
            foreach (var apiVersion in AppPublicApis.All)
            {
                swaggerGenOptions.SwaggerDoc(apiVersion.Info.Version, apiVersion.Info);
            }
        }

        private static void ApplyDocInclusions(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.DocInclusionPredicate((appApiVersion, apiDesc) =>
            {
                return appApiVersion.StartsWith(apiDesc.GroupName);
            });
        }
    }
}
