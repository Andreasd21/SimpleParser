using ParserJS.Lexar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Scope
    {

        private Scope? ChildScope { get; set; }
        private Scope? ParentScope { get; set; }
        public List<Token> Tokens { get; set; }
        

        public Scope(Scope? Parent)
        {
            if (Parent != null)
            {
                ParentScope = Parent;
            }
        }

        public Scope? Pop()
        {
            if (ParentScope != null)
            {
                return ParentScope;
            }
            else return null;
        }
        public Scope ToRoot()
        {
            if (ParentScope == null)
            {
                return this;
            }
            else
            {
                return ParentScope.ToRoot();
            }
        }

        public void Reserve(Token t)
        {
            if(t.Type != TType.Name)
            {
                throw new ArgumentException("token not the right type");
            }

        }

        public void AssignChild(Scope child)
        {
            ChildScope = child;
        }
    }
}
