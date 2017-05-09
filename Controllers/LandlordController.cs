namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Permissions;
    using Landlords.Interfaces;
    using Model.Permissions;

    [Route("api/[controller]")]
    public class LandlordController : Controller
    {
        private readonly ILandlordDataProvider _landlordDataProvider;

        public LandlordController(ILandlordDataProvider landlordDataProvider)
        {
            _landlordDataProvider = landlordDataProvider;
        }

        [HttpGet]
        [Permission(LandlordList.ListId, LandlordList.ListRouteId, LandlordList.ListDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _landlordDataProvider.GetListAsync(User.GetAgencyId()));
        }
    }
}