using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetAllComments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT c.Id, c.PostId, c.UserProfileId, 
                              c.Subject, c.Content,
                              c.CreateDateTime, 
                              p.Title AS PostTitle,
                              u.DisplayName 
                         FROM Comment c
                              LEFT JOIN Post p ON c.PostId = p.id
                              LEFT JOIN UserProfile u ON p.UserProfileId = u.id
                        ";
                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        comment.Post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                            Title = reader.GetString(reader.GetOrdinal("PostTitle"))
                        };

                        comment.UserProfile = new UserProfile()
                        {
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                        };
                        comments.Add(comment);
                        
                    }

                    reader.Close();

                    return comments;
                }
            }
        }

        public Comment GetCommentById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                           SELECT c.Id, c.PostId, c.UserProfileId, 
                              c.Subject, c.Content,
                              c.CreateDateTime,
                              p.Title AS PostTitle,
                              u.DisplayName 
                         FROM Comment c
                              LEFT JOIN Post p ON c.PostId = p.id
                              LEFT JOIN UserProfile u ON p.UserProfileId = u.id
                        WHERE c.Id = @id
                              ";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };

                        comment.Post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                         
                            Title = reader.GetString(reader.GetOrdinal("PostTitle"))
                        };

                        comment.UserProfile = new UserProfile()
                        {
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                        };

                        reader.Close();

                        return comment;
                    }

                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }


        }

        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Comment (
                            PostId, UserProfileId, Subject, Content, CreateDateTime)
                        OUTPUT INSERTED.ID
                        VALUES (
                            @PostId, @UserProfileId, @Subject, @Content, @CreateDateTime)";
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteComment(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Comment WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditComment(Comment comment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Comment
                            SET
                                PostId = @PostId,
                                UserProfileId = @UserProfileId,
                                Subject = @Subject,
                                Content = @Content,
                                CreateDateTime = @CreateDateTime";

                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
