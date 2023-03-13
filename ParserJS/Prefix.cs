using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Prefix
    {
        public Branch PrefixNud(Branch First, Branch branch, Parser parser)
        {
            switch (branch.BranchValue.symbol.Value)
            {
                case "(":
                    return nud(First, branch, parser);
                case "[":
                    return nud(First, branch, parser);
                case "{":
                    return nud(First, branch, parser);
                default:
                    return nud(First, branch, parser);
            }
        }
        public Branch nud (Branch First, Branch branch, Parser parser)
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
