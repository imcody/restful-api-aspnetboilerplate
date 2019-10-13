using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Authorization.Users;
using ResponsibleSystem.Domain.Enums;
using ResponsibleSystem.Entities.Enums;
using ResponsibleSystem.Inventory.Dto;
using ResponsibleSystem.Shared.Sessions.Services;

namespace ResponsibleSystem.Inventory
{
    [AbpAuthorize(PermissionNames.Pages_Inventory)]
    public class InventoryAppService : AsyncCrudAppService<Entities.Inventory, InventoryDto, long, PagedResultRequestDto, RegisterInventoryDto, InventoryDto>
    {
        private readonly IRepository<Entities.Leather, long> _leatherRepository;
        private readonly UserManager _userManager;
        private readonly SessionService _sessionService;

        public InventoryAppService(IRepository<Entities.Inventory, long> repository,
            IRepository<Entities.Leather, long> leatherRepository,
            UserManager userManager,
            SessionService sessionService) : base(repository)
        {
            _leatherRepository = leatherRepository;
            _userManager = userManager;
            _sessionService = sessionService;
        }

        protected override IQueryable<Entities.Inventory> CreateFilteredQuery(PagedResultRequestDto input)
        {
            ShoemakerType? shoemakerType = GetShoemakerType();

            if (shoemakerType.HasValue)
            {
                return Repository.GetAll().Where(x => x.ShoemakerType == shoemakerType.Value);
            }
            throw new UserFriendlyException("User with this role cannot access this endpoint");
        }

        public override async Task<InventoryDto> Create(RegisterInventoryDto input)
        {
            var shoemakerType = GetShoemakerType();
            if (shoemakerType == null)
            {
                throw new UserFriendlyException("User with this role cannot access this endpoint");
            }

            var leather = await _leatherRepository.FirstOrDefaultAsync(x =>
                x.Id == input.LeatherId &&
                x.Status == LeatherProductionChainStatus.Storage &&
                !x.Farm.IsDeleted &&
                x.InventoryId == null);

            if (leather == null)
            {
                throw new UserFriendlyException("Invalid leather RSID");
            }

            var inventoryItem = new Entities.Inventory
            {
                Leather = leather,
                ArrivalDate = input.ArrivalDate,
                ShoemakerType = shoemakerType.Value,
                LeatherId = leather.Id
            };
            leather.Status = LeatherProductionChainStatus.Production;

            Repository.Insert(inventoryItem);
            _leatherRepository.Update(leather);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(inventoryItem);
        }

        public override async Task<InventoryDto> Update(InventoryDto input)
        {
            var shoemakerType = GetShoemakerType();
            if (shoemakerType == null)
            {
                throw new UserFriendlyException("User with this role cannot access this endpoint");
            }

            var inventoryItem = await Repository
                .FirstOrDefaultAsync(x => x.Id == input.Id && x.ShoemakerType == shoemakerType.Value);

            if (inventoryItem == null)
            {
                throw new UserFriendlyException("Invalid inventory item");
            }

            inventoryItem.ArrivalDate = input.ArrivalDate;
            Repository.Update(inventoryItem);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(inventoryItem);
        }

        public override Task Delete(EntityDto<long> input)
        {
            throw new UserFriendlyException("Not implemented");
        }

        private ShoemakerType? GetShoemakerType()
        {
            switch (_sessionService.GetCurrentUserRole())
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
