using Microsoft.AspNetCore.Mvc;
using testNST.Dtos;
using testNST.Mappers;
using testNST.Models;

namespace testNST.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        private readonly PersonMapper _personMapper;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _personMapper = new PersonMapper();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedPerson = await _personRepository.Delete(id);

            return Ok(deletedPerson);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _personRepository.Get();
            var personDtos = persons.Select(_personMapper.PersonToPersonDto).ToList();

            return Ok(personDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personRepository.Get(id);

            if (person == null)
                return NotFound();

            var personDto = _personMapper.PersonToPersonDto(person);

            return Ok(personDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] PersonDto? dto)
        {
            if (dto == null || dto.Skills.Any(skill => skill.SkillName == "" || double.IsNaN(skill.Level)))
                return BadRequest();

            var person = new Person();
            _personMapper.PersonDtoToPerson(dto, person);
            await _personRepository.PostPerson(person);

            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, [FromBody] PersonDto? updatedPersonDto)
        {
            if (updatedPersonDto == null)
                return BadRequest();

            await _personRepository.PutPerson(id, updatedPersonDto);

            return Ok(updatedPersonDto);
        }
    }
}