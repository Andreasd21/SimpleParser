using ParserJS.Lexar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Tree
    {
        public List<Branch>branches = new ();

        public void AddBranch(Branch b)
        {
            branches.Add(b);
        }

        public override string? ToString()
        {
            string tree = "";
            foreach (Branch branch in branches)
            {
                tree += $"{branch}";
            }
            return tree;    
        }
    }
}


