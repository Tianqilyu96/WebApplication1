using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Post
    {
        public long Id { get; set; }

        private string _key;

        public string Key
        {
            get
            {
                if(_key == null)
                {
                    _key = Regex.Replace(title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set { _key = value; }
        }


        [StringLength(100,MinimumLength =5,ErrorMessage ="Please enter 5-100 chars")]//validation attributes
        public string name { get; set; }


        [Display(Name = "The Title")] //chage name to Your Name on page
        [Required]
        [DataType(DataType.Text)]//specify data type
        [StringLength(15,MinimumLength = 5, ErrorMessage ="Invalid title!!!")]
        public string title { get; set; }
       
        [Display(Name ="Say Something!")]
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(20,ErrorMessage ="Enter more!!!!")]
        public string body { get; set; }

        public DateTime time { get; set; }

    }
}
