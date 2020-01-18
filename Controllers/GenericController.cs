using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RouteamTest.Models;
using RouteamTest.Models.Interfaces;

namespace RouteamTest.Controllers
{
    public abstract class GenericController<T> : Controller
        where T : class, IBaseModel
    {
        protected AppDbContext db;
        public GenericController(AppDbContext context)
        {
            db = context;
        }
        
        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var model = await db.Set<T>().AsNoTracking().ToListAsync();
            return View(model);
        }
        [HttpGet]
        public virtual IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public virtual async Task<IActionResult> Create(T model)
        {
            try
            {
                ValidateModel();
                db.Set<T>().Add(model);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Невозможно добавить.");
                return View(model);
            }
            
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public virtual IActionResult Edit(int id)
        {
            try
            {
                ValidateModel();
                var model = db.Set<T>().AsNoTracking().First(obj=>obj.Id==id);
                return View(model);
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Невозможно сохранить изменения.");
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public virtual async Task<IActionResult> Edit(T model)
        {
            try
            {
                ValidateModel();
                db.Entry(model).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Невозможно сохранить изменения.");
            }
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                var model = db.Set<T>().Find(id);
                db.Set<T>().Remove(model);
                db.SaveChanges();
            }
            catch (DataException)
            {
                
            }
            return RedirectToAction(nameof(List));
        }
        protected T GetById(int id)
        {
            return db.Set<T>().AsNoTracking().First(obj => obj.Id == id);
        }
        async public Task<List<SelectListItem>> GetDataSelectListItems<TEntity>() where TEntity : class
        {
            DbSet<TEntity> dbSet = db.Set<TEntity>();

            return await (dbSet.AsNoTracking().Select(a => new SelectListItem()
            {
                Value = ((IBaseModel)a).Id.ToString(),
                Text = ((IBaseModel)a).Name
            })).ToListAsync();
        }
        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Ошибка в модели.");
            }
        }
       

        
    }
}