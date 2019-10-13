using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;
using ResponsibleSystem.Configuration;

namespace ResponsibleSystem.Localization
{
    public static class ResponsibleSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AppConfig.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ResponsibleSystemLocalizationConfigurer).GetAssembly(),
                        "ResponsibleSystem.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
