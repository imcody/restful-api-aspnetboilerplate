using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using ResponsibleSystem.Configuration;

namespace ResponsibleSystem.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly ResponsibleSystemDbContext _context;

        public DefaultSettingsCreator(ResponsibleSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, AppConfig.Defaults.MailFromAddress);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, AppConfig.Defaults.MailFromDisplayName);

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, AppConfig.Defaults.Language);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
