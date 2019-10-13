using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using ResponsibleSystem.Authorization.Permissions;
using ResponsibleSystem.Domain.Enums;
using ResponsibleSystem.Shared.Dto;
using ResponsibleSystem.Tannery.Dto;

namespace ResponsibleSystem.Tannery
{
    [AbpAuthorize(PermissionNames.Pages_Tannery)]
    public class TanneryAppService : ResponsibleSystemAppServiceBase
    {
        private readonly IRepository<Entities.Leather, long> _repository;

        public TanneryAppService(IRepository<Entities.Leather, long> repository)
        {
            _repository = repository;
        }

        public Task<List<TanneryLeatherDto>> GetAll()
        {
            return _repository.GetAll()
                .Where(x => x.Status == LeatherProductionChainStatus.Tannery ||
                            x.Status == LeatherProductionChainStatus.Storage)
                .Select(x => new TanneryLeatherDto
                {
                    FarmId = x.FarmId,
                    Farm = x.Farm.Name,
                    Id = x.Id,
                    IdNo = x.IdNo,
                    PPNo = x.PPNo,
                    IsCrust =  x.IsCrust,
                    Extra = x.Extra,
                    IsWaxed =  x.IsWaxed,
                    PricePerFt = x.PricePerFt,
                    Thickness = x.Thickness,
                    TotalArea = x.TotalArea,
                    InStorage = x.Status == LeatherProductionChainStatus.Storage
                })
                .ToListAsync();
        }

        public async Task<bool> RegisterLeather(RegisterTanneryLeatherInput input)
        {
            var leather = await _repository.GetAll().FirstOrDefaultAsync(x => x.PPNo == input.PPNo &&
                                                                        x.IdNo == input.IdNo &&
                                                                        x.Status == LeatherProductionChainStatus.Slaughterhouse);
            if (leather == null)
            {
                throw new UserFriendlyException("Leather not found");
            }

            leather.Status = LeatherProductionChainStatus.Tannery;
            leather.TanneryUserId = AbpSession.UserId;

            leather.IsCrust = input.IsCrust;
            leather.Thickness = input.Thickness;
            leather.TotalArea = input.TotalArea;
            leather.PricePerFt = input.PricePerFt;
            leather.IsWaxed = input.IsWaxed;
            leather.Extra = input.Extra;

            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(UpdateTanneryLeatherInput input)
        {
            var leather = await _repository.GetAll()
                .Where(x => (x.Status == LeatherProductionChainStatus.Tannery ||
                             x.Status == LeatherProductionChainStatus.Storage) &&
                            x.Id == input.Id)
                .FirstOrDefaultAsync();

            if (leather == null)
            {
                throw new UserFriendlyException("Leather not found");
            }

            if (leather.Status == LeatherProductionChainStatus.Storage)
            {
                throw new UserFriendlyException("Leather in storage cannot be updated");
            }

            leather.IsCrust = input.IsCrust;
            leather.Thickness = input.Thickness;
            leather.TotalArea = input.TotalArea;
            leather.PricePerFt = input.PricePerFt;
            leather.IsWaxed = input.IsWaxed;
            leather.Extra = input.Extra;

            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MoveToStorage(EntityDto<long> input)
        {
            var leather = await _repository.GetAll()
                .Where(x => x.Status == LeatherProductionChainStatus.Tannery &&
                            x.Id == input.Id)
                .FirstOrDefaultAsync();

            if (leather == null)
            {
                throw new UserFriendlyException("Leather not found");
            }

            if (leather.Status == LeatherProductionChainStatus.Storage)
            {
                throw new UserFriendlyException("Leather is already in storage");
            }

            leather.Status = LeatherProductionChainStatus.Storage;
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsValid(ValidateLeatherInput input)
        {
            var matchingCount = await _repository.CountAsync(x => x.IdNo == input.IdNumber &&
                                                                  x.PPNo == input.PPNumber &&
                                                                  x.Status == LeatherProductionChainStatus.Slaughterhouse);
            return matchingCount > 0;
        }
    }
}
