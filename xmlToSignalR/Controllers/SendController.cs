using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace xmlToSignalR.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IHubContext<TestXmlHub, ITestXmlHubClient> _hub;

        public SendController(IHubContext<TestXmlHub, ITestXmlHubClient> hub)
        {
            _hub = hub;
        }
        
        [HttpPost]
        public async Task<NoContentResult> Send()
        {
            var message = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <note>
                <to>Tove</to>
                <from>Jani</from>
                <heading>Reminder</heading>
                <body>Don't forget me this weekend!</body>
                </note>";
            await _hub.Clients.All.SendXml(message);

            return NoContent();
        }
        
        [HttpPost("serialized")]
        public async Task<NoContentResult> SendModel()
        {
            var model = new SomeModel
            {
                Id = Guid.NewGuid(),
                Name = "Model",
                Created = new()
                {
                    Creator = "System",
                    Date = new DateTime()
                }
            };
            
            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(SomeModel));
            serializer.Serialize(stringWriter, model);
            var message = stringWriter.ToString();
            await _hub.Clients.All.SendXml(message);

            return NoContent();
        }
    }
}