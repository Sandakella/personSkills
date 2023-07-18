using System.ComponentModel.DataAnnotations;

namespace testNST.Dtos
{
    public class PersonDto
    {
        [Required(ErrorMessage = "Введите отображаемое имя")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Неправильно введены навыки")]
        public virtual List<SkillDto> Skills { get; set; }
    }
}