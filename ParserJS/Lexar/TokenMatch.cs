using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS.Lexar
{
    public class TokenMatch
    {
        public TType Type;
        public string Value;
        public int Line;
        public string RemainingText;

        public TokenMatch(TType type, string value, int line, string remainingText)
        {
            Type = type;
            Value = value;
            Line = line;
            RemainingText = remainingText;
        }
    }
}
