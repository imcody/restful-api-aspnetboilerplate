using Microsoft.AspNetCore.Antiforgery;
using ResponsibleSystem.Controllers;

namespace ResponsibleSystem.Web.Host.Controllers
{
    public class AntiForgeryController : ResponsibleSystemControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
