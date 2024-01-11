using FarmManager.Server;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAll();
    Animal? Get(string name);
    void Add(Animal animal);
    bool Remove(string name);
}
