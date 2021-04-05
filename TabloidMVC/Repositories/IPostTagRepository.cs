using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostTagRepository
    {
        List<PostTag> GetAllPostTagsByPostId(int postId);
        //void AddPostTag(PostTag postTag);
        //void DeletePostTag(PostTag postTag);
    }
}
