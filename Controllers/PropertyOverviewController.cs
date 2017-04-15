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

        [Authorize(Roles = "Admin")]
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