using Abp.Authorization;
using Abp.AutoMapper;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Sandbox.HFEAForms.Domain;
using ResponsibleSystem.Sandbox.HFEAForms.Dto;
using ResponsibleSystem.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponsibleSystem.Common.CosmosDb.Repositories;

namespace ResponsibleSystem.Sandbox.HFEAForms
{
    [AbpAuthorize(PermissionNames.Pages_HfeaForms)]
    public class HfeaFormAppService : ResponsibleSystemAppServiceBase, IHfeaFormAppService
    {
        private readonly ICosmosDbRepository<HfeaForm> _hfeaRepository;

        public HfeaFormAppService(ICosmosDbRepository<HfeaForm> hfeaRepository)
        {
            _hfeaRepository = hfeaRepository;
        }

        public async Task<HfeaFormDto> Get(string id)
        {
            var hfeaForm = await _hfeaRepository.GetItemAsync(id);
            if (hfeaForm == null)
                throw new ResponsibleSystemUserFriendlyException($"Hfea with id = {id} not found", "Could not found the HFEA form, maybe it's deleted.");

            var result = hfeaForm.MapTo<HfeaFormDto>();
            return result;
        }

        public async Task<HfeaFormReadOnlyDto> GetReadOnly(string id)
        {
            var hfeaForm = await _hfeaRepository.GetItemAsync(id);
            if (hfeaForm == null)
                throw new ResponsibleSystemUserFriendlyException($"Hfea with id = {id} not found", "Could not found the HFEA form, maybe it's deleted.");

            var result = hfeaForm.MapTo<HfeaFormReadOnlyDto>();
            return result;
        }

        public async Task<IList<HfeaFormReadOnlyDto>> GetAll()
        {
            var hfeaForms = await _hfeaRepository
                .GetItemsAsync<HfeaFormReadOnlyDto>("c.IsActive");

            var result = hfeaForms.OrderByDescending(m => m.Name)
                .ToList()
                .MapTo<List<HfeaFormReadOnlyDto>>();

            return result;
        }

        public async Task<HfeaForm> Create(CreateHfeaFormInput input)
        {
            var publisher = await SessionService.GetPublisherAsync();
            var hfeaClassification = input.HfeaClassification.MapTo<HfeaClassification>();

            var hfeaForm = HfeaForm.Create(
                name: input.Name,
                versionNumber: input.VersionNumber,
                formCode: input.FormCode,
                shortDescription: input.ShortDescription,
                publisher: publisher,
                hfeaFormFile: input.FormFile,
                hfeaClassification: hfeaClassification,
                hfeaDescriptionVideo: input.DescriptionVideo,
                priority: input.Priority,
                useDefaultPriority: input.UseDefaultPriority);

            await _hfeaRepository.CreateItemAsync(hfeaForm);

            return hfeaForm;
        }

        public async Task<HfeaForm> Edit(UpdateHfeaFormInput input)
        {
            var originHfeaForm = await _hfeaRepository.GetItemAsync(input.ParentId);
            var publisher = await SessionService.GetPublisherAsync();

            // archive old hfea form

            originHfeaForm.IsActive = false;
            await _hfeaRepository.UpdateItemAsync(input.ParentId, originHfeaForm);

            // Create new version

            var hfeaClassification = input.HfeaClassification.MapTo<HfeaClassification>();
            var hfeaForm = HfeaForm.Create(
                    name: input.Name,
                    versionNumber: input.VersionNumber,
                    shortDescription: input.ShortDescription,
                    publisher: publisher,
                    formCode: input.FormCode,
                    rootId: originHfeaForm.RootId,
                    hfeaFormFile: input.FormFile,
                    hfeaClassification: hfeaClassification,
                    hfeaDescriptionVideo: input.DescriptionVideo,
                    priority: input.Priority,
                    useDefaultPriority: input.UseDefaultPriority);

            await _hfeaRepository.CreateItemAsync(hfeaForm);

            return hfeaForm;
        }

        public async Task Delete(string id)
        {
            var hfeaForm = await _hfeaRepository.GetItemAsync(id);
            if (hfeaForm == null)
                throw new ResponsibleSystemUserFriendlyException($"Hfea with id = {id} not found", "Could not found the HFEA form, maybe it's already deleted.");

            var rootId = hfeaForm.RootId;
            await _hfeaRepository.DeleteItemAsync(id);
        }
    }
}
