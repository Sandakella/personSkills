using System.ComponentModel.DataAnnotations;

namespace testNST.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Введите отображаемое имя")]
        public string DisplayName { get; set; }

        public long Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Неправильно введены навыки")]
        public virtual List<Skill> Skills { get; set; }
    }
}