using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiVersioningError.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class PetsController : ControllerBase
	{
		public static readonly Dictionary<int, Pet> _petStorage = new Dictionary<int, Pet>();

		public enum PetType
		{
			Dog,
			Cat
		}

		// GET: api/<PetController>
		[HttpGet]
		public IEnumerable<Pet> Get()
		{
			return _petStorage.Values;
		}

		// GET api/<PetController>/5
		[HttpGet("{id}")]
		public Pet Get(int id)
		{
			return _petStorage[id];
		}

		// POST api/<PetController>
		[HttpPost]
		public ActionResult Post([FromBody] Pet pet, ApiVersion version)
		{
			if (pet.Id is null)
			{
				lock (_petStorage)
				{
					if (_petStorage.Count == 0)
					{
						pet.Id = 1;
					}
					else
					{
						pet.Id = _petStorage.Keys.Max() + 1;
					}
				}
			}

			_petStorage.Add(pet.Id.Value, pet);

			return CreatedAtAction(nameof(Get), new { pet.Id, version = version.ToString() }, pet);
		}

		// PUT api/<PetController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Pet pet)
		{
			_petStorage[id] = pet;
		}

		// DELETE api/<PetController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			_petStorage.Remove(id);
		}

		public class Pet
		{
			public int? Id { get; set; }

			[Required]
			public string Name { get; set; }

			[Required]
			public PetType? Type { get; set; }
		}
	}
}
