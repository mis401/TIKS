﻿using event_driven_backend.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace event_driven_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private Context context { get; set; }

        private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger, Context context)
        {
            _logger = logger;
            this.context = context;
        }
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] NewDocumentDTO nd)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.ID == nd.creatorId);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            
            var calendar = await context.Calendars.FirstOrDefaultAsync(cal => cal.ID == nd.calendarId);
            if (calendar == null)
            {
                return BadRequest("Calendar not found");
            }
            var document = new Document {
                Name = nd.Name, 
                Creator = user, 
                Calendar = calendar, 
                CreatedAt = DateTime.Now.ToUniversalTime(), 
                UpdatedAt = DateTime.Now.ToUniversalTime(), 
                Text = String.Empty
            };
            calendar.Documents.Add(document);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(document);
        }
    }
}