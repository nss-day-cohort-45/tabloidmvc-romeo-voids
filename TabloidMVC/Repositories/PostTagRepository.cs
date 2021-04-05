using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class PostTagRepository : BaseRepository, IPostTagRepository
    {
        public PostTagRepository(IConfiguration config) : base(config) { }
        public List<PostTag> GetAllPostTagsByPostId(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, [Name]
                         FROM Tag
                        WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    List<PostTag> postTags = new List<PostTag>();

                    while (reader.Read())
                    {
                        PostTag postTag = new PostTag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            TagId = reader.GetInt32(reader.GetOrdinal("TagId")),
                        };
                        postTags.Add(postTag);
                    }
                    reader.Close();
                    return postTags;
                }
            }
        }
        public void AddPostTag(PostTag postTag);
        public void DeletePostTag(PostTag postTag);
    }
}
