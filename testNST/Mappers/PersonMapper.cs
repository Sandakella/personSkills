using testNST.Dtos;
using testNST.Models;

namespace testNST.Mappers
{
    public class PersonMapper
    {
        public static void PersonDtoToPerson(PersonDto dto, Person person)
        {
            person.Name = dto.Name;
            person.DisplayName = dto.Name;
            person.Skills = dto.Skills.Select(SkillDtoToSkill).ToList();
        }

        public static PersonDto PersonToPersonDto(Person person)
        {
            return new PersonDto
            {
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills.Select(SkillToSkillDto).ToList(),
            };
        }

        private static Skill SkillDtoToSkill(SkillDto dto)
        {
            return new Skill
            {
                Level = dto.Level,
                SkillName = dto.SkillName,
            };
        }

        private static SkillDto SkillToSkillDto(Skill skill)
        {
            return new SkillDto
            {
                Level = skill.Level,
                SkillName = skill.SkillName,
            };
        }
    }
}