using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TestApplication.DTO.QueueGroup;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MonitoringController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetQueueGroupData()
        {
            var data = _context.Monitors.Include(m => m.QueueGroup).Select(m => new QueueGroupDTO
            {
                QueueGroupName = m.QueueGroup.Name,
                Offered = m.Offered,
                Handled = m.Handled,
                AverageTalkTime = TimeSpan.FromSeconds(m.Handled == 0 ? 0 : m.TalkTime / m.Handled).ToString(@"hh\:mm\:ss"),
                AverageHandlingTime = TimeSpan.FromSeconds(m.Handled == 0 ? 0 : (m.TalkTime + m.AfterCallWorkTime) / m.Handled).ToString(@"hh\:mm\:ss"),
                ServiceLevel = m.Offered == 0 ? 0 : (float)Math.Round((float)m.HandledWithinSL / m.Offered * 100, 1),
                SLA_Percent = m.QueueGroup.SLA_Percent,
            }).ToList();

            return Ok(data);
        }
    }
}
