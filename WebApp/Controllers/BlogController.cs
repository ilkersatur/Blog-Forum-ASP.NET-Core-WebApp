using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BlogController : Controller
    {
        BlogDB _db;
        UserManager<Uye> _userManager;
        SignInManager<Uye> _signInManager;
        public BlogController(BlogDB db,UserManager<Uye> userManager, SignInManager<Uye> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager= signInManager;
            _db.Database.EnsureCreated();
            
        }

        public IActionResult Index()
        {
            var konular = _db.Konular;
                   
            return View(konular.ToList());
        }

        private int GetUserID() 
        {
            return int.Parse(_userManager.GetUserId(User));
        }
        public IActionResult Detay(int id)
        {

            var konu = _db.Konular.Include("Yazar").ToList().Find(x=>x.KonuID==id);


            ViewBag.kategoriler = _db.Konu_Kategori.Include("Kategori").Where(x => x.KonuID == id);




            ViewBag.yorumlar = _db.Yorumlar.Include("Uye").Where(x=>x.KonuID == id).OrderByDescending(x=>x.YorumTarih).ToList();

            return View(konu);
        }

    }
}
