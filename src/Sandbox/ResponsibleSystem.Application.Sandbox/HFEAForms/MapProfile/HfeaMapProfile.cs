using AutoMapper;
using ResponsibleSystem.Common.Extensions;
using ResponsibleSystem.Sandbox.HFEAForms.Domain;
using ResponsibleSystem.Sandbox.HFEAForms.Dto;

namespace ResponsibleSystem.Sandbox.HFEAForms.MapProfile
{
    public class HfeaMapProfile : Profile
    {
        public HfeaMapProfile()
        {
            CreateMap<HfeaForm, HfeaFormDto>()
                .AfterMap((src, dest) =>
                {
                    dest.CreateDateString = src.CreateDate.ToAppDateTimeString();
                });

            CreateMap<HfeaForm, HfeaFormReadOnlyDto>()
                .AfterMap((src, dest) =>
                {
                    dest.CreateDateString = src.CreateDate.ToAppDateTimeString();
                    dest.PublisherName = src.Publisher?.ToString();
                });

            CreateMap<HfeaClassification, HfeaClassificationDto>()
                .ForMember(dest => dest.FormCategory, expression => expression.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.FormCategory = src.FormCategory == null ? "" : src.FormCategory.GetDescriptionFromValue();
                });

            CreateMap<HfeaClassification, HfeaClassificationReadOnlyDto>()
                .ForMember(dest => dest.MaritalStatus, expression => expression.Ignore())
                .ForMember(dest => dest.FormCategory, expression => expression.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.FertilityTreatment = src.FertilityTreatmentType?.ToString();
                    dest.FormCategory = src.FormCategory == null ? "" : src.FormCategory.GetDescriptionFromValue();
                    dest.MaritalStatus = src.MaritalStatus?.ToString();
                });
        }
    }
}
