﻿using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ISender _sender;

        // Constructor
        public NotificationController(ISender sender)
        {
            _sender = sender;
        }

        // Create Notification
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationCommand request)
        {
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // Delete Notification
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int userId)
        {
            var result = await _sender.Send(new DeleteNotificationCommand(id, userId));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // Get Notification by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetNotificationByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }

        // Get All Notifications
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sender.Send(new GetAllNotificationsQuery());
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        // Get Unread Notifications
        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnread(int userId)
        {
            var result = await _sender.Send(new GetUnreadNotificationsQuery(userId));
            return result.Count > 0 ? Ok(result) : NoContent();
        }

        // Mark Notification as Read
        [HttpPut("markAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _sender.Send(new MarkNotificationAsReadCommand(id));

            if (result.IsSuccess)
            {
                return Ok(result); // Əgər əməliyyat uğurludursa
            }

            return BadRequest(result); // Əgər əməliyyat uğursuzdursa
        }
    }

}

