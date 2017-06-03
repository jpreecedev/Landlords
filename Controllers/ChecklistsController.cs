namespace Landlords.Controllers
{
    using Landlords.Permissions;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using System;
    using ViewModels;

    [Route("api/[controller]")]
    public class ChecklistsController : Controller
    {
        private readonly IChecklistDataProvider _checklistDataProvider;
        private readonly IChecklistInstanceDataProvider _checklistInstanceDataProvider;

        public ChecklistsController(IChecklistDataProvider checklistDataProvider, IChecklistInstanceDataProvider checklistInstanceDataProvider)
        {
            _checklistDataProvider = checklistDataProvider;
            _checklistInstanceDataProvider = checklistInstanceDataProvider;
        }

        [HttpGet("{checklistId}")]
        [Permission(Permissions_CL.GetById, Permissions_CL.GetByRouteId, Permissions_CL.GetByDescription)]
        public async Task<IActionResult> Get(Guid checklistId)
        {
            return Ok(await _checklistInstanceDataProvider.GetChecklistByIdAsync(User.GetPortfolioId(), checklistId));
        }

        [HttpGet("overview")]
        [Permission(Permissions_CL.OverviewId, Permissions_CL.OverviewRouteId, Permissions_CL.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            var portfolioId = User.GetPortfolioId();
            var agencyId = User.GetAgencyId();
            
            return Ok(await _checklistDataProvider.GetChecklistOverviewAsync(portfolioId, agencyId, false));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_CL.CreateId, Permissions_CL.CreateRouteId, Permissions_CL.CreateDescription)]
        public async Task<IActionResult> Post(Guid checklistId, Guid? propertyDetailsId)
        {
            if (checklistId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            return Ok(await _checklistInstanceDataProvider.CreateChecklistInstanceAsync(User.GetPortfolioId(), checklistId, propertyDetailsId));
        }
        
        [HttpPost("archive"), ValidateAntiForgeryToken]
        [Permission(Permissions_CL.ArchiveId, Permissions_CL.ArchiveRouteId, Permissions_CL.ArchiveDescription)]
        public async Task<IActionResult> Archive(Guid checklistId)
        {
            if (checklistId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistInstanceDataProvider.ArchiveChecklistInstanceAsync(User.GetPortfolioId(), checklistId);
            return Ok();
        }

        [HttpGet("archived")]
        [Permission(Permissions_CL.ArchivedId, Permissions_CL.ArchivedRouteId, Permissions_CL.ArchivedDescription)]
        public async Task<IActionResult> Archived()
        {
            return Ok(await _checklistInstanceDataProvider.GetArchivedChecklistInstancesAsync(User.GetPortfolioId()));
        }

        [HttpDelete("{checklistId}"), ValidateAntiForgeryToken]
        [Permission(Permissions_CL.DeleteById, Permissions_CL.DeleteByRouteId, Permissions_CL.DeleteByDescription)]
        public async Task<IActionResult> Delete(Guid checklistId)
        {
            if (checklistId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistInstanceDataProvider.DeleteInstanceAsync(User.GetPortfolioId(), checklistId);
            return Ok();
        }
    }
}