using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Uye:IdentityUser<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adres { get; set; }


        public ICollection<Yorum>? Yorumlar { get; set; }

        //[InverseProperty("Yorumlanan")]
        //public ICollection<Yorum>? Yorumlayanlar { get; set; }
        //[InverseProperty("Yorumlayan")]
        //public ICollection<Yorum>? Yorumlananlar { get; set; }

    }
}
