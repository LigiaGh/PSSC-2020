using LanguageExt.UnitsOfMeasure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Question.Domain
{
    public struct CreateQuestionCmd
    {
        [Required(ErrorMessage = "Title is missing")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Title is not valid.")]
        [MinLength(15),MaxLength(150)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body is missing")]
        [MinLength(30),MaxLength(30000)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        [MinLength(1), MaxLength(10)]
        public string[] Tags { get; set; }


        //[Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        //[MinLength(1), MaxLength(10)]
        //public string Tag { get; set; }
        //public List<string> tagList = null;

        public CreateQuestionCmd(string title, string body, string[] tags)
        {
            this.Title = title;
            this.Body = body;
            this.Tags = tags;
            //tagList.Add(tag);
        }

        //public List<string> GetTagList()
        //{
        //    return tagList;
        //}
    }
}
