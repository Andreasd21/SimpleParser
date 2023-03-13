using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS.Lexar
{
    public class Token
    {
        public TType Type;
        public string Value;
        public int Word;
        public Symbol? symbol = null;

        public Token(TType type, string value, int word)
        {
            Type = type;
            Value = value;
            Word = word;
        }

        public bool AssignSymbol(Symbol s)
        {
            if(symbol == null)
            {
                symbol = s;
                return true;
            }
            else
            {
                return false;
            }

        }

        public override string? ToString()
        {
            return $"Type: {Type}{Environment.NewLine}Value: {Value}{Environment.NewLine}Word:{Word}{Environment.NewLine} ";
        }
    }
}
