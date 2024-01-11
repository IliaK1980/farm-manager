using Microsoft.AspNetCore.Mvc;

namespace FarmManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalsController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public IActionResult GetAnimals()
        {
            var animals = _animalRepository.GetAll();
            return Ok(animals);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            if (string.IsNullOrEmpty(animal.Name))
            {
                return BadRequest("Animal name should not be null or empty");
            }

            var existingAnimal = _animalRepository.Get(animal.Name);
            if (existingAnimal != null)
            {
                return BadRequest("Animal already exists.");
            }

            _animalRepository.Add(animal);

            return Created();
        }

        [HttpDelete("{name}")]
        public IActionResult RemoveAnimal(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Animal name should not be null or empty");
            }

            var existingAnimal = _animalRepository.Get(name);
            if (existingAnimal == null)
            {
                return NotFound();
            }

            _animalRepository.Remove(name);

            return Ok();
        }
    }
}
