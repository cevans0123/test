using System.Collections.Generic;
using System.Linq;
using Ideas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ideas.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext _context;
        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("Main");
        }

        [HttpGet]
        [Route("main")]
        public IActionResult Main()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("CurrUser", user.Name);
                return RedirectToAction("Home");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password)
        {
            var user = _context.Users.SingleOrDefault(p => p.Email == Email);
            if (user != null && Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                var result = Hasher.VerifyHashedPassword(user, user.Password, Password);
                if (result != 0)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("CurrUser", user.Name);
                    return RedirectToAction("Home");
                }
            }
            return View("Index");
        }

        [HttpGet]
        [Route("bright_ideas")]
        public IActionResult Home()
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            ViewBag.SessionId = HttpContext.Session.GetInt32("UserId");
            List<Idea> AllIdeas = _context.Ideas.Include(p => p.User).Include(s => s.Participants).ToList();
            ViewBag.AllIdeas = AllIdeas;
            ViewBag.CurrUser = HttpContext.Session.GetString("CurrUser");
            // ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View("Home");
        }

        [HttpPost]
        [Route("bright_ideas")]
        public IActionResult Add(Idea newIdea)
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                Idea NewIdea = new Idea {
                Post = newIdea.Post,
                User = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"))
                };
                _context.Ideas.Add(NewIdea);
                _context.SaveChanges();
                return RedirectToAction("Home");
            }
            else
            {
                return View("Home");
            }
        }

        [HttpGet]
        [Route("like/{id}")]
        public IActionResult Like(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            User ThisUser = _context.Users.SingleOrDefault(p => p.UserId == HttpContext.Session.GetInt32("UserId"));
            Idea OneIdea = _context.Ideas.SingleOrDefault(p => p.IdeaId == id);
            Participant like = new Participant {
                UserId = ThisUser.UserId,
                User = ThisUser,
                IdeaId = id,
                Idea = OneIdea,
            };
            _context.Participants.Add(like);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("users/{id}")]
        public IActionResult DisplayUser(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            User ThisUser = _context.Users.Include(z => z.Participants).ThenInclude(p => p.User).SingleOrDefault(p => p.UserId == id);
            ViewBag.ThisUser = ThisUser;
            ViewBag.CurrUser = HttpContext.Session.GetString("CurrUser");
            List<Idea> AllIdeas = _context.Ideas.Include(p => p.User).Include(s => s.Participants).ToList();
            ViewBag.AllIdeas = AllIdeas;
            return View();
        }

        [HttpGet]
        [Route("bright_ideas/{id}")]
        public IActionResult DisplayIdea(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            Idea ThisIdea = _context.Ideas.Include(z => z.Participants).ThenInclude(p => p.User).SingleOrDefault(p => p.IdeaId == id);
            ViewBag.ThisIdea = ThisIdea;
            ViewBag.CurrUser = HttpContext.Session.GetString("CurrUser");
            List<User> AllUsers = _context.Users.Include(s => s.Participants).ThenInclude(w => w.Idea).ToList();
            ViewBag.AllUsers = AllUsers;
            return View();
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(HttpContext.Session.GetInt32("UserId") == null){
                return RedirectToAction("Index");
            }
            Idea SingleIdea = _context.Ideas.SingleOrDefault(s => s.IdeaId == id);
            _context.Ideas.Remove(SingleIdea);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}