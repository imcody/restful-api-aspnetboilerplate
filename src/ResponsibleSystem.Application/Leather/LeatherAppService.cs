using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Authorization.Roles;
using ResponsibleSystem.Backoffice.Users.Dto;
using ResponsibleSystem.Domain.Enums;
using ResponsibleSystem.Entities;
using ResponsibleSystem.Extensions;
using ResponsibleSystem.Leather.Dto;
using ResponsibleSystem.Shared.Sessions.Services;

namespace ResponsibleSystem.Leather
{
    [AbpAuthorize(PermissionNames.Pages_Leather)]
    public class LeatherAppService : AsyncCrudAppService<Entities.Leather, LeatherDto, long, PagedResultRequestDto, CreateLeatherDto, LeatherDto>
    {
        protected override string CreatePermissionName => PermissionNames.Leather_Add;
        protected override string UpdatePermissionName => PermissionNames.Leather_Edit;
        protected override string DeletePermissionName => PermissionNames.Leather_Delete;

        private readonly ISessionService _sessionService;
        private readonly IRepository<Farm, long> _farmRepository;

        public LeatherAppService(IRepository<Entities.Leather, long> leatherRepository,
            ISessionService sessionService,
            IRepository<Farm, long> farmRepository) : base(leatherRepository)
        {
            _sessionService = sessionService;
            _farmRepository = farmRepository;
        }

        protected override IQueryable<Entities.Leather> CreateFilteredQuery(PagedResultRequestDto input)
        {
            CheckGetAllPermission();

            var userRole = _sessionService.GetCurrentUserRole();

            var leathersQuery = Repository.GetAllIncluding(x => x.Farm);
            if (userRole == AppUserRole.Farmer)
            {
                var currentUserFarmId = _sessionService.GetCurrentUserFarmId();
                leathersQuery = leathersQuery.Where(x => x.FarmId == currentUserFarmId && x.Status == LeatherProductionChainStatus.Farmer);
            }
            return leathersQuery;
        }

        [AbpAuthorize(PermissionNames.Leather_Details)]
        public Task<LeatherDetailsDto> GetDetails(EntityDto<long> input)
        {
            return CreateFilteredQuery(null)
                .Where(x => x.Id == input.Id)
                .Select(x => new LeatherDetailsDto()
                {
                    IdNo = x.IdNo,
                    PPNo = x.PPNo,
                    Status =  x.Status.ToString(),
                    EarsOn = x.EarsOn,
                    Weight = x.Weight,
                    Slaughterhouse = x.SlaughterhouseUser != null ? x.SlaughterhouseUser.UserRole.GetDescriptionFromValue() : "n/a",
                    Tannery = x.TanneryUser != null ? x.TanneryUser.UserRole.GetDescriptionFromValue() : "n/a",
                    IsCrust = x.IsCrust,
                    Farm = x.Farm.Name,
                    Extra = x.Extra,
                    Gender = x.Gender,
                    IsWaxed = x.IsWaxed,
                    PricePerFt = x.PricePerFt,
                    TotalArea = x.TotalArea,
                    Thickness = x.Thickness,
                    Age = x.Age,
                    Color = x.Color,
                    EstimatedSlaughterDate = x.EstimatedSlaughterDate
                })
                .FirstOrDefaultAsync();
        }

        public override async Task<LeatherDto> Get(EntityDto<long> input)
        {
            CheckGetPermission();

            var userFarmId = _sessionService.GetCurrentUserFarmId();

            var entityQuery = Repository.GetAll();
            if (userFarmId != null)
            {
                entityQuery = entityQuery.Where(x => x.FarmId == userFarmId);
            }
            var entity = await entityQuery.FirstOrDefaultAsync(x => x.Id == input.Id);

            return MapToEntityDto(entity);
        }

        public override async Task<LeatherDto> Create(CreateLeatherDto input)
        {
            CheckCreatePermission();
            if (input != null && _sessionService.GetCurrentUserRole() == AppUserRole.Farmer)
            {
                input.FarmId = _sessionService.GetCurrentUserFarmId();
            }

            var existing = await Repository.CountAsync(x =>
                x.IdNo.Equals(input.IdNo, StringComparison.InvariantCultureIgnoreCase) &&
                x.PPNo.Equals(input.PPNo, StringComparison.InvariantCultureIgnoreCase));
            try
            {
                return await base.Create(input);
            }
            catch (Exception e)
            {
                if (IsDuplicateException(e))
                {
                    throw new UserFriendlyException("Animal with this PP Number and Id number already exists");
                }
                throw;
            }
        }

        public override async Task<LeatherDto> Update(LeatherDto input)
        {
            CheckUpdatePermission();
            if (input != null && _sessionService.GetCurrentUserRole() == AppUserRole.Farmer)
            {
                input.Status = AppUserRole.Farmer.GetDescriptionFromValue();
                input.FarmId = _sessionService.GetCurrentUserFarmId();
                var canEdit = await Repository.CountAsync(x => x.Id == input.Id && 
                                                                       x.Status == LeatherProductionChainStatus.Farmer &&
                                                                       x.FarmId == input.FarmId) > 0;
                if (!canEdit)
                {
                    throw new UserFriendlyException("Cannot update this animal");
                }
            }
            try
            {
                return await base.Update(input);
            }
            catch (Exception e)
            {
                if (IsDuplicateException(e))
                {
                    throw new UserFriendlyException("Animal with this PP Number and Id number already exists");
                }
                throw;
            }
        }

        private bool IsDuplicateException(Exception e)
        {
            var msg = new StringBuilder();
            var curr = e;
            while (curr != null)
            {
                msg.AppendLine(curr.Message);
                curr = curr.InnerException;
            }

            var msgStr = msg.ToString();

            return (msgStr.Contains("IX_Leathers_IdNo_PPNo") &&
                    msgStr.Contains("duplicate"));
        }

        [AbpAuthorize(PermissionNames.Pages_Farms)]
        public Task<List<FarmInfoDto>> GetFarms()
        {
            return _farmRepository.GetAll()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new FarmInfoDto { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }
    }
}
