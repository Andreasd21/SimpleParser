using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserJS
{
    public class Symbol
    {
        public Symbol( string value, SymbolType symbolType, int lbp = 0, SymbolType? extraSybol = null)
        {
            Value = value;
            this.lbp = lbp;
            this.symbolType = symbolType;
            ExtraSybol = extraSybol;
        }

        public string Value { get; set; }
        public int lbp { get; set; } = 0;
        public SymbolType symbolType { get; set; }
        public SymbolType? ExtraSybol { get; set; }
    }
}
