using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        List<Post> GetPostsByUserId(int userId);
        void UpdatePost(Post post);
        void ReplacePostCategory(int id, Category category);
        void DeletePost(int id);
    }
}