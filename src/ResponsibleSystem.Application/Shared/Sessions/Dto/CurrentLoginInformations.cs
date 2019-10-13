using ResponsibleSystem.Authorization.Users;

namespace ResponsibleSystem.Shared.Sessions.Dto
{
    public class CurrentLoginInformations
    {
        public ApplicationInfoDto Application { get; set; }
        public User User { get; set; }
        public AppHeaders Headers { get; set; }
    }
}