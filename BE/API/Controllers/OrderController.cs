﻿using BLL.Interface;
using DTO.Order;
using DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder order;

        public OrderController(IOrder order)
        {
            this.order = order;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Get(int orderState)
        {
            BaseResponseModel model = order.Get(orderState);

            return model.IsSuccess ? Ok(model) : BadRequest(model);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Order([FromBody] OrderRequestModule request)
        {
            BaseResponseModel model = order.Post(request);

            if (model.IsSuccess)
            {
                return Ok(model);
            }

            return BadRequest(model);
        }
    }
}
