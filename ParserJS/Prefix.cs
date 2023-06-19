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
                    return BracketNud(parser);
                case "[":
                    return BlokBracketNud(First, branch, parser);
                case "{":
                    return CurlyBracketNud(First, branch, parser);
                default:
                    return nud(branch, parser);
            }
        }
        public Branch nud ( Branch branch, Parser parser)
        {
            branch.First = parser.Expression(70);
            return branch;
        }

        public Branch BracketNud(Parser parser)
        {
            Branch branch = parser.Expression(0);
            parser.Advance(")");
            return branch;
        }

        public Branch BlokBracketNud(Branch First, Branch branch, Parser parser)
        {
            return nud( branch, parser);
        }

        public Branch CurlyBracketNud(Branch First, Branch branch, Parser parser)
        {
            return nud( branch, parser);
        }
    }
}
