using Microsoft.AspNetCore.Mvc;
using PeopleRegistry.Models;
using PeopleRegistry.Repositories.Interfaces;
using PeopleRegistry.Services;

namespace PeopleRegistry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> ExibirTodaasAsPessoas()
        {
            return await _personRepository.GetAllPeople();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> BuscarPessoaPorId(int id)
        {
            return await _personRepository.GetPersonById(id);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<Person>> BuscarPessoaPorCpf(string cpf)
        {
            return await _personRepository.GetPersonByCpf(cpf);
        }

        [HttpPost]
        public async Task<Person> InsertPerson([FromBody] Person person)
        {
            return await _personRepository.InsertPerson(person);
        }

        [HttpPut("{id}")]
        public async Task<Person> UpdatePerson(Person person, int id)
        {
            return await _personRepository.UpdatePerson(person, id);
        }

        [HttpDelete("{id}")]
        public async Task DeletePerson(int id)
        {
            await _personRepository.DeletePerson(id);
        }
    }
}