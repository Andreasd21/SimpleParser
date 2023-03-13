using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Infixr
    {
        public Branch led(Branch First, Branch branch, Parser parser)
        {
            Branch Secondr = parser.Expression(branch.BranchValue.symbol.lbp - 1);
            if (Secondr == null)
            {
                throw new Exception("Something went wrong");
            }
            branch.AddBranchValue(First, Secondr);
            return branch;
        }
    }
}
