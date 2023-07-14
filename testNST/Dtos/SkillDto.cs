using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testNST.Dtos
{
    public class SkillDto
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Введите навык")]
        public string SkillName { get; set; }

        [Required(ErrorMessage = "Введите уровень навыка")]
        [Range(1, 10, ErrorMessage = "Уровень навыка должен находиться в диапазоне от 1 до 10")]
        public int Level { get; set; }
    }
}
