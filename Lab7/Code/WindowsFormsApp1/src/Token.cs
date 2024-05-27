using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    public enum TokenType
    {
        Null,
        ArithOp,
        PowOp,
        MultOp,
        Exp,
        Multexp,
        Powexp,
        ID,
        Error
    }
    internal class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public Token(TokenType type, string value, int startIndex, int endIndex)
        {
            Type = type;
            Value = value;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}
