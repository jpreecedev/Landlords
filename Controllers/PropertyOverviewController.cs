namespace Landlords.Controllers
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    
    [Route("api/[controller]")]
    public class PropertyOverviewController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public PropertyOverviewController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }
        
        [Authorize]
        [HttpGet]
        public PropertyOverview Get()
        {
            var claims = User.Claims.ToList();
            var identities = User.Identities.ToList();
            var identiyu = User.Identity;

            return _dataAccessProvider.GetPropertyOverview();
        }
        
        [HttpPost]
        public void Post()
        {
            //_dataAccessProvider.SavePropertyOverview(value);
        }
    }
}