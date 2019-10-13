using Abp.MultiTenancy;
using System.Collections.Generic;
using ResponsibleSystem.Authorization.Users;

namespace ResponsibleSystem.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
