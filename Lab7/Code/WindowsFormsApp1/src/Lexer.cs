using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    internal class Lexer
    {
        private List<Token> Tokens;
        public List<Token> Analyze(string input)
        {
            int i;
            string value;
            Tokens.Clear();
            for (i = 0; i < input.Length; i++)
            {
                value = string.Empty + input[i];
                if (char.IsLetter(input[i]) || char.IsDigit(input[i]))
                {
                    int startIndex = i;
                    while ((i + 1) < input.Length && (char.IsLetter(input[i + 1]) || char.IsDigit(input[i + 1])))
                    {
                        i++;
                        value += input[i];
                    }
                    Tokens.Add(new Token(TokenType.ID, value, startIndex + 1, i + 1));
                    //if (i < input.Length - 1 && (input[i + 2] == '+' || input[i + 2] == '-'))
                    //{
                    //    Tokens.Add(new Token(TokenType.Exp, value, startIndex + 1, i + 1));
                    //}
                    //else if (i < input.Length - 1 && (input[i + 2] == '*' || input[i + 2] == '/'))
                    //{
                    //    Tokens.Add(new Token(TokenType.Multexp, value, startIndex + 1, i + 1));
                    //}
                    //else if (i > 1 && input[i - 2] == '^')
                    //{
                    //    Tokens.Add(new Token(TokenType.Powexp, value, startIndex + 1, i + 1));
                    //}
                    //else
                    //{
                    //    Tokens.Add(new Token(TokenType.ID, value, startIndex + 1, i + 1));
                    //}
                }
                else
                {
                    switch (input[i])
                    {
                        case '\t':
                        case ' ':
                            break;
                        case '+':
                        case '-':
                            Tokens.Add(new Token(TokenType.ArithOp, value, i + 1, i + 1));
                            break;
                        case '*':
                        case '/':
                            Tokens.Add(new Token(TokenType.MultOp, value, i + 1, i + 1));
                            break;
                        case '^':
                            Tokens.Add(new Token(TokenType.PowOp, value, i + 1, i + 1));
                            break;
                        default:
                            Tokens.Add(new Token(TokenType.Error, value, i + 1, i + 1));
                            break;
                    }

                }
            }
            return Tokens;
        }
        public Lexer()
        {
            Tokens = new List<Token>();
        }
    }
}
