using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookOpinions.Web.Areas.Admin.Controllers
{
    [Area(WebConstants.AdminArea)]
    [Authorize(Roles = WebConstants.AdminRole)]
    public class BaseAdminController : Controller
    {
    }
}
