using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Konu_Kategori
    {
        [Key]
        public int KKID { get; set; }

        public int KonuID { get; set; }
        public int KategoriID { get; set; }

        public ICollection<Konu_Kategori>? Kategoriler { get; set; }
        public ICollection<Yorum>? Yorumlar { get; set; }

        public Konu? Konu { get; set; }
        public Kategori? Kategori { get; set; }
    }
}
