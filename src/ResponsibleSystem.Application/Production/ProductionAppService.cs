using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Entities;
using ResponsibleSystem.Entities.Enums;
using ResponsibleSystem.Extensions;
using ResponsibleSystem.Production.Dto;
using ResponsibleSystem.Shared.Sessions.Services;

namespace ResponsibleSystem.Production
{
    [AbpAuthorize(PermissionNames.Pages_Production)]
    public class ProductionAppService : AsyncCrudAppService<Entities.Production, ProductionDto, long, PagedResultRequestDto, CreateProductionDto, ProductionDto>
    {
        protected override string CreatePermissionName => PermissionNames.Production_Admin;
        protected override string DeletePermissionName => PermissionNames.Production_Admin;

        private readonly ISessionService _sessionService;
        private readonly IRepository<Entities.Inventory, long> _inventoryRepository;

        public ProductionAppService(IRepository<Entities.Production, long> repository,
            IRepository<Entities.Inventory, long> inventoryRepository,
            ISessionService sessionService) : base(repository)
        {
            _inventoryRepository = inventoryRepository;
            _sessionService = sessionService;
        }

        protected override IQueryable<Entities.Production> CreateFilteredQuery(PagedResultRequestDto input)
        {
            var query = Repository.GetAll();

            var role = _sessionService.GetCurrentUserRole();
            var permissions = StaticRolePermissions.RolePermissions[role.Value];

            if (permissions.Contains(PermissionNames.Production_Admin))
            {
                query = query.Where(x => true);
            }
            else if (permissions.Contains(PermissionNames.Pages_Shoemaker_Step1))
            {
                query = query.Where(x => x.Status == ProductionChainStatus.Shoemaker_Step1);
            }
            else if (permissions.Contains(PermissionNames.Pages_Shoemaker_Step2))
            {
                query = query.Where(x => x.Status == ProductionChainStatus.Shoemaker_Step2);
            }
            else
            {
                throw new UserFriendlyException("No permissions to get production list");
            }
            return query;
        }

        protected override void CheckUpdatePermission()
        {
            if (!PermissionChecker.IsGranted(false, PermissionNames.Pages_Shoemaker_Step1, PermissionNames.Pages_Shoemaker_Step2))
            {
                throw new AbpAuthorizationException();
            }
        }

        public Task<ProductionDetailsDto> GetDetails(EntityDto<long> input)
        {
            return CreateFilteredQuery(null)
                .Where(x => x.Id == input.Id)
                .Select(x => new ProductionDetailsDto()
                {
                    UpperLeather = x.UpperLeather.LeatherId,
                    LiningLeather = x.LiningLeather.LeatherId,
                    BackCounterLeather = x.BackCounterLeather.LeatherId,
                    WeltLeather = x.WeltLeather.LeatherId,
                    SoleLeather = x.SoleLeather.LeatherId,
                    HeelLeather = x.HeelLeather.LeatherId,
                    InSockLeather = x.InSockLeather.LeatherId,
                    FillingLeather = x.FillingLeather.LeatherId,
                    ReinforcementLeather = x.ReinforcementLeather.LeatherId,
                    RemovableInSockLeather = x.RemovableInSockLeather.LeatherId,
                })
                .FirstOrDefaultAsync();
        }

