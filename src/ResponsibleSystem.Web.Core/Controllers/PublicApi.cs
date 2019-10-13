using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResponsibleSystem.Entities.Enums;
using ResponsibleSystem.Models.PublicApi;

namespace ResponsibleSystem.Controllers
{
    public class PublicApi : ResponsibleSystemControllerBase
    {
        private readonly IRepository<Entities.Production, long> _productionRepository;
        private readonly IRepository<Entities.Leather, long> _leatherRepository;

        public PublicApi(IRepository<Entities.Production, long> productionRepository,
            IRepository<Entities.Leather, long> leatherRepository)
        {
            _productionRepository = productionRepository;
            _leatherRepository = leatherRepository;
        }

        [HttpGet("/api/products/{id}")]
        [DontWrapResult]
        public async Task<IActionResult> Get(long id)
        {
            var production = await _productionRepository
                .GetAll()
                .Where(x => x.Id == id && x.Status == ProductionChainStatus.Completed)
                .Select(x=> new
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
                    RemovableInSockLeather = x.RemovableInSockLeather.LeatherId
                })
                .FirstOrDefaultAsync();

            if (production == null)
            {
                return new NotFoundResult();
            }

            var leatherIds = new[]
            {
                production.UpperLeather,
                production.LiningLeather,
                production.BackCounterLeather,
                production.WeltLeather,
                production.SoleLeather,
                production.HeelLeather,
                production.InSockLeather,
                production.FillingLeather,
                production.ReinforcementLeather,
                production.RemovableInSockLeather
            }
                .OfType<long>()
                .ToHashSet();

            var leathers = await _leatherRepository
                .GetAll()
                .Where(x => leatherIds.Contains(x.Id))
                .Select(x=> new LeatherInfo
                {
                    RSID = x.Id,
                    IsCrust = x.IsCrust,
                    Gender = x.Gender,
                    IsWaxed = x.IsWaxed,
                    Color = x.Color,
                    Age = x.Age,
                    ExtraInfo = x.Extra,
                    FarmName = x.Farm.Name,
                    FarmLatitude = x.Farm.Latitude,
                    FarmLongitude = x.Farm.Longitude
                })
                .ToArrayAsync();

            ProductionPartInfo rsid(long? idVal) => new ProductionPartInfo{RSID = idVal.Value};

            return Json(new ProductionInfoDto
            {
                Leathers = leathers,
                UpperLeather = rsid(production.UpperLeather),
                LiningLeather = rsid(production.LiningLeather),
                BackCounterLeather = rsid(production.BackCounterLeather),
                WeltLeather = rsid(production.WeltLeather),
                SoleLeather = rsid(production.SoleLeather),
                HeelLeather = rsid(production.HeelLeather),
                InSockLeather = rsid(production.InSockLeather),
                FillingLeather = rsid(production.FillingLeather),
                ReinforcementLeather = rsid(production.ReinforcementLeather),
                RemovableInSockLeather = rsid(production.RemovableInSockLeather)
            });
        }
    }
}
