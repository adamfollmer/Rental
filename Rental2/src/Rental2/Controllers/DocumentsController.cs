using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Rental2.Models;

namespace Rental2.Controllers
{
    public class DocumentsController : Controller
    {
        private RentalContext _context;

        public DocumentsController(RentalContext context)
        {
            _context = context;    
        }

        // GET: Documents
        public IActionResult Index()
        {
            return View(_context.Documents.ToList());
        }

        // GET: Documents/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Document document = _context.Documents.Single(m => m.ID == id);
            if (document == null)
            {
                return HttpNotFound();
            }

            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Documents.Add(document);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Document document = _context.Documents.Single(m => m.ID == id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Update(document);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Document document = _context.Documents.Single(m => m.ID == id);
            if (document == null)
            {
                return HttpNotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Document document = _context.Documents.Single(m => m.ID == id);
            _context.Documents.Remove(document);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
