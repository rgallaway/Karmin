using Hangfire;
using Newtonsoft.Json;
using Karmin.Models;
using Karmin.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Karmin.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public ActionResult ScheduleNotification(NotificationSchedulerItem item)
        {
            BackgroundJob.Schedule(() => Console.WriteLine("IT WORKED " + item.data), TimeSpan.FromSeconds(10));
            return Content("Scheduled notification");
        }
    }
}