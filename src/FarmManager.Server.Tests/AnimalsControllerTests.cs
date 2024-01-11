using FarmManager.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FarmManager.Server.Tests
{
    public class AnimalsControllerTests
    {
        [Fact]
        public void GetAnimals_ReturnsAllAnimals()
        {
            // Arrange
            var mockRepo = new Mock<IAnimalRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetTestAnimals());
            var controller = new AnimalsController(mockRepo.Object);

            // Act
            var result = controller.GetAnimals();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Animal>>(actionResult.Value);
            var animals = returnValue;
            Assert.Equal(2, animals.Count);
        }

        [Fact]
        public void AddAnimal_ReturnsOk_WhenAnimalIsAdded()
        {
            // Arrange
            var mockRepo = new Mock<IAnimalRepository>();
            var controller = new AnimalsController(mockRepo.Object);
            var newAnimal = new Animal { Name = "Goat" };

            // Act
            var result = controller.AddAnimal(newAnimal);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void RemoveAnimal_ReturnsOk_WhenAnimalExists()
        {
            // Arrange
            var mockRepo = new Mock<IAnimalRepository>();
            mockRepo.Setup(repo => repo.Remove(It.IsAny<string>())).Returns(true);
            mockRepo.Setup(repo => repo.Get(It.IsAny<string>())).Returns(new Animal { Name = "ExistingAnimal" });
            var controller = new AnimalsController(mockRepo.Object);

            // Act
            var result = controller.RemoveAnimal("ExistingAnimal");

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void RemoveAnimal_ReturnsNotFound_WhenAnimalDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IAnimalRepository>();
            mockRepo.Setup(repo => repo.Remove(It.IsAny<string>())).Returns(false);
            var controller = new AnimalsController(mockRepo.Object);

            // Act
            var result = controller.RemoveAnimal("NonExistingAnimal");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private List<Animal> GetTestAnimals()
        {
            var animals = new List<Animal>
        {
            new Animal { Name = "Cow" },
            new Animal { Name = "Chicken" }
        };
            return animals;
        }
    }

}
