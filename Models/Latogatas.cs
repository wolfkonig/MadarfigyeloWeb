using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MadarfigyeloWeb.Models
{
    public class Latogatas
    {
        [Key]
        public int Id { get; set; }
        public Odu? Odu { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Dátum")]
        public DateTime Datum {  get; set; }
        [EnumDataType(typeof(Tevekenyseg))]
        [DisplayName("Tevékenység")]
        public Tevekenyseg Tevekenyseg { get; set; }
        [EnumDataType(typeof(Allapot))]
        [DisplayName("Állapot")]
        public Allapot Allapot { get; set; }
        [DisplayName("Faj vagy genus")]
        [Comment("HURING kód vagy magyar név")]
        public string Faj {  get; set; }
        [DisplayName("Tojás szám")]
        public int TojasSzam { get; set; }
        [DisplayName("Fióka szám")]
        public int FiokaSzam { get; set; }
        [DisplayName("Fiókák kora")]
        [Comment("Az útmutatóban leírt 5 korcsoport szerint")]
        public string? FiokakKora {  get; set; }
        [DisplayName("Megjegyzések")]
        public string? Megjegyzesek { get; set; }
    }
}
