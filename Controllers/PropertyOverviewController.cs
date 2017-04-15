namespace Landlords.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Landlords.Database;

    [Route("api/[controller]")]
    public class PropertyOverviewController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public PropertyOverviewController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [Authorize(Roles = ApplicationRoles.Landlord)]
        [HttpGet]
        public PropertyOverview Get()
        {
            return _dataAccessProvider.GetPropertyOverview();
        }
        
        [HttpPost]
        public void Post([FromBody] PropertyOverview value)
        {
            _dataAccessProvider.SavePropertyOverview(value);
        }
    }
}