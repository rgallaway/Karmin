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
    [Route("api/message")]
    [ApiController]        
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseItem>> GetEmotions(string message)
        {
            // This will pass some stuff through to a few APIs and return a result
            var response = new ResponseItem
            {
                OriginalMessage = message,
                Temperature = await GetTemperatureFromMessage(message),
                Entities = await GetEntitiesFromMessage(message)
            };
            return response;
        }

        private async Task<int> GetTemperatureFromMessage(string message)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, Constants.QemotionApiUrl))
            {
                // Send request to Qemotion API
                requestMessage.Headers.Add("text", message);
                requestMessage.Headers.Add("lang", "en");
                // requestMessage.Headers.Add("Content-Type", "application/json; charset=UTF-8");
                var response = await Program.QemotionClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();

                // Parse response (in snake_case for some reason)
                var emotionResponse = JsonConvert.DeserializeObject<EmotionResponseItem>(jsonString, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver 
                    { 
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    },
                    NullValueHandling = NullValueHandling.Ignore
                });

                return emotionResponse.Content.Emotions.Eindex;
            }
        }

        private async Task<List<EntityItem>> GetEntitiesFromMessage(string message)
        {
            // Build JSON object for request to Azure API
            var request = new EntityRequestItem {
                Documents = new List<DocumentItem> {
                    new DocumentItem {
                        Language = "en",
                        Id = "1",
                        Text = message
                    }
                }
            };
            // Call Azure entity extraction API
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Program.EntityClient.PostAsync(Constants.EntityApiUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var entityResponse = JsonConvert.DeserializeObject<EntityResponseItem>(jsonString);

            // Parse response from entity extraction API and get data we care about
            var entities = new List<EntityItem>();
            foreach (var doc in entityResponse.Documents)
            {
                foreach (var entry in doc.Entities)
                {
                    entities.Add(new EntityItem 
                    {
                        Name = entry.Name,
                        Type = entry.Type,
                        Length = entry.Matches[0].Length
                    });
                }
            }

            return entities;
        }
    }
}