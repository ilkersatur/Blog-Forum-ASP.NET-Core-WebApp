using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebApp.DAL;
using WebApp.Models;

namespace WebApp.Areas.UyePanel.Controllers
{
    [Area("UyePanel")]
    public class UyeController : Controller
    {
        UserManager<Uye> _userManager;
        BlogDB _db;
        public UyeController(UserManager<Uye> userManager, BlogDB db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YorumEkle(int konuID, string mesaj)
        {
            Yorum yorum = new Yorum();
            yorum.Mesaj = mesaj;
            yorum.KonuID = konuID;
            yorum.UyeID = GetUserID();
            yorum.YorumTarih = DateTime.Now;

            _db.Yorumlar.Add(yorum);
            _db.SaveChanges();
            return Redirect("~/Blog/Detay/" + konuID);
        }

        private int GetUserID()
        {
            return int.Parse(_userManager.GetUserId(User));
        }

    }
}
