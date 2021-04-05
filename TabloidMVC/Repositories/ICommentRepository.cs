using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();
        Comment GetCommentById(int id);

        void Add(Comment comment);
        void DeleteComment(int id);

        //void EditComment(Comment comment);
     
    }
}