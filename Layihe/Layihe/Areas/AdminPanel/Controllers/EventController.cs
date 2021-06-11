using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Events.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page)
            {
                return NotFound();
            }

            var events = _dbContext.Events.Where(x => x.IsDeleted == false).OrderByDescending(y => y.Id).Include(x => x.EventDetail).Include(x => x.EventSpikers).ThenInclude(x => x.Spiker)
                                                               .Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(events);
        }
        public IActionResult Create()
        {
            ViewBag.Speakers = _dbContext.Spikers.Where(x => x.IsDeleted ==false).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Event evnt,List<int?> SpeakersId)
        {
            ViewBag.Speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if(SpeakersId == null)
            {
                ModelState.AddModelError("", "Please select");
                return View();
            }

            if (evnt.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select Photo");
                return View();
            }

            if (!evnt.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Not the image you uploaded");
                return View();
            }

            if (!evnt.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                return View();
            }

            var fileName = await FileUtil.GenerateFileAsync(Constants.EventImageFolderPath, evnt.Photo);
            evnt.Image = fileName;
            evnt.IsDeleted = false;

            var eventSpikers = new List<EventSpiker>();
           
            await _dbContext.Events.AddAsync(evnt);
            evnt.EventDetail.EventId = evnt.Id;
            foreach (int es in SpeakersId)
            {
                var eventSpiker = new EventSpiker();
                eventSpiker.EventId = evnt.Id;
                eventSpiker.SpikerId = es;
                eventSpikers.Add(eventSpiker);
            }
            evnt.EventSpikers = eventSpikers;
            await _dbContext.EventDetails.AddAsync(evnt.EventDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            ViewBag.Speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();

            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(x => x.EventSpikers).ThenInclude(x => x.Spiker)
                                                                                                    .FirstOrDefault(x => x.Id == id);

            if (dbEvent == null)
                return NotFound();

            return View(dbEvent);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Event evnt, List<int?> SpeakersId)
        {
            ViewBag.Speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (SpeakersId == null)
            {
                ModelState.AddModelError("", "Please select");
                return View();
            }

            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(x => x.EventSpikers).ThenInclude(x => x.Spiker)
                                                                                                    .FirstOrDefault(x => x.Id == id);

            if (dbEvent == null)
                return NotFound();

            if (evnt.Photo != null)
            {
                if (!evnt.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Not the image you uploaded");
                    return View();
                }

                if (!evnt.Photo.IsSizeAllowed(1024))
                {
                    ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                    return View();
                }

                var fileName = await FileUtil.GenerateFileAsync(Constants.EventImageFolderPath, evnt.Photo);
                evnt.Image = fileName;
                evnt.IsDeleted = false;
            }

            var eventSpikers = new List<EventSpiker>();

            foreach (int es in SpeakersId)
            {
                var eventSpiker = new EventSpiker();
                eventSpiker.EventId = dbEvent.Id;
                eventSpiker.SpikerId = es;
                eventSpikers.Add(eventSpiker);
            }

            dbEvent.Name = evnt.Name;
            dbEvent.Adress = evnt.Adress;
            dbEvent.StartingTime = evnt.StartingTime;
            dbEvent.EndTime = evnt.EndTime;
            dbEvent.EventDetail.Decscription = evnt.EventDetail.Decscription;
            dbEvent.EventSpikers = eventSpikers;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(y => y.EventSpikers)
                                                                            .ThenInclude(y => y.Spiker).FirstOrDefault(z => z.Id == id);

            if (dbEvent == null)
                return NotFound();

            return View(dbEvent);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(y => y.EventSpikers)
                                                                            .ThenInclude(y => y.Spiker).FirstOrDefault(z => z.Id == id);

            if (dbEvent == null)
                return NotFound();

            return View(dbEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEvent(int? id)
        {
            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(y => y.EventSpikers)
                                                                            .ThenInclude(y => y.Spiker).FirstOrDefault(z => z.Id == id);

            if (dbEvent == null)
                return NotFound();

            dbEvent.IsDeleted = true;
            dbEvent.EventDetail.IsDelete = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
