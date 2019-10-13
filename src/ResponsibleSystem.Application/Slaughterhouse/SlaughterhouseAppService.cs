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
using ResponsibleSystem.Slaughterhouse.Dto;

namespace ResponsibleSystem.Slaughterhouse
{
    [AbpAuthorize(PermissionNames.Pages_Slaughterhouse)]
    public class SlaughterhouseAppService : ResponsibleSystemAppServiceBase
    {
        private readonly IRepository<Entities.Leather, long> _repository;

        public SlaughterhouseAppService(IRepository<Entities.Leather, long> repository)
        {
            _repository = repository;
        }

        public Task<List<SlaughterhouseLeatherDto>> GetAll()
        {
            return _repository.GetAll()
                .Where(x => x.Status == LeatherProductionChainStatus.Slaughterhouse)
                .Select(x => new SlaughterhouseLeatherDto
                {
                    FarmId = x.FarmId,
                    Farm = x.Farm.Name,
                    Id = x.Id,
                    EarsOn = x.EarsOn,
                    IdNo = x.IdNo,
                    PPNo = x.PPNo,
                    Weight = x.Weight
                })
                .ToListAsync();
        }

        public async Task<bool> RegisterLeather(RegisterLeatherInput input)
        {
            var leather = await _repository.GetAll().FirstOrDefaultAsync(x => x.PPNo == input.PPNo &&
                                                                        x.IdNo == input.IdNo &&
                                                                        x.Status == LeatherProductionChainStatus.Farmer);
            if (leather == null)
            {
                throw new UserFriendlyException("Animal not found or already registered");
            }

            leather.Status = LeatherProductionChainStatus.Slaughterhouse;
            leather.Weight = input.Weight;
            leather.EarsOn = input.EarsOn;
            leather.SlaughterhouseUserId = AbpSession.UserId;
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(UpdateLeatherInput input)
        {
            var leather = await _repository.GetAll()
                .Where(x => x.Status == LeatherProductionChainStatus.Slaughterhouse &&
                            x.Id == input.Id)
                .FirstOrDefaultAsync();

            if (leather == null)
            {
                throw new UserFriendlyException("Animal not found");
            }

            leather.Weight = input.Weight;
            leather.EarsOn = input.EarsOn;
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsValid(ValidateLeatherInput input)
        {
            var matchingCount = await _repository.CountAsync(x => x.IdNo == input.IdNumber &&
                                                                  x.PPNo == input.PPNumber &&
                                                                  x.Status == LeatherProductionChainStatus.Farmer);
            return matchingCount > 0;
        }
    }
}
