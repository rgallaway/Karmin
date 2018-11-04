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
            BackgroundJob.Schedule(() => SendNotificationAsync(item), TimeSpan.FromSeconds(item.Time));
            return Content("Scheduled notification");
        }

        public async Task SendNotificationAsync(NotificationSchedulerItem item)
        {
            var alert = "{\"aps\":{\"alert\":\"" + item.Text + "\",\"sound\":\"default\"}}";
            await Program.NotificationClient.SendAppleNativeNotificationAsync(alert);
        }
    }
}