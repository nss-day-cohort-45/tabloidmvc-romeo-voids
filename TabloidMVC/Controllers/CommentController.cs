using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
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
            var comments = _commentRepository.GetAllPublishedComments();
            return View(comments);
        }

        //GET: CommentController/Details/2
        public IActionResult Details(int id)
        {
            var comment = _commentRepository.GetPublishedCommentById(id);
            if (comment == null)
            {
                int userId = GetCurrentUserProfileId();
                comment = _commentRepository.GetUserCommentById(id, userId);
                if (comment == null)
                {
                    return NotFound();
                }
            }
            return View(comment);
        }

        //GET: CommentController/Create
        public IActionResult Create()
        {
            var vm = new CommentCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        //POST: CommentController/Create
        [HttpPost]
        public IActionResult Create(CommentCreateViewModel vm)
        {
            try
            {
                vm.Comment.CreateDateTime = DateAndTime.Now;
                vm.Comment.IsApproved = true;
                vm.Comment.UserProfileId = GetCurrentUserProfileId();

                _commentRepository.Add(vm.Comment);

                return RedirectToAction("Details", new { id = vm.Comment.Id });
            }
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
