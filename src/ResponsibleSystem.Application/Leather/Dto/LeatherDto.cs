using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace ResponsibleSystem.Leather.Dto
{
    public class LeatherDto: CreateLeatherDto, IEntityDto<long>
    {
        public long Id { get; set; }
        public string Farm { get; set; }
        public string Status { get; set; }
    }
}
