using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserJS.Lexar
{
    public class Tokinizer
    {
        //private string[] Source; niet gelukt om per lijn te doen
        private string Source;
        private List<Token> Tokens;
        private List<TokenDefenition> TokenDefs;


        public Tokinizer(string source)
        {
            TokenDefs = new List<TokenDefenition>();
            Tokens = new List<Token>();
            Source = source;

            TokenDefs.Add(new(TType.Operative, @"^\+|-|>=|\*|/|=|>|<|===|<=|&|\||%|!|\^|\(|\)|;$"));
            TokenDefs.Add(new(TType.Comment, @" \/\/ .* ")); // error als ik iets doe zoals 8 *8
            TokenDefs.Add(new(TType.Function, @"^function$"));
            TokenDefs.Add(new(TType.String, "\".*\"")); // error als string spactie bevat
            TokenDefs.Add(new(TType.Name, @"[a-z A-Z][ a-z A-Z _ 0-9 ]*[u0020+]{0,1}"));
            TokenDefs.Add(new(TType.Number, @"\d+(?: \. \d+ )?(?: [ e E ] [ + \- ]? \d+ )?"));
        }

        public List<Token> MakeTokens()
        {
            string[] SourceSplit = Source.Split(null);
            List<string> strings = SourceSplit.ToList();
            for (int i = 0; i < strings.Count; i++)
            {
                string line = strings[i];
                while (!string.IsNullOrEmpty(line))
                {
                    TokenMatch match = FindMatch(line, i);
                    string remain = match.RemainingText;
                    if(remain.Contains(' '))
                    {
                        List<string> listRemain = SplitRemain(remain);
                        strings.AddRange(listRemain);
                        match.RemainingText = String.Empty;
                    }
                    line = match.RemainingText;
                    Tokens.Add(new(match.Type, match.Value, match.Line));
                }
            }
            return Tokens;
        }

        private TokenMatch FindMatch(string line, int LinePos)
        {
            foreach (TokenDefenition TD in TokenDefs)
            {
                TokenMatch match = TD.Match(line, LinePos);
                if (match.Type != TType.Unknown)
                {
                    return match;
                }
            }
            return new(TType.Unknown, line, 0, string.Empty);
        }

        private List<string> SplitRemain(string remain)
        {
            string[] SplitRemain = remain.Split(null);
            List<string> listRemain = SplitRemain.ToList();
            return listRemain;
        }
    }
}
