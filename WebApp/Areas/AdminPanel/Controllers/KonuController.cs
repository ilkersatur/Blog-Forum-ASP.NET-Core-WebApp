using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL;
using WebApp.Models;

namespace WebApp.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class KonuController : Controller
    {
        private readonly BlogDB _context;
        UserManager<Uye> _userManager;

        public KonuController(BlogDB context, UserManager<Uye> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var kutuphaneDB = _context.Konular.Include(k => k.Yazar);
            return View(await kutuphaneDB.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Konular == null)
            {
                return NotFound();
            }

            var konu = await _context.Konular
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KonuID == id);
            if (konu == null)
            {
                return NotFound();
            }

            return View(konu);
        }


        public IActionResult Create()
        {
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAdi");

            ViewBag.Kategoriler = _context.Kategoriler.ToList();
            return View();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KonuID,KonuAdi,YazarID,Yazi,KapakResmi,EklendigiTarih")] Konu konu,IFormFile Kapak_Resmi,string[] kategori)
        {
            if (ModelState.IsValid && konu.YazarID != 0)
            {
                FileStream fs = new FileStream("wwwroot/Kapakresimleri/" + Kapak_Resmi.FileName, FileMode.Create);
                Kapak_Resmi.CopyTo(fs);
                fs.Close();
                konu.KapakResmi = Kapak_Resmi.FileName;
                konu.EklendigiTarih = DateTime.Now;
                _context.Add(konu);

                await _context.SaveChangesAsync();

                //Tum kategorileri ekle
                foreach (string str in kategori)
                {
                    _context.Konu_Kategori.Add(new Konu_Kategori { KategoriID=int.Parse(str), KonuID =konu.KonuID });
                }

                _context.SaveChanges();              
                return RedirectToAction(nameof(Index));
            }
            if (konu.YazarID == 0)
                ModelState.AddModelError("YazarID", "Lütfen yazar seciniz...");
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarAdi", konu.YazarID);
            return View(konu);

            //string strKat = "";
            //foreach(string item in kategori)
            //    strKat += item + " ";
            //return Content(strKat);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Konular == null)
            {
                return NotFound();
            }

            var konu = await _context.Konular.FindAsync(id);
            if (konu == null)
            {
                return NotFound();
            }
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarID", konu.YazarID);
            return View(konu);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KonuID,KonuAdi,YazarID,Yazi,KapakResmi,EklendigiTarih")] Konu konu)
        {
            if (id != konu.KonuID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonuExists(konu.KonuID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["YazarID"] = new SelectList(_context.Yazarlar, "YazarID", "YazarID", konu.YazarID);
            return View(konu);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Konular == null)
            {
                return NotFound();
            }

            var konu = await _context.Konular
                .Include(k => k.Yazar)
                .FirstOrDefaultAsync(m => m.KonuID == id);
            if (konu == null)
            {
                return NotFound();
            }

            return View(konu);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Konular == null)
            {
                return Problem("Entity set 'BlogDB.Konular'  is null.");
            }
            var konu = await _context.Konular.FindAsync(id);
            if (konu != null)
            {
                _context.Konular.Remove(konu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KonuExists(int id)
        {
          return _context.Konular.Any(e => e.KonuID == id);
        }

        public IActionResult KategoriEkle(int? id)
        {
            //Daha once eklenmiş kategorileri gormek için
            ViewBag.Kategoriler = _context.Konu_Kategori.Include("Kategori").Where(x => x.KonuID == id).ToList();
                        
            ViewBag.KonuID = new SelectList(_context.Konular.Where(x => x.KonuID == id).ToList(), "KonuID", "KonuAdi");
            ViewBag.KategoriID = new SelectList(_context.Kategoriler,"KategoriID","KategoriAdi");

            return View();
        }
        [HttpPost]
        public IActionResult KategoriEkle(Konu_Kategori konuKategori)
        {
            _context.Konu_Kategori.Add(konuKategori);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
