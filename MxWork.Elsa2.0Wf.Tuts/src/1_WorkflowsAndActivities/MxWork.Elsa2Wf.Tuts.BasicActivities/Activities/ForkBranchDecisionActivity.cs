using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    public class ForkBranchDecisionActivity: Activity
    {
        protected override IActivityExecutionResult OnExecute()
        {
            Console.WriteLine("Choose a branch. A or B");
            Console.WriteLine("Typing a will resulting in going through branch A");
            Console.WriteLine("Any other key will result in branch B");
            var userChoosenBranch = Console.ReadLine();

            if(userChoosenBranch!.ToUpper() == "A")
                return Outcome("A");

            return Outcome("B");
        }

    }
}
