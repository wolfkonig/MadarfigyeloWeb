using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadarfigyeloWeb.Models
{
    public class Odu
    {
        [Key]
        public int Id { get; set; }
        public string OduAzonosito { get; set; }
        public Odutelep? Odutelep { get; set; }
        public string OduTipus {  get; set; }
        public int BejaratiNyilasMm {  get; set; }
        public Decimal GpsLatitude { get; set; }
        public Decimal GpsLongitude { get; set; }
        public string Elohelykod {  get; set; }
        public string MireVanHelyezve { get; set; }
        public string FelhelyezesModja { get; set; }
        public string OduTajolasa { get; set; }
        public string OdutTartoNovenyfaj { get; set; }
        public string MagassagMeter {  get; set; }
        public ICollection<Latogatas> Latogatas { get; set; }
     }
}
