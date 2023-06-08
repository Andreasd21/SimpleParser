using ParserJS.Lexar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Statement
    {
        public Branch SwitchStatement(Branch First,Branch branch, Parser parser)
        {

            switch (branch.BranchValue.Value)
            {
                case "let":
                    return stdLet(First,  parser);
                default:
                   return std(First,parser);
            }
        }

        private Branch stdLet(Branch First, Parser parser)
        {
            parser.Advance();
            Branch mainBranch= new(parser.token); 
            if (parser.token.Value == "=")
            {
                mainBranch.BranchValue =  parser.token;
                parser.Advance("=");
                mainBranch.First = First;
                mainBranch.Second = parser.Expression(0);
                if (parser.token.Value == ",")
                {
                    parser.Advance(",");
                }
            }
            parser.Advance(";");
            return mainBranch;
        }

        public Branch std(Branch First, Parser parser)
        {
            Branch Second = parser.Expression(First.BranchValue.symbol.lbp);
            if (Second == null)
            {
                throw new Exception("Something went wrong");
            }
            First.AddBranchValue(First, Second);
            return First;
        }
    }
}
