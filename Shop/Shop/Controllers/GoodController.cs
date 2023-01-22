using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Shop.BLL.DTO;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.PL.Models;


namespace Shop.PL.Controllers
{
	[Route("api/Goods")]
	[Authorize]
	[ApiController]
	public class GoodController : ControllerBase
	{
		private IGoodService service { get; set; }

		public GoodController(IGoodService service)
		{
			this.service = service;
		}

		// GET: api/<GoodsController>
		[HttpGet]
		public IActionResult Get()
		{

			var gDTO = service.GetItems();

			return new JsonResult(MapperWEB.Mapper.Map<IEnumerable<GoodViewModel>>(gDTO));

		}

		// GET api/<GoodsController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			try
			{
				var gDTO = service.GetItem(id);
				if (gDTO == null) return new NotFoundResult();
				return new JsonResult(MapperWEB.Mapper.Map<GoodViewModel>(gDTO));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new
				{
					errorText = e.Message
				});
			}
		}

		[Route("search")]
		[HttpGet]
		public IActionResult Get([FromQuery] string name)
		{
			try
			{
				var gDTO = service.GetItems(name);
				return new JsonResult(MapperWEB.Mapper.Map<IEnumerable<GoodViewModel>>(gDTO));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new
				{
					errorText = e.Message
				});
			}
		}

		// POST api/<GoodsController>
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Post([FromBody] GoodViewModel good)
		{
			try
			{
				service.AddItem(MapperWEB.Mapper.Map<GoodDTO>(good));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();
		}

		// PUT api/<GoodsController>/5
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public IActionResult Put([FromBody] GoodViewModel good)
		{
			try
			{
				service.UpdateItem(MapperWEB.Mapper.Map<GoodDTO>(good));
			}
			catch (ValidationException e)
			{
				return new BadRequestObjectResult(new { errorText = e.Message });
			}

			return Ok();
		}

		// DELETE api/<GoodsController>/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			try
			{
				var gDTO = service.GetItem(id);
				service.DeleteItem(gDTO);
			}
			catch (ValidationException e)
			{
				return new NotFoundObjectResult(new { errorText = e.Message });
			}

			return Ok();
		}
	}
}
