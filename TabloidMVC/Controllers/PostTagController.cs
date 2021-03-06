using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostTagController : Controller
    {
        private readonly IPostTagRepository _postTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;
        public PostTagController(IPostTagRepository postTagRepository, ITagRepository tagRepository, IPostRepository postRepository)
        {
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;
            _postRepository = postRepository;
        }

        // GET: PostTagController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PostTagController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostTagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostTagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostTagController/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new PostTagFormViewModel();
            vm.TagOptions = _tagRepository.GetAllTags();
            vm.Post = _postRepository.GetPublishedPostById(id);

            var postId = id;
            vm.PostTags = _postTagRepository.GetAllPostTagsByPostId(postId); 

            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: PostTagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostTagFormViewModel vm)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            //try
            //{
            //    // Need to delete existing postTag tables
            //    var postId = id;
            //    var OldPostTags = _postTagRepository.GetAllPostTagsByPostId(postId);

            //    foreach (var postTag in OldPostTags)
            //    {
            //        _postTagRepository.DeletePostTag(postId);
            //    }

            //    // Need to get a list of all the checked tags and use the tagIds to make new postTag entries in the DB
            //    var NewPostTags = vm.PostTags;

            //    foreach (var postTag in NewPostTags)

            //    _postTagRepository.AddPostTag(postTag);



            //    return RedirectToAction("Details", new { id = vm.Post.Id });
            //}
            //catch
            //{
            //    vm.TagOptions = _tagRepository.GetAllTags();
            //    return View(vm);
            //}
        }

        // GET: PostTagController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostTagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
