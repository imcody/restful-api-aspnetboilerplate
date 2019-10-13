using System;
using ResponsibleSystem.Common.CosmosDb.Domain;
using ResponsibleSystem.Shared.Dto;

namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public class HfeaForm : CosmosDbEntityBase
    {
        public const int DEFAULT_PRIORITY = 200;

        public string RootId { get; set; }
        public bool IsActive { get; set; }

        public UserDto Publisher { get; set; }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string VersionNumber { get; set; }
        public string FormCode { get; set; }

        public virtual string FormFile { get; set; }
        public virtual string DescriptionVideo { get; set; }
        public bool UseDefaultPriority { get; set; }
        public int Priority { get; set; }

        public HfeaClassification HfeaClassification { get; set; }

        public static HfeaForm Create(
            string name,
            string versionNumber,
            string formCode,
            string shortDescription,
            string hfeaFormFile,
            string hfeaDescriptionVideo,
            UserDto publisher,
            HfeaClassification hfeaClassification = null,
            string rootId = "",
            bool useDefaultPriority = true,
            int priority = 0
            )
        {
            var hfeaId = "";
            var idPrefix = "h-";

            if (string.IsNullOrWhiteSpace(rootId))
            {
                rootId = $"{idPrefix}{Guid.NewGuid()}";
                hfeaId = rootId;
            }
            else
            {
                hfeaId = $"{idPrefix}{Guid.NewGuid()}";
            }

            if (useDefaultPriority)
            {
                priority = DEFAULT_PRIORITY;
            }

            var hfeaform = new HfeaForm
            {
                RootId = rootId,
                Id = hfeaId,
                Name = name,
                ShortDescription = shortDescription,
                FormFile = hfeaFormFile,
                DescriptionVideo = hfeaDescriptionVideo,
                Publisher = publisher,
                IsActive = true,
                VersionNumber = versionNumber,
                CreateDate = DateTime.UtcNow,
                HfeaClassification = hfeaClassification,
                FormCode = formCode,
                UseDefaultPriority = useDefaultPriority,
                Priority = priority
            };

            return hfeaform;
        }
    }
}
