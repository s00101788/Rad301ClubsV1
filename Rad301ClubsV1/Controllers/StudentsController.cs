using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rad301ClubsV1.Models.ClubModel;
using PagedList;

namespace Rad301ClubsV1.Controllers
{
    // Roles Admin or ClubAdmin
    [Authorize(Roles = "Admin,ClubAdmin")]
    // Roles must include both
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "ClubAdmin")]
    public class StudentsController : Controller
    {
        private ClubContext db = new ClubContext();

        // GET: Students
        public async Task<ActionResult> Index(string sort, string search, int? page
            /*string Firstname = null, string Surname = null*/)
        {
            //ViewBag.VFname = Firstname;
            //ViewBag.VSname = Surname;
            //return View(await db.Students
            //    .Where(s => (Surname == null && Firstname == null) 
            //    ||  s.Fname.StartsWith(Firstname) 
            //        && s.Sname.StartsWith(Surname))
            //    .ToListAsync());
            ViewBag.SnameSort = String.IsNullOrEmpty(sort) ? "sname_desc" : string.Empty;
            ViewBag.FnameSort = sort == "fname" ? "fname_desc" : "fname";

            ViewBag.CurrentSearch = search;
            IQueryable<Student> students = db.Students;

            if (!String.IsNullOrEmpty(search))
                students =
                    students.Where(
                        s => s.Fname.StartsWith(search) || s.Sname.StartsWith(search));
            switch (sort)
            {

                case "sname_desc":
                    students = students.OrderByDescending(s => s.Sname).ThenBy(s => s.Fname);
                    break;
                case "fname":
                    students = students.OrderBy(s => s.Fname).ThenBy(s => s.Sname);
                    break;
                case "fname_desc":
                    students = students.OrderByDescending(s => s.Fname).ThenBy(s => s.Sname);
                    break;
                default:
                    students = students.OrderBy(s => s.Sname).ThenBy(s => s.Fname);
                    break;
            }

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentID,Fname,Sname")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentID,Fname,Sname")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
