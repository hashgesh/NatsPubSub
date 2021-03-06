﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NATS.Client;
using snow_app.Nats;
using snow_app.Services;

namespace snow_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageService _messageService;

        public MessageController(ILogger<MessageController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>>  Get()
        {
            //_logger.LogDebug("Get request received to present messages");
            return _messageService.GetAllMessagesFromCache();
        }

        [HttpPost]
        public async Task<Message>  Post(Message message)
        {
            //_logger.LogDebug("Post request received", message);
            return  await _messageService.PublishMessageToTopic(message);
        }

    }
}
