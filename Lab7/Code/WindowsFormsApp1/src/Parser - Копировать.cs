using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    internal class Parser2
    {
        private string result = "";
        private List<Token> tokens;
        private Token CurrToken;
        private Token NextToken;
        private Token NextNextToken;
        private Token PrevToken;
        private int CurrIndex;
        private int MaxIndex;
        private const string sep1 = " -> ";

        public string Parse(List<Token> tokensList)
        {
            tokens = tokensList;
            CurrIndex = 0;
            MaxIndex = tokensList.Count - 1;
            CurrToken = tokens[CurrIndex];

            if (MaxIndex > 1 ) NextToken = tokens[CurrIndex + 1];    
            //PrevToken = tokens[CurrIndex - 1];
            result = string.Empty;
            try
            {
                log($"<Expr>", sep1);
                Rer(false);
            }
            catch (SyntaxErrorException)
            {
               // log("Syntax Error: Обнаружено некоррнктное выражение.");
            }
            return result;
        }
        private void log(string str, string sep = sep1)
        {
            result += (result == string.Empty) ? str : $"{sep}{str}";
        }
        private void ChangeCurrentToken()
        {
            if (CanGetNext())
            {
                CurrIndex += 1;
                CurrToken = tokens[CurrIndex];
                if (CurrIndex + 1 <= MaxIndex) NextToken = tokens[CurrIndex + 1];
            }
            else
            {
                throw new SyntaxErrorException();
            }
        }
        private bool CanGetNext() => CurrIndex < MaxIndex;

        private void Rer(bool get)
        {
            if (get) ChangeCurrentToken();

            //log($"<Exp>", sep1);

            if (NextToken.Type == TokenType.PowOp)
            {
                log($"<PowExp>", sep1);
                Pow(false);               
            }
            else if (NextToken.Type == TokenType.ArithOp)
            {
                log($"<Exp>", sep1);
                Exp(false);
            }
            else if (NextToken.Type == TokenType.MultOp)
            {
                log($"<MultExp>", sep1);
                Mult(false);
            }
            else
            {
                log("Syntax Error: АААААААААААА 404.");
            }
        }
        private void POW()
        {
            if (CurrToken.Type == TokenType.ID && NextToken.Type == TokenType.PowOp)
            {
                log($"IsD \"{CurrToken.Value}\"", sep1);                
                ChangeCurrentToken();                
                POW();
            }
            else if (CurrToken.Type == TokenType.PowOp)
            {
                log($"PowOp \"{CurrToken.Value}\"", sep1);                
                ChangeCurrentToken();                
                POW();
            }
            else
            {
                log("Syntax Error: Ожидался идентификатор.");
            }
        }

        private void Pow(bool get)
        {
            if (get) ChangeCurrentToken();

            if (CurrToken.Type == TokenType.ID)
            {
                log($"PowExp", sep1);
                log($"ID \"{CurrToken.Value}\"", sep1);

                Pow(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.PowOp && NextToken.Type == TokenType.PowOp)
            {
                log($"PowOp \"{CurrToken.Value}\"", sep1);
                log("Syntax Error: Ожидался идентификатор.");
            }
            else if (CurrToken.Type == TokenType.PowOp )
            {
                log($"PowOp \"{CurrToken.Value}\"", sep1);
                Pow(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.ArithOp)
            {
                Exp(true);
            }
            else if (CurrToken.Type == TokenType.MultOp)
            {
                Mult(true);
            }
            else
            {
                log("Syntax Error: Ожидался идентификатор.");
            }
        }
        private void Exp(bool get)
        {
            if (get) ChangeCurrentToken();

            if (CurrToken.Type == TokenType.ID)
            {
                log($"Exp", sep1);
                log($"ID \"{CurrToken.Value}\"", sep1);

                Exp(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.ArithOp && NextToken.Type == TokenType.ArithOp)
            {
                log($"ArithOp \"{CurrToken.Value}\"", sep1);
                log("Syntax Error: Ожидался идентификатор.");
            }
            else if (CurrToken.Type == TokenType.ArithOp)
            {
                log($"ArithOp \"{CurrToken.Value}\"", sep1);
                Exp(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.PowOp)
            {
                Pow(true);
            }
            else if (CurrToken.Type == TokenType.MultOp)
            {
                Mult(true);
            }
            else
            {
                log("Syntax Error: Ожидался идентификатор.");
            }
        }
        private void Mult(bool get)
        {
            if (get) ChangeCurrentToken();

            if (CurrToken.Type == TokenType.ID)
            {
                log($"MultExp", sep1);
                log($"ID \"{CurrToken.Value}\"", sep1);

                Mult(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.MultOp && NextToken.Type == TokenType.MultOp)
            {
                log($"MultOp \"{CurrToken.Value}\"", sep1);
                log("Syntax Error: Ожидался идентификатор.");
            }
            else if (CurrToken.Type == TokenType.MultOp)
            {
                log($"MultOp \"{CurrToken.Value}\"", sep1);
                Mult(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.PowOp)
            {
                Pow(true);
            }
            else if (CurrToken.Type == TokenType.ArithOp)
            {
                Exp(true);
            }
            else
            {
                log("Syntax Error: Ожидался идентификатор.");
            }
        }
    }
}
    

