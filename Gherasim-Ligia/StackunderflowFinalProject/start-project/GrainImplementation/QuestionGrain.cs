using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackUnderflow.EF.Models;


namespace GrainImplementation
{
    public class QuestionGrain: Orleans.Grain
    {
        private StackUnderflowContext _dbContext;
        private QuestionGrain state;
        public QuestionGrain(StackUnderflowContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override Task OnActivateAsync()
        {
            //read state from Db where postid = or parentid=
            //subscribe to reply states
            return base.OnActivateAsync();
        }

        //public GetQuestionWithReplys()
        //{

        //}
    }
}
