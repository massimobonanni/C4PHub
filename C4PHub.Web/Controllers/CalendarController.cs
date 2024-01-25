using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace C4PHub.Web.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IC4PCalendarGenerator _calendarGenerator;
        private readonly IC4PPersistance _persistance;

        public CalendarController(IC4PCalendarGenerator calendarGenerator,
            IC4PPersistance persistance,
            ILogger<CalendarController> logger)
        {
            _calendarGenerator=calendarGenerator;
            _persistance=persistance;
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromQuery(Name = "c4pId")]string c4pId, [FromQuery(Name ="year")] string year,
            [FromQuery(Name ="type")] CalendarType type, [FromQuery(Name ="format")] CalendarFormat format)
        {
            var c4PInfo = await _persistance.GetC4PAsync(c4pId,year);
            if (c4PInfo == null)
            {
                return NotFound();
            }
            var iCal = await _calendarGenerator.GenerateAsync(c4PInfo, format, type);
            byte[] calendarBytes = System.Text.Encoding.UTF8.GetBytes(iCal); 
            
            return File(calendarBytes, "text/calendar", $"{c4PInfo.EventName}-{type}-event.ics");

        }
    }
}
