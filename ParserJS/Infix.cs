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
                    return QuestionLed(First, branch, parser);
                case ".":
                    return DotLed(First, branch, parser);
                case "[":
                    return BlokBracketLed(First, branch, parser);
                case "(":
                    return BracketLed(First, branch, parser);
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

        public Branch DotLed(Branch First, Branch branch, Parser parser)
        {
            return DefaultLed(First, branch, parser);
        }

        public Branch BlokBracketLed(Branch First, Branch branch, Parser parser)
        {
            return DefaultLed(First, branch, parser);
        }

        public Branch QuestionLed(Branch First, Branch branch, Parser parser)
        {
            return DefaultLed(First, branch, parser);
        }
    }
}
