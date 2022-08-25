using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;

namespace SimpleCrud.Mvc.Controllers;

[Route("/[controller]/[action]")]
public class StudentsController : Controller
{
    private readonly IStudentRepository _studentRepository;

    public StudentsController(IStudentRepository repository)
    {
        _studentRepository = repository;
    }
    internal SelectList GetGroupsSelectList(IGroupRepository repository)
    {
        return new SelectList(repository.GetAll(), nameof(Group.Id), nameof(Group.Name));
    }

    [Route("/")]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public IActionResult Index()
    {
        return View(_studentRepository.GetAll());
    }

    [HttpGet("{id?}")]
    public IActionResult Delete(int? id)
    {
        if (!id.HasValue) return NotFound();

        var student = _studentRepository.GetById(id);
        if (student == null) return NotFound();

        return View(student);
    }

    [HttpPost("{id}")]
    public IActionResult Delete(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        _studentRepository.Delete(student);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id?}")]
    public IActionResult Edit([FromServices] IGroupRepository repository, int? id)
    {
        if (!id.HasValue) return NotFound();

        var student = _studentRepository.GetById(id);
        if (student == null) return NotFound();

        ViewBag.Groups = GetGroupsSelectList(repository);
        return View(student);
    }

    [HttpPost("{id}")]
    public IActionResult Edit([FromServices] IGroupRepository repository, int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        student.Group = repository.GetById(student.Group.Id);
        //A viewmodel should be used, but adding it now is time consuming 
        ModelState.Remove("Passport.StudentId");
        ModelState.Remove("Passport.Student");

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
        return View(new Student { Group = new Group() });
    }

    [HttpPost]
    public IActionResult Create([FromServices] IGroupRepository repository,
        [FromServices] ISubjectRepository subjectRepository,
        Student student)
    {
        //A viewmodel should be used, but adding it now is time consuming
        ModelState.Remove("Passport.StudentId");
        ModelState.Remove("Passport.Student");

        if (ModelState.IsValid)
        {
            student.Group = repository.GetById(student.Group.Id);
            student.Subjects = subjectRepository.GetAll().ToList().GetRange(0, 3);
            _studentRepository.Add(student);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Groups = GetGroupsSelectList(repository);
        return View(student);
    }

    [HttpGet("{id}")]
    public IActionResult Info(int id)
    {
        Student student = _studentRepository.GetById(id);
        if (student == null) return NotFound();
        return View(student);
    }
}