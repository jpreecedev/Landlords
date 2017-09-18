namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using ViewModels;

    [Route("api/[controller]")]
    public class MaintenanceRequestsController : Controller
    {
        private readonly IMaintenanceRequestsDataProvider _maintenanceRequestsDataProvider;
        private readonly IPropertyDataProvider _propertyDataProvider;

        public MaintenanceRequestsController(IMaintenanceRequestsDataProvider maintenanceRequestsDataProvider, IPropertyDataProvider propertyDataProvider)
        {
            _maintenanceRequestsDataProvider = maintenanceRequestsDataProvider;
            _propertyDataProvider = propertyDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_MR.OverviewId, Permissions_MR.OverviewRouteId, Permissions_MR.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _maintenanceRequestsDataProvider.GetMaintenanceRequests(User));
        }

        [HttpGet("ViewData")]
        [Permission(Permissions_MR.OverviewId, Permissions_MR.OverviewRouteId, Permissions_MR.OverviewDescription)]
        public async Task<IActionResult> GetViewData()
        {
            if (User.IsTenant())
            {
                return Ok(new MaintenanceRequestOverviewViewModel());
            }

            return Ok(new MaintenanceRequestOverviewViewModel
            {
                Properties = await _propertyDataProvider.GetBasicDetailsAsync(User.GetUserId())
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_MR.NewId, Permissions_MR.NewRouteId, Permissions_MR.NewDescription)]
        public async Task<IActionResult> New([FromBody] MaintenanceRequestViewModel value)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _maintenanceRequestsDataProvider.CreateAsync(User, value));
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }

        [HttpPost("newentry"), ValidateAntiForgeryToken]
        [Permission(Permissions_MR.NewEntryId, Permissions_MR.NewEntryRouteId, Permissions_MR.NewEntryDescription)]
        public async Task<IActionResult> NewEntry([FromBody] MaintenanceEntryViewModel value)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _maintenanceRequestsDataProvider.AddMaintenanceEntryAsync(User, value));
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }

        [HttpPost("archive/{maintenanceRequestId}"), ValidateAntiForgeryToken]
        [Permission(Permissions_MR.ArchiveId, Permissions_MR.ArchiveRouteId, Permissions_MR.ArchiveDescription)]
        public async Task<IActionResult> Archive(Guid maintenanceRequestId)
        {
            if (ModelState.IsValid)
            {
                await _maintenanceRequestsDataProvider.ArchiveMaintenanceRequest(User, maintenanceRequestId);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}