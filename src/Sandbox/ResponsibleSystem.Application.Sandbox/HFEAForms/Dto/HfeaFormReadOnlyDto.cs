using ResponsibleSystem.Common.CosmosDb.Domain;

namespace ResponsibleSystem.Sandbox.HFEAForms.Dto
{
    public class HfeaFormReadOnlyDto
    {
        [SqlSelectProperty]
        public string Id { get; set; }

        [SqlSelectProperty]
        public string RootId { get; set; }

        [SqlSelectProperty]
        public bool IsActive { get; set; }

        [SqlSelectProperty]
        public string Name { get; set; }

        [SqlSelectProperty]
        public string VersionNumber { get; set; }

        [SqlSelectProperty]
        public string FormCode { get; set; }

        [SqlSelectProperty]
        public string ValidFor { get; set; }

        [SqlSelectProperty]
        public string ShortDescription { get; set; }

        [SqlSelectProperty]
        public string FormFile { get; set; }

        [SqlSelectProperty]
        public string DescriptionVideo { get; set; }

        [SqlSelectProperty(Name = "CreateDate")]
        public string CreateDateString { get; set; }

        [SqlSelectProperty(Name = "Publisher")]
        public string PublisherName { get; set; }

        [SqlSelectProperty]
        public HfeaClassificationReadOnlyDto HfeaClassification { get; set; }
    }
}