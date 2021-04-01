using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }



        [Required]
        [DisplayName("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }




        [DisplayName("Author")]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }



        [Required]
        public string Subject { get; set; }




        [Required]
        public string Content { get; set; }



        public DateTime CreateDateTime { get; set; }

        [DisplayName("Published")]
        [DataType(DataType.Date)]
        public DateTime? PublishDateTime { get; set; }




    }
}