        public override async Task<ProductionDto> Update(ProductionDto input)
        {
            var production = await CreateFilteredQuery(null).FirstOrDefaultAsync(x => x.Id == input.Id);
            if (production == null)
            {
                throw new UserFriendlyException("Invalid production");
            }

            var inputIds = new[]
            {
                input.BackCounterLeatherId,
                input.FillingLeatherId,
                input.HeelLeatherId,
                input.InSockLeatherId,
                input.LiningLeatherId,
                input.ReinforcementLeatherId,
                input.RemovableInSockLeatherId,
                input.SoleLeatherId,
                input.UpperLeatherId,
                input.WeltLeatherId
            }.OfType<long>();
            var inventoryIds = await GetInventoryQuery().Select(x => (long?)x.Id).Where(x => inputIds.Contains(x.Value)).ToListAsync();
            inventoryIds.Add(null);
            long? Validate(long? id, long? oldValue)
            {
                return inventoryIds.Contains(id) ? id : oldValue;
            }

            if (await IsGrantedAsync(PermissionNames.Pages_Shoemaker_Step1))
            {
                production.DepartureTime = input.DepartureTime;
            }
            production.BackCounterLeatherId = Validate(input.BackCounterLeatherId, production.BackCounterLeatherId);
            production.FillingLeatherId = Validate(input.FillingLeatherId, production.FillingLeatherId);
            production.HeelLeatherId = Validate(input.HeelLeatherId, production.HeelLeatherId);
            production.InSockLeatherId = Validate(input.InSockLeatherId, production.InSockLeatherId);
            production.LiningLeatherId = Validate(input.LiningLeatherId, production.LiningLeatherId);
            production.ReinforcementLeatherId = Validate(input.ReinforcementLeatherId, production.ReinforcementLeatherId);
            production.RemovableInSockLeatherId = Validate(input.RemovableInSockLeatherId, production.RemovableInSockLeatherId);
            production.SoleLeatherId = Validate(input.SoleLeatherId, production.SoleLeatherId);
            production.UpperLeatherId = Validate(input.UpperLeatherId, production.UpperLeatherId);
            production.WeltLeatherId = Validate(input.WeltLeatherId, production.WeltLeatherId);

            await CurrentUnitOfWork.SaveChangesAsync();
            return MapToEntityDto(production);
        }

        [AbpAuthorize(PermissionNames.Pages_Shoemaker_Step1)]
        public async Task<bool> RegisterStep1(long productionId)
        {
            var production = await Repository.FirstOrDefaultAsync(x => x.Id == productionId && x.Status == ProductionChainStatus.New);
            if (production == null)
            {
                throw new UserFriendlyException("Invalid production");
            }
            production.Status = ProductionChainStatus.Shoemaker_Step1;
            return true;
        }

        [AbpAuthorize(PermissionNames.Pages_Shoemaker_Step2)]
        public async Task<bool> RegisterStep2(long productionId)
        {
            var production = await Repository.FirstOrDefaultAsync(x => x.Id == productionId && x.Status == ProductionChainStatus.Shoemaker_Step1);
            if (production == null)
            {
                throw new UserFriendlyException("Invalid production");
            }
            if (!production.IsAnythingFilled || !production.DepartureTime.HasValue)
            {
                throw new UserFriendlyException("Cannot register this production");
            }
            production.Step2StartDate = DateTime.UtcNow;
            production.Status = ProductionChainStatus.Shoemaker_Step2;
            return true;
        }

        [AbpAuthorize(PermissionNames.Pages_Shoemaker_Step2)]
        public async Task<bool> CompleteProduction(long productionId)
        {
            var production = await Repository.FirstOrDefaultAsync(x => x.Id == productionId && x.Status == ProductionChainStatus.Shoemaker_Step2);
            if (production == null)
            {
                throw new UserFriendlyException("Invalid production");
            }
            if (!production.IsCompleted)
            {
                throw new UserFriendlyException("Cannot mark this production as completed");
            }

            production.Status = ProductionChainStatus.Completed;
            production.Step2EndDate = DateTime.UtcNow;
            return true;
        }

        public async Task<List<InventoryItemDto>> GetInventory()
        {
            return await GetInventoryQuery()
                .Select(x => new InventoryItemDto
                {
                    Id = x.Id,
                    InventoryName = $"RSID: {x.Leather.Id} ({x.ArrivalDate.ToString("dd-MM-yyyy")})",
                })
                .ToListAsync();
        }

        private IQueryable<Entities.Inventory> GetInventoryQuery()
        {
            ShoemakerType? shoemakerType = GetShoemakerType();

            var query = _inventoryRepository.GetAll();
            if (shoemakerType.HasValue)
            {
                query = query.Where(x => x.ShoemakerType == shoemakerType);
            }
            return query.OrderBy(x => x.Leather.Id);
        }

        private ShoemakerType? GetShoemakerType()
        {
            var userRole = _sessionService.GetCurrentUserRole();
            switch (userRole)
            {
                case AppUserRole.ShoemakerAemme:
                    return ShoemakerType.Aemme;
                case AppUserRole.ShoemakerFramat:
                    return ShoemakerType.Framat;
                case AppUserRole.ShoemakerItaly:
                    return ShoemakerType.Italy;
                default:
                    return null;
            }
        }
    }
}
