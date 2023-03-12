namespace WebApp.Models
{
    public class Konu
    {
        public  int KonuID { get; set; }
        public string KonuAdi { get; set; }
        public int YazarID { get; set; }
        public string Yazi { get; set; }
        public string KapakResmi { get; set; }
        public DateTime EklendigiTarih { get; set; }

        public ICollection<Konu_Kategori>? Kategoriler { get; set; }

        public ICollection<Yorum>? Yorumlar { get; set; }

        public Yazar? Yazar { get; set; }


    }
}
