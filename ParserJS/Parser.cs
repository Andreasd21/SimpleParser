using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using ParserJS.Lexar;

namespace ParserJS
{
    public class Parser
    {
        public Tree Tree = new();
        public List<Symbol> symbols;
        public List<Token> tokens;
        public int tokenIndex = 0;
        public Token token;
        public Branch temp;
        public Prefix prefix = new();
        public Infixr infixr = new();
        public Infix infix = new();
        public Statement statement = new();


        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;

            symbols = new ();
            JObject OperatorPrecedence = JObject.Parse(File.ReadAllText(@"../../../OperatorPrecedence.json"));
            if (OperatorPrecedence == null)
            {
                throw (new Exception("json empty or unable to read"));
            }
            foreach (string Key in OperatorPrecedence.Properties().Select(p => p.Name).ToList())
            {
                SymbolType Type = CheckSymbolType(Key);
                foreach (JProperty Symbol in OperatorPrecedence[Key])
                {
                    symbols.Add(new(Symbol.Name, Type, Convert.ToInt32(Symbol.Value)));
                }
            }

            //give symbol double type if neccesary here like '(' '[' '{'
            symbols.Add(new("(", SymbolType.infix));
            symbols[symbols.Count - 1].ExtraSybol = SymbolType.prefix;

            symbols.Add(new("[", SymbolType.infix));
            symbols[symbols.Count - 1].ExtraSybol = SymbolType.prefix;

            symbols.Add(new("{", SymbolType.prefix));
            symbols[symbols.Count - 1].ExtraSybol = SymbolType.statement;

            Advance();
            Statements();

        }

        private SymbolType CheckSymbolType(string type)
        {
            SymbolType symbolType = type switch
            {
                "infix" => SymbolType.infix,
                "infixr" => SymbolType.infixr,
                "prefix" => SymbolType.prefix,
                "assignement" => SymbolType.assignement,
                "statement" => SymbolType.statement,
                _ => SymbolType.symbol,
            };
            return symbolType;
        }

        public void Advance(string? token_value = null)
        {
            // gives last token the end symbol // niet nodig zonder scope
            if(tokenIndex == tokens.Count)
            {
                Symbol? end = symbols.Find(x => x.Value == "(end)");
                if (end == null)
                {
                    throw new Exception("end symbol niet gevonden");
                }
                tokens.Add(new(TType.Unknown, tokens[tokenIndex - 1].Value, 0));
                tokens[tokenIndex].AssignSymbol(end);
            }

            token = tokens[tokenIndex];
            // Check if token value corresponds with expexted value given in parameters. 
            if(token_value != null) 
            {
                if(token_value != token.Value)
                {
                    throw new Exception("Expected: " + token_value);
                }
            }
            if (token.Type == TType.Name)
            {
                Symbol? name = symbols.Find(x => x.Value == "(name)");
                if (name == null)
                {
                    throw new Exception("name symbol niet gevonden");
                }
                token.AssignSymbol(name);
            }
            else if (token.Type == TType.Operative)
            {
                Symbol? Operator = symbols.Find(x => x.Value == token.Value);
                if(Operator == null)
                {
                    throw new Exception("unkown operator");
                }
                token.AssignSymbol(Operator);
            }
            else if(token.Type == TType.String || token.Type == TType.Number)
            {
                Symbol? literal = symbols.Find(x => x.Value == "(literal)");
                if (literal == null)
                {
                    throw new Exception("literal symbol niet gevonden");
                }
                token.AssignSymbol(literal);
            }
            else if(token.symbol.Value == "(end)")
            {
                return;
            }
            else
            {
                throw new Exception("unkown token given ");
            }
            tokenIndex++;
        
        }

        public Branch Expression(int rbp)
        {
            Token t = token;
            Advance();
            Branch prevBranch = new(t);
            if(token.symbol.symbolType == SymbolType.prefix || token.symbol.ExtraSybol == SymbolType.prefix)
            {
                nud(new(token), prevBranch);
            }
            while(rbp < token.symbol.lbp)
            {
                t = token;
                Advance();
                prevBranch = led(prevBranch, new(t));
            }
            return prevBranch;
        }

        public void Statements()
        {
            while (true)
            {
                if (tokens[tokenIndex].symbol != null)
                {
                    if (tokens[tokenIndex].symbol.Value == "(end)")
                    {
                        break;
                    }
                }
                Expression(0);
                Tree.AddBranch(temp);
                Advance(";");
            }
        }

        public Branch led(Branch First, Branch branch) 
        {
            switch (branch.BranchValue.symbol.symbolType)
            {
                case SymbolType.infix:
                    temp = infix.InfixLed(First, branch, this);
                    break;
                case SymbolType.infixr:
                    temp = infixr.led(First, branch, this);
                    break;
                default:
                    throw new Exception("Not the correct symbol given");
            }
            return temp;
        }
        public Branch nud(Branch First, Branch branch)
        {
            temp = prefix.PrefixNud(First, branch,this);
            return temp;
        }
    }
}
