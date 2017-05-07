namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using DataProviders;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;

    [Route("api/[controller]")]
    public class LandlordController : Controller
    {
        private readonly ILandlordDataProvider _landlordDataProvider;

        public LandlordController(ILandlordDataProvider landlordDataProvider)
        {
            _landlordDataProvider = landlordDataProvider;
        }

        [HttpGet]
        [RequiresPermission(Permissions.LandlordList)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _landlordDataProvider.GetListAsync(User.GetAgencyId()));
        }
    }
}