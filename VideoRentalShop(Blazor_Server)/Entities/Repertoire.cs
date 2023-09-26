using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VideoRentalShop_Blazor_Server_.Entities
{
    public class Repertoire
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите название!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Укажите имя режиссёра!")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Укажите описание фильма!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Укажите страну производства!")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Укажите название студии!")]
        public string? Studio { get; set; }

        [Required(ErrorMessage = "Укажите дату выхода!")]
        public int ReleaseDate { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Image {  get; set; }
        public ICollection<RepertoireGenre> RepertoireGenres { get; set; }
    }
}
