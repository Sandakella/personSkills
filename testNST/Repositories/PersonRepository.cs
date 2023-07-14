using Microsoft.EntityFrameworkCore;
using testNST.Models;
using System.Collections.Generic;
using System.Linq;

namespace testNST
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PSContext Context;
        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> persons = Context.Person.Include(q => q.Skills);
            return persons;
        }
        public Person Get(long id)
        {
            return Context.Person.Include(q => q.Skills).FirstOrDefault(q => q.Id == id);
        }
        public PersonRepository(PSContext context)
        {
            Context = context;
        }
        public void PostPerson(Person item)
        {
            Context.Person.Add(item);
            foreach (var skill in item.Skills)
            {
                Context.Skill.Add(skill);
            }
            Context.SaveChanges();
        }

        public void PutPerson(long personId, Person updatedPerson)
        {
            Person currentItem = Context.Person.Include(q => q.Skills).FirstOrDefault(w => w.Id == personId);
            currentItem.Name = updatedPerson.Name;
            currentItem.DisplayName = updatedPerson.DisplayName;
            foreach (var skill in currentItem.Skills)
            {
                Skill updatedSkill = Context.Skill.FirstOrDefault(q => q.SkillName == skill.SkillName && q.PersonId == skill.PersonId);
                if (updatedSkill == null)
                {
                    Skill newSkill = new Skill() { PersonId = skill.PersonId, SkillName = skill.SkillName, Level = skill.Level };
                }
                else
                {
                    updatedSkill.Level = skill.Level;
                }
            }
            Context.Person.Update(currentItem);
            Context.SaveChanges();
        }

        public Person Delete(long id)
        {
            Person person = Get(id);

            if (person != null)
            {
                Context.Person.Remove(person);
                Context.SaveChanges();
            }

            return person;
        }
    }
}
