using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvTest.Models;

namespace EvTest.Controllers
{
    [Produces("application/json")]
    [Route("Person")]
    public class PersonController : Controller
    {
        private readonly PersonContext db;

        public PersonController(PersonContext context)
        {
            db = context;
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<PersonVM> pvm = db.Person.Select(p => new PersonVM
            {
                ID = p.ID,
                Name = p.Name,
                Phone = p.Phone,
                Age = p.Age
            });
            return Json(await db.Person.ToListAsync());
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonVM pvm)
        {
            if (ModelState.IsValid)
            {
                Person ps = new Person();
                ps.ID = pvm.ID;
                ps.Name = pvm.Name;
                ps.Phone = pvm.Name;
                ps.Age = pvm.Age;
                db.Person.Add(ps);
                await db.SaveChangesAsync();
            }
            return Json(new {Message= "Create sucessfully" });
        }
        [Route("Update/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PersonVM pvm = await db.Person.Select(p => new PersonVM
            {
                ID = p.ID,
                Name = p.Name,
                Phone = p.Phone,
                Age = p.Age
            }).Where(i => i.ID == id).FirstOrDefaultAsync();
            return Json(pvm);
        }
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Person pvm)
        {
            if (ModelState.IsValid)
            {
                Person ps = new Person();
                ps.ID = pvm.ID;
                ps.Name = pvm.Name;
                ps.Phone = pvm.Name;
                ps.Age = pvm.Age;
                db.Entry(ps).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Json(new { Message = "Create sucessfully" });
        }
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] Person ps)
        {
            var person = await db.Person.FindAsync(ps.ID);
            db.Person.Remove(person);
            await db.SaveChangesAsync();
            return Json(new { message = "Delete successful" });
        }
    }
}
