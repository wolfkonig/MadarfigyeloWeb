using System.ComponentModel.DataAnnotations;

namespace MadarfigyeloWeb.Models
{
    public class Odutelep
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Azonosito { get; set; }
        public string Telepules { get; set; }
        public string TeruletNev { get; set; }
        public string UtmNegyzetKod { get; set; }
        public string KezeloSzervezetNev { get; set; }
        public string FelelosSzemelyNev { get; set; }
        public string FelelosSzemelyCim { get; set; }
        public string FelelosSzemelyTelefonszam { get; set; }
        public string FelelosSzemelyEmail { get; set; }
        public string Megjegyzes { get; set; }

        public ICollection<Odu> Oduk {  get; set; }
    }
}
