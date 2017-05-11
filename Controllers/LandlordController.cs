namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Permissions;
    using Landlords.Interfaces;
    using Model;

    [Route("api/[controller]")]
    public class LandlordController : Controller
    {
        private readonly ILandlordDataProvider _landlordDataProvider;

        public LandlordController(ILandlordDataProvider landlordDataProvider)
        {
            _landlordDataProvider = landlordDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_LL.ListId, Permissions_LL.ListRouteId, Permissions_LL.ListDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _landlordDataProvider.GetListAsync(User.GetAgencyId()));
        }
    }
}