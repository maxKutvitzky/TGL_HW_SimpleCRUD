using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;
using SimpleCrud.Model.Enums;

namespace SimpleCrud.Mvc.Controllers
{
    [Route("/[controller]/[action]")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentsController(IStudentRepository repository)
        {
            _studentRepository = repository;
        }
        internal SelectList GetGroupsSelectList(IGroupRepository repository) =>
            new SelectList(repository.GetAll(), nameof(Group.Id), nameof(Group.Name));

        [Route("/")]
        [Route("/[controller]")]
        [Route("/[controller]/[action]")]
        public IActionResult Index() => View(_studentRepository.GetAll());
        
        [HttpGet("{id?}")]

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            Student student = _studentRepository.GetById(id);
            return View(student);
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _studentRepository.Delete(student);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id?}")]
        public IActionResult Edit([FromServices] IGroupRepository repository ,int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            Student student = _studentRepository.GetById(id);
            ViewBag.Groups = GetGroupsSelectList(repository);
            return View(student);
        }

        [HttpPost("{id}")]
        public IActionResult Edit([FromServices] IGroupRepository repository, int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            student.Group = repository.GetById(student.Group.Id);
            if (ModelState.IsValid)
            {
                _studentRepository.Update(student);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Groups = GetGroupsSelectList(repository);
            return View(student);
        }
        [HttpGet]
        public IActionResult Create([FromServices] IGroupRepository repository)
        {
            ViewBag.Groups = GetGroupsSelectList(repository);
            return View(new Student{Group = new Group()});
        }

        [HttpPost]
        public IActionResult Create([FromServices] IGroupRepository repository, Student student)
        {
            if (ModelState.IsValid)
            {
                student.Group = repository.GetById(student.Group.Id);
                _studentRepository.Add(student);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Groups = GetGroupsSelectList(repository);
            return View(student);
        }
    }
}
