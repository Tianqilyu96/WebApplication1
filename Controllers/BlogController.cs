using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogData _db;

        public BlogController(BlogData db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.time).Take(5).ToArray();//select most recent 5 posts in database
            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Key == key);//find the post
            return View(post);
        }

        [Authorize]
        [HttpGet,Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost,Route("create")]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid) //set some varify
            {
                return View();
            }

            post.name = User.Identity.Name;
            post.time = DateTime.Now;

            _db.Posts.Add(post);
            _db.SaveChanges(); //save post to database

            return RedirectToAction("Post", "Blog", 
                new { year = post.time.Year, month = post.time.Month, key = post.Key});//redirect to single post page
            
        }
    }
}
