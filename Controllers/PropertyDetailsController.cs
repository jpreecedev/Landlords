﻿namespace Landlords.Controllers
{
    using DataProviders;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using System;
    using System.Linq;
    using Landlords.Core;
    using Landlords.ViewModels;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public class PropertyDetailsController : Controller
    {
        private readonly IPropertyDataProvider _propertyDataProvider;

        public PropertyDetailsController(IPropertyDataProvider dataAccessProvider)
        {
            _propertyDataProvider = dataAccessProvider;
        }

        [HttpGet("ViewData")]
        public PropertyDetailsViewModel GetViewData()
        {
            return new PropertyDetailsViewModel();
        }

        [HttpGet]
        public async Task<PropertyDetailsViewModel[]> Get()
        {
            var properties = await _propertyDataProvider.GetListAsync(User.GetUserId());
            return properties.Select(property => new PropertyDetailsViewModel(property)).ToArray();
        }

        [HttpGet("{propertyId}")]
        public async Task<PropertyDetailsViewModel> Get(Guid propertyId)
        {
            var propertyDetails = await _propertyDataProvider.GetDetailsAsync(User.GetUserId(), propertyId);
            return new PropertyDetailsViewModel(propertyDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PropertyDetailsViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _propertyDataProvider.CreateAsync(User, value);
                return Ok();
            }
            
            return BadRequest(new { Errors = ModelState.Errors() });
        }
    }
}