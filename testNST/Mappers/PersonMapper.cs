using testNST.Dtos;
using testNST.Models;

namespace testNST.Mappers
{
    public class PersonMapper
    {
        public PersonDto PersonToPersonDto(Person person)
        {
            return new PersonDto()
            {
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills.Select(SkillToSkillDto).ToList(),
            };

        }
        public SkillDto SkillToSkillDto(Skill skill)
        {
            return new SkillDto()
            {
                Level = skill.Level,
                SkillName = skill.SkillName,
            };
        }
        public Person PersonDtoToPerson(PersonDto dto)
        {
            return new Person()
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Skills = dto.Skills.Select(SkillDtoToSkill).ToList()
            };

        }
        public Skill SkillDtoToSkill(SkillDto dto)
        {
            return new Skill()
            {
                Level = dto.Level,
                SkillName = dto.SkillName,
            };
        }
    }
}
