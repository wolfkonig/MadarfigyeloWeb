using System;
using System.ComponentModel.DataAnnotations;

namespace MadarfigyeloWeb.Models
{
    public class DbTest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
