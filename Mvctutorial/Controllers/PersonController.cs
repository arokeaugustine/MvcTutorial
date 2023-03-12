using Microsoft.AspNetCore.Mvc;
using Mvctutorial.Models.Domain;

namespace Mvctutorial.Controllers;
    public class PersonController : Controller
    {
        private readonly DatabaseContext _context;

        public PersonController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        // [HttpPost]
        // public IActionResult AddPerson(Person person)
        // {
        //     return View();
        // }
        public IActionResult AddPerson(Person person)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _context.Person.Add(person);
                _context.SaveChanges();
                TempData["message"] = "Added successfully";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {

                TempData["message"] = "Could not be added";
                return View();
            }
           
        }

        public IActionResult DisplayPersons()
        {
            var persons = _context.Person.ToList();
            return View(persons);
        }

        public IActionResult PersonDetail(int id)
        {
            var person = _context.Person.Find(id);
            return View(person);
        }

         public IActionResult EditPerson(int id)
         {
            var person = _context.Person.Find(id);
            
            return View(person);
         }


        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
          
            if (!ModelState.IsValid)
            {
                return View(person);
               
            }

            try
            {
                _context.Person.Update(person);
                _context.SaveChanges();
                TempData["message"] = "Updated successfully";
                return RedirectToAction("DisplayPersons");
            }
            catch(Exception ex)
            {
                ex.ToString();
                TempData["message"] = "Could not be updated";
                return View();
            }

        }


        public IActionResult DeletePerson(int id)
        {
            
            try
            {
                var persons = _context.Person.Find(id);
                if (persons != null)
                {
                    _context.Person.Remove(persons);
                    _context.SaveChanges();
                    return RedirectToAction("DisplayPersons");
                }
            }
            catch (Exception ex)
            {
                
                ex.ToString();
            }
            return RedirectToAction("AddPersons");
            
        }

            
    }
