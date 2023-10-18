using PeopleRegistry.Models;

namespace PeopleRegistry.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllPeople();

        Task<Person> GetPersonById(int id);

        Task<Person> GetPersonByCpf(string cpf);

        Task<Person> InsertPerson(Person person);

        Task<Person> UpdatePerson(Person person, int id);

        Task DeletePerson(int id);
    }
}
