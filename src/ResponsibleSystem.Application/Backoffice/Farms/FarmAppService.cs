using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Backoffice.Farms.Dto;
using ResponsibleSystem.Entities;

namespace ResponsibleSystem.Backoffice.Farms
{
    [AbpAuthorize(PermissionNames.Pages_Farms)]
    public class FarmAppService: AsyncCrudAppService<Farm, FarmDto, long, PagedResultRequestDto, CreateFarmDto, FarmDto>
    {
        public FarmAppService(IRepository<Farm, long> repository) : base(repository)
        {}
    }
}
