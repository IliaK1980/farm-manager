using FarmManager.Server;

namespace FarmManager.Server.Tests
{
    public class InMemoryAnimalRepositoryTests
    {
        [Fact]
        public void Add_ShouldAddAnimal()
        {
            // Arrange
            var repository = new InMemoryAnimalRepository();
            var animal = new Animal { Name = "Cow" };

            // Act
            repository.Add(animal);

            // Assert
            Assert.Equal(animal, repository.Get("Cow"));
        }

        [Fact]
        public void Remove_ShouldRemoveAnimal()
        {
            // Arrange
            var repository = new InMemoryAnimalRepository();
            var animal = new Animal { Name = "Sheep" };
            repository.Add(animal);

            // Act
            var result = repository.Remove("Sheep");

            // Assert
            Assert.True(result);
            Assert.Null(repository.Get("Sheep"));
        }

        [Fact]
        public void GetAll_ShouldReturnAllAnimals()
        {
            // Arrange
            var repository = new InMemoryAnimalRepository();
            repository.Add(new Animal { Name = "Chicken" });
            repository.Add(new Animal { Name = "Duck" });

            // Act
            var animals = repository.GetAll();

            // Assert
            Assert.Equal(2, animals.Count());
            Assert.Contains(animals, a => a.Name == "Chicken");
            Assert.Contains(animals, a => a.Name == "Duck");
        }

        [Fact]
        public void Get_ShouldReturnAnimal()
        {
            // Arrange
            var repository = new InMemoryAnimalRepository();
            var animal = new Animal { Name = "Pig" };
            repository.Add(animal);

            // Act
            var result = repository.Get("Pig");

            // Assert
            Assert.Equal(animal, result);
        }
    }
}