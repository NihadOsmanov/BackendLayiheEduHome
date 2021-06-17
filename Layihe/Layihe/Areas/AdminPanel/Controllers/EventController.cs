using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
using Layihe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = RoleConstant.Admin)]
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Events.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page || page <= 0)
            {
                return NotFound();
            }

            var events = _dbContext.Events.Where(x => x.IsDeleted == false).OrderByDescending(y => y.Id).Include(x => x.EventDetail).Include(x => x.EventSpikers).ThenInclude(x => x.Spiker)
                                                               .Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(events);
        }

        #region Create
        public IActionResult Create()
        {
            ViewBag.Speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event evnt, List<int?> SpeakersId, List<int?> CategoriesId)
        {
            var speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Speakers = speakers;
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (SpeakersId.Count == 0)
            {
                ModelState.AddModelError("", "Please select Spiker");
                return View();
            }

            foreach (var speaker in SpeakersId)
            {
                if(speakers.All(x => x.Id != (int)speaker))
                {
                    return BadRequest();
                }
            }

            if (CategoriesId.Count == 0)
            {
                ModelState.AddModelError("", "Please select Category");
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

            if(evnt.StartingTime > evnt.EndTime)
            {
                ModelState.AddModelError("", "Start date cannot be later than end date");
                return View();
            }

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

            var eventCategories = new List<EventCategory>();

            foreach (var ec in CategoriesId)
            {
                var eventCategory = new EventCategory();
                eventCategory.EventId = evnt.Id;
                eventCategory.CategoryId = (int)ec;
                eventCategories.Add(eventCategory);
            }

            evnt.EventSpikers = eventSpikers;
            evnt.EventCategories = eventCategories;
            await _dbContext.EventDetails.AddAsync(evnt.EventDetail);
            await _dbContext.SaveChangesAsync();

            string href = "https://localhost:44364/Event/Detail/" + evnt.Id;
            string subject = "New Event Created";
            string msgBody = $"<a href={href}>New Event Created for see you click here</a> ";

            foreach (var item in (await _dbContext.Subscribers.ToListAsync()))
            {
                await Helper.SendMessage(subject, msgBody, item.Email);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Update
        public IActionResult Update(int? id)
        {
            ViewBag.Speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(x => x.EventSpikers).ThenInclude(x => x.Spiker)
                                                .Include(y => y.EventCategories).ThenInclude(y => y.Category).FirstOrDefault(x => x.Id == id);

            if (dbEvent == null)
                return NotFound();

            return View(dbEvent);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Event evnt, List<int?> SpeakersId, List<int?> CategoriesId)
        {
            var speakers = _dbContext.Spikers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Speakers = speakers;
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (SpeakersId == null)
            {
                ModelState.AddModelError("", "Please select Speaker");
                return View();
            }

            if (CategoriesId == null)
            {
                ModelState.AddModelError("", "Please select Category");
                return View();
            }

            foreach (var speaker in SpeakersId)
            {
                if (speakers.All(x => x.Id != (int)speaker))
                {
                    return BadRequest();
                }
            }

            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(x => x.EventSpikers).ThenInclude(x => x.Spiker)
                                                      .Include(y => y.EventCategories).ThenInclude(y => y.Category).FirstOrDefault(x => x.Id == id);

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

            if (evnt.StartingTime > evnt.EndTime)
            {
                ModelState.AddModelError("", "Start date cannot be later than end date");
                return View();
            }

            var eventSpikers = new List<EventSpiker>();

            foreach (int es in SpeakersId)
            {
                var eventSpiker = new EventSpiker();
                eventSpiker.EventId = dbEvent.Id;
                eventSpiker.SpikerId = es;
                eventSpikers.Add(eventSpiker);
            }

            var eventCategories = new List<EventCategory>();

            foreach (var ec in CategoriesId)
            {
                var eventCategory = new EventCategory();
                eventCategory.EventId = evnt.Id;
                eventCategory.CategoryId = (int)ec;
                eventCategories.Add(eventCategory);
            }

            dbEvent.Name = evnt.Name;
            dbEvent.Adress = evnt.Adress;
            dbEvent.StartingTime = evnt.StartingTime;
            dbEvent.EndTime = evnt.EndTime;
            dbEvent.EventDetail.Decscription = evnt.EventDetail.Decscription;
            dbEvent.EventSpikers = eventSpikers;
            dbEvent.EventCategories = eventCategories;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDeleted == false).Include(x => x.EventDetail).Include(y => y.EventSpikers)
                                 .ThenInclude(y => y.Spiker).Include(y => y.EventCategories).ThenInclude(y => y.Category).FirstOrDefault(z => z.Id == id);

            if (dbEvent == null)
                return NotFound();

            return View(dbEvent);
        }

        #endregion

        #region Delete
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
        #endregion

    }
}
