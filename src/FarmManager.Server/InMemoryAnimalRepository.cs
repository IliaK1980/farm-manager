using System.Collections.Concurrent;

namespace FarmManager.Server
{
    public class InMemoryAnimalRepository : IAnimalRepository
    {
        private readonly ConcurrentDictionary<string, Animal> _animals = new();

        public IEnumerable<Animal> GetAll()
        {
            return _animals.Values;
        }

        public Animal? Get(string name)
        {
            _animals.TryGetValue(name, out var animal);
            return animal;
        }

        public void Add(Animal animal)
        {
            _animals.TryAdd(animal.Name, animal);
        }

        public bool Remove(string name)
        {
            return _animals.TryRemove(name, out var _);
        }
    }
}