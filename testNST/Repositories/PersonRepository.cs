using Microsoft.EntityFrameworkCore;
using testNST.Dtos;
using testNST.Mappers;
using testNST.Models;

namespace testNST
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PSContext _context;

        private readonly PersonMapper _personMapper;

        public PersonRepository(PSContext context)
        {
            _context = context;
            _personMapper = new PersonMapper();
        }

        public async Task<Person?> Delete(long id)
        {
            var person = await Get(id);

            if (person == null)
                return person;

            foreach (var skill in person.Skills)
            {
                _context.Skill.Remove(skill);
            }
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<List<Person>> Get()
        {
            var persons = await _context.Person
                .Include(x => x.Skills)
                .ToListAsync();

            return persons;
        }

        public async Task<Person?> Get(long id)
        {
            return await _context.Person
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task PostPerson(Person person)
        {
            _context.Person.Add(person);

            await _context.SaveChangesAsync();
        }

        public async Task PutPerson(long personId, PersonDto updatedPersonDto)
        {
            var person = await Get(personId);
            if (person != null)
            {
                _personMapper.PersonDtoToPerson(updatedPersonDto, person);

                _context.Person.Update(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}