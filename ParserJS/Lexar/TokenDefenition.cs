using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserJS.Lexar
{
    internal class TokenDefenition
    {
        private Regex regex;
        private readonly TType tokenType;

        public TokenDefenition(TType token, string regexP)
        {
            regex = new Regex(regexP, RegexOptions.IgnoreCase);
            tokenType = token;
        }

        public TokenMatch Match(string input, int linePOS) // TODO: Current bug by number*number without space or 19* 8 with operative on first number
        {
            var match = regex.Match(input);
            if (match.Success)
            {
                string remaining = string.Empty;
                if (match.Length != input.Length)
                {
                    //remaining = input.Substring(match.Length);
                    Regex Splice = new Regex($@"\{match}\b");
                    remaining= Splice.Replace(input, " ", 1);
                }
                return new TokenMatch(tokenType, match.Value, linePOS, remaining);
            }
            else
            {
                return new TokenMatch(TType.Unknown, input, 0, string.Empty);
            }
        }

    }
}
