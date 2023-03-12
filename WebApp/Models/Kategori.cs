namespace WebApp.Models
{
    public class Kategori
    {
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }

        public ICollection<Konu_Kategori>? Konular { get; set; }
    }
}
