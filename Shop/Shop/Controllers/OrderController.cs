using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shop.BLL.Interfaces;
using Shop.BLL.DTO;
using Shop.BLL.Infrastructure;
using Shop.PL.Models;


namespace Shop.PL.Controllers
{
	[Route("api/Orders")]
	[Authorize]
	[ApiController]
	public class OrderController : ControllerBase
	{

		private IOrderService service { get; set; }

		public OrderController(IOrderService service)
		{
			this.service = service;
		}

		// GET: api/<OrdersController>
		[HttpGet]
		public IActionResult Get()
		{
			var orderDTO = service.GetItems();
			return new JsonResult(MapperWEB.Mapper.Map<IEnumerable<OrderViewModel>>(orderDTO));
		}

		// GET api/<OrdersController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			try
			{
				var oDTO = service.GetItem(id);
				if (oDTO == null) return new NotFoundResult();
				return new JsonResult(MapperWEB.Mapper.Map<OrderViewModel>(oDTO));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}	
		}

		// GET api/<OrdersController>/5
		[HttpGet("ByUserId/{id}")]
		public IActionResult GetByUserId(int id)
		{
			try
			{
				var oDTO = service.GetItemsByUserId(id);
				if (oDTO == null) return new NotFoundResult();
				return new JsonResult(MapperWEB.Mapper.Map<IEnumerable<OrderViewModel>>(oDTO));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}
		}

		[Route("search")]
		[HttpGet]
		public IActionResult Get([FromQuery] string address)
		{
			try
			{
				var oDTO = service.GetItems(address);
			return new JsonResult(MapperWEB.Mapper.Map<IEnumerable<OrderViewModel>>(oDTO));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}
		}

		/*[HttpPost("{id}")]
		public IActionResult Post(int id, [FromQuery] string login, [FromBody] int[] answerid)
		{
			try
			{
				var res = service.CheckTest(id, login, answerid);

				return new OkObjectResult(new 
				{
					points = res?.summuryPoints,  
					percentOfCorrectAnswers = res?.GetPercentOfCorrectAnswers(),
					percentOfWrongAnswers = res?.GetPercentOfWrongAnswers(),
					percentOfCorrAnswFromAllCorrAns = res?.GetPercentOfCorrAnswFromAllCorrAns(),
				});
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();

		}*/

		// POST api/<OrdersController>
		/*[HttpPost]
		public IActionResult Post([FromQuery] string address, int userId)
		{
			try
			{
				service.AddItem(MapperWEB.Mapper.Map<OrderDTO>(new OrderViewModel()
				{
					UserId = userId,
					Address = address
				}
				));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message } );
			}

			return Ok();

		}*/

		// POST api/<OrdersController>
		[HttpPost]
		public IActionResult Post([FromBody] OrderViewModel order)
		{
			try
			{
				service.AddItem(MapperWEB.Mapper.Map<OrderDTO>(order));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();

		}


		[HttpPost("{id}/{goodId}")]
		public IActionResult AddGood(int id, int goodId)
		{
			try
			{
				service.AddGood(MapperWEB.Mapper.Map<OrderDTO>(new OrderViewModel()
				{
					Id= id,
					Goods = new List<GoodViewModel>()
					{
						new GoodViewModel(){Id = goodId}
					}
				}
				));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();
		}

		// PUT api/<OrdersController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromQuery] string address)
		{
			try
			{
				service.UpdateItem(MapperWEB.Mapper.Map<OrderDTO>(new OrderViewModel()
				{
					Id = id,
					Address = address,
				}
				));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();

		}

		// DELETE api/<OrdersController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var tDTO = service.GetItem(id);
				service.DeleteItem(tDTO);
			}
			catch (ValidationException e)
			{
				return new NotFoundObjectResult(new { errorText = e.Message });
			}

			return Ok();
		}

		[HttpDelete("{id}/{goodId}")]
		public IActionResult DeleteGood(int id, int goodId)
		{
			try
			{
				service.DeleteGood(MapperWEB.Mapper.Map<OrderDTO>(new OrderViewModel()
				{
					Id = id,
					Goods = new List<GoodViewModel>()
					{
						new GoodViewModel(){Id = goodId}
					}
				}
				));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();
		}
	}
}
