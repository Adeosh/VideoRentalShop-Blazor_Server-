using System.ComponentModel.DataAnnotations;

namespace VideoRentalShop_Blazor_Server_.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Требуется описание!")]
        public string Description { get; set; }
        public ICollection<RepertoireGenre> RepertoireGenres { get; set; }
    }
}
