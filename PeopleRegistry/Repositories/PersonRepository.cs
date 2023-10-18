using Microsoft.EntityFrameworkCore;
using PeopleRegistry.Data;
using PeopleRegistry.Models;
using PeopleRegistry.Repositories.Interfaces;
using PeopleRegistry.Services;

namespace PeopleRegistry.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleRegistryDBContext _dBContext;
        private readonly CpfService _validateCpf;

        public PersonRepository(PeopleRegistryDBContext dBContext, CpfService validateCpf)
        {
            _dBContext = dBContext;
            _validateCpf = validateCpf;
        }

        public async Task<List<Person>> GetAllPeople()
        {
            return await _dBContext.Pessoas.ToListAsync();
        }

        public async Task<Person> GetPersonById(int id)
        {
            var searchPersonById = await _dBContext.Pessoas.FirstOrDefaultAsync(x => x.Id == id);
            if (searchPersonById == null)
                throw new Exception($"Pessoa com o id {searchPersonById} não foi encontrada");
            return searchPersonById;  
        }

        public async Task<Person> GetPersonByCpf(string cpf)
        {
            var searchPersonByCpf = await _dBContext.Pessoas.FirstOrDefaultAsync(x => x.Cpf == cpf);
            if (searchPersonByCpf == null)
                throw new Exception($"Pessoa com o cpf {searchPersonByCpf} não foi encontrada");
            return searchPersonByCpf;
        }

        public async Task<Person> InsertPerson(Person person)
        {
            if (!_validateCpf.IsValidCpf(person.Cpf))
            {
                throw new Exception($"Cpf inválido");
            }
            if (!_validateCpf.VerificaCpfBanco(person.Cpf))
            {
                throw new Exception($"Cpf inválido");
            }
            await _dBContext.Pessoas.AddAsync(person);
            await _dBContext.SaveChangesAsync();
            return person;       
        }

        public async Task<Person> UpdatePerson(Person person, int id)
        {
            var searchPersonById = await GetPersonById(id);
            if (searchPersonById == null)
                throw new Exception($"Pessoa com o id {searchPersonById} não foi encontrada");
            searchPersonById.Name = person.Name;
            searchPersonById.Email = person.Email;
            searchPersonById.Cpf = person.Cpf;

            _dBContext.Update(searchPersonById);
            await _dBContext.SaveChangesAsync();
            return searchPersonById;
        }

        public async Task DeletePerson(int id)
        {
            var deletePersonById = await GetPersonById(id);
            if (deletePersonById == null)
                throw new Exception($"Pessoa com o id {deletePersonById} não foi encontrada");
            _dBContext.Remove(deletePersonById);
            await _dBContext.SaveChangesAsync();
        }
    }
}