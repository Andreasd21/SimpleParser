using ParserJS.Lexar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public  class Infix
    {
        public Branch InfixLed(Branch First, Branch branch, Parser parser)
        {
            switch (branch.BranchValue.symbol.Value)
            {
                case "?":
                    return DefaultLed(First, branch, parser);
                case ".":
                    return DefaultLed(First, branch, parser);
                case "[":
                    return DefaultLed(First, branch, parser);
                case "(":
                    return DefaultLed(First, branch, parser);
                default:
                    return DefaultLed( First,  branch,  parser);
            }
        }
        public Branch DefaultLed(Branch First, Branch branch, Parser parser)
        {
            Branch Second = parser.Expression(branch.BranchValue.symbol.lbp);
            if (Second == null)
            {
                throw new Exception("Something went wrong");
            }
            branch.AddBranchValue(First, Second);
            return branch;
        }

        public Branch BracketLed(Branch First, Branch branch, Parser parser)
        {
            return DefaultLed(First, branch, parser);
        }
    }
}
