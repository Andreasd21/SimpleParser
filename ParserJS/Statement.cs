using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Statement
    {
        public int specialCharackter(string value)
        {
            return 0;
        }
        public Branch std(Branch First, Branch branch, Parser parser)
        {
            Branch Second = parser.Expression(branch.BranchValue.symbol.lbp);
            if (Second == null)
            {
                throw new Exception("Something went wrong");
            }
            branch.AddBranchValue(First, Second);
            return branch;
        }
    }
}
