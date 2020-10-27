using LanguageExt.UnitsOfMeasure;
using Question.Domain.CreateQuestionWorkflow;
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
        public string Title { get; private set; }
        [Required(ErrorMessage = "Body is missing")]
        [MinLength(30),MaxLength(30000)]
        public string Body { get; private set; }

        [Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        [MinLength(1), MaxLength(10)]
        public string[] Tags { get; private set; }

        public int VoteCount { get; private set; }
        //public IReadOnlyCollection<VoteEnum> AllVotes { get; private set; }

        //[Required(ErrorMessage = "Please enter at least one tag; see a list of popular tags.")]
        //[MinLength(1), MaxLength(10)]
        //public string Tag { get; set; }
        //public List<string> tagList = null;

        public CreateQuestionCmd(string title, string body, string[] tags, int voteCount)/*List<VoteEnum> votes*/
        {
            this.Title = title;
            this.Body = body;
            this.Tags = tags;
            this.VoteCount = voteCount;
            //tagList.Add(tag);
        }

        //public List<string> GetTagList()
        //{
        //    return tagList;
        //}
    }
}
