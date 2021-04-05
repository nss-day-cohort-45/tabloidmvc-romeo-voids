using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        //instance of comment repository
        public CommentController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        //GET: CommentController
        public IActionResult Index()
        {
            var comments = _commentRepository.GetAllComments();
            return View(comments);
        }



        //GET: CommentController/Create
        public IActionResult Create()
        {
     
            return View();
        }

        //POST: CommentController/Create
        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            try
            {
                comment.CreateDateTime = DateAndTime.Now;
          
                _commentRepository.Add(comment);

                return RedirectToAction("Index");
            }
            catch
            {

                return View(comment);
            }
        }


        //GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            Comment comment = _commentRepository.GetCommentById(id);

            return View(comment);
        }

        //POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            try
            {
                _commentRepository.DeleteComment(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(comment);
            }
        }

        //GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            Comment comment = _commentRepository.GetCommentById(id); 

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        //POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                _commentRepository.EditComment(comment);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(comment);
            }
        }
    }
}
