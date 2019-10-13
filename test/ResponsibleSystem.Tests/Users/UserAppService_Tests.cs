using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Abp.Application.Services.Dto;
using ResponsibleSystem.Backoffice.Users;
using ResponsibleSystem.Backoffice.Users.Dto;

namespace ResponsibleSystem.Tests.Backoffice.Users
{
    public class BackofficeUserAppService_Tests : ResponsibleSystemTestBase
    {
        private readonly IUserAppService _userAppService;

        public BackofficeUserAppService_Tests()
        {
            _userAppService = Resolve<IUserAppService>();
        }

        [Fact]
        public async Task GetUsers_Test()
        {
            // Act
            var output = await _userAppService.GetAll(new PagedResultRequestDto{MaxResultCount=20, SkipCount=0} );

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CreateUser_Test()
        {
            // Act
            await _userAppService.Create(
                new CreateUserDto
                {
                    EmailAddress = "john@volosoft.com",
                    IsActive = true,
                    Name = "John",
                    Surname = "Nash",
                    Password = "123qwe",
                });

            await UsingDbContextAsync(async context =>
            {
                var johnNashUser = await context.Users.FirstOrDefaultAsync(u => u.EmailAddress == "john@volosoft.com");
                johnNashUser.ShouldNotBeNull();
            });
        }
    }
}
