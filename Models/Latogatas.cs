using System.ComponentModel.DataAnnotations;

namespace MadarfigyeloWeb.Models
{
    public class Latogatas
    {
        [Key]
        public int Id { get; set; }
        public Odu Odu { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public int Datum {  get; set; }
        public Tevekenyseg Tevekenyseg { get; set; }
        public Allapot Allapot { get; set; }
        public string Faj {  get; set; }
        public int TojasSzam { get; set; }
        public int FiokaSzam { get; set; }
        public string FiokakKora {  get; set; } 
        public string Megjegyzesek { get; set; }
    }
}
