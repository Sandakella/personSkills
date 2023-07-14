using System.Collections.Generic;
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
        readonly IPersonRepository PersonRepository;

        public PersonMapper personMapper;

        public PersonController(IPersonRepository psRepository)
        {
            PersonRepository = psRepository;
            personMapper = new PersonMapper();
        }

        [HttpGet(Name = "GetAllItems")]
        public IEnumerable<PersonDto> Get()
        {
            var persons = PersonRepository.Get();
            return persons.Select(personMapper.PersonToPersonDto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var personItem = PersonRepository.Get(id);

            if (personItem == null)
            {
                return NotFound();
            }
            var personDto = personMapper.PersonToPersonDto(personItem);

            return new ObjectResult(personDto);
        }

        [HttpPost]
        public IActionResult PostPerson([FromBody] PersonDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            foreach (var skillchar in dto.Skills)
            {
                if (skillchar.SkillName == "" || double.IsNaN(skillchar.Level))
                {
                    return BadRequest();
                }
            }
            var person = personMapper.PersonDtoToPerson(dto);
            PersonRepository.PostPerson(person);
            return Ok(person);
        }

        [HttpPut("{personId}")]
        public IActionResult PutPerson(long personId, [FromBody] PersonDto updatedPersonDto)
        {
            if (updatedPersonDto == null)
            {
                return BadRequest();
            }
            var updatedPerson = personMapper.PersonDtoToPerson(updatedPersonDto);
            PersonRepository.PutPerson(personId, updatedPerson);
            return Ok(updatedPersonDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedPersonItem = PersonRepository.Delete(id);

            if (deletedPersonItem == null)
            {
                return BadRequest();
            }

            return new ObjectResult(deletedPersonItem);
        }
    }
}
