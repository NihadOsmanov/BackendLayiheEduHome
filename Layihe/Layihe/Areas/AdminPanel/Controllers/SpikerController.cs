using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SpikerController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SpikerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Spikers.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < ViewBag.Page)
            {
                return BadRequest();
            }

            var spikers = _dbContext.Spikers.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(spikers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spiker spiker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (spiker.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select Photo");
                return View();
            }

            if (!spiker.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Not the image you uploaded");
                return View();
            }

            if (!spiker.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                return View();
            }

            var fileName = await FileUtil.GenerateFileAsync(Constants.EventImageFolderPath, spiker.Photo);

            spiker.Image = fileName;
            spiker.IsDeleted = false;

            await _dbContext.Spikers.AddAsync(spiker);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
                return NotFound();

            var spikers = _dbContext.Spikers.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);

            if (spikers == null)
                return NotFound();

            return View(spikers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Spiker spiker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbSpiker = _dbContext.Spikers.Where(x => x.IsDeleted == false).FirstOrDefault(y => y.Id == id);

            if (dbSpiker == null)
                return NotFound();

            if (spiker.Photo != null)
            {
                if (!spiker.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Not the image you uploaded");
                    return View();
                }

                if (!spiker.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                    return View();
                }

                var path = Path.Combine(Constants.EventImageFolderPath, dbSpiker.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var fileName = await FileUtil.GenerateFileAsync(Constants.EventImageFolderPath, spiker.Photo);
                dbSpiker.Image = fileName;
            }

            dbSpiker.FullName = spiker.FullName;
            dbSpiker.Profession = spiker.Profession;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var dbSpikers = _dbContext.Spikers.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);

            if (dbSpikers == null)
                return NotFound();

            return View(dbSpikers);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dbSpikers = _dbContext.Spikers.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == id);

            if (dbSpikers == null)
                return NotFound();

            return View(dbSpikers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSpikert(int? id)
        {
            if (id == null)
                return NotFound();

            var dbSpiker = _dbContext.Spikers.Where(x => x.IsDeleted == false).FirstOrDefault(z => z.Id == id);

            if (dbSpiker == null)
                return NotFound();

            dbSpiker.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
