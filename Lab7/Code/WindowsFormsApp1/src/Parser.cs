using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.src
{
    internal class Parser
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
            if (MaxIndex > 1) NextToken = tokens[CurrIndex + 1];            
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

                CurrIndex++;
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
            

            if (CurrToken.Type == TokenType.ID )
            {
                if (NextToken.Type == TokenType.PowOp)
                {
                    log($"<PowExpr>", sep1);
                    log($"ID \"{CurrToken.Value}\"", sep1);
                    //log("PowExp", sep1);
                    Pow(true);
                }
                if (NextToken.Type == TokenType.MultOp)
                {
                    log($"<MultExpr>", sep1);
                    log($"ID \"{CurrToken.Value}\"", sep1);
                    //log("PowExp", sep1);
                    Mult(true);
                }
                if (NextToken.Type == TokenType.ArithOp)
                {
                    log($"<Expr>", sep1);
                    log($"ID \"{CurrToken.Value}\"", sep1);
                    //log("PowExp", sep1);
                    Exp(true);
                }
            }
            else
            {
                //log("Syntax Error: Ожидалось ключевое слово \"ЖОПА\".");
            }
        }

        private void While(bool get)
        {
            if (get) ChangeCurrentToken();
            log("<EXP>", "");

            if (CurrToken.Type == TokenType.Exp)
            {
                log("Exp", sep1);
                Exp(true);

                if (CurrToken.Type == TokenType.Multexp)
                {
                    log("MultExp", sep1);
                    Mult(true);
                    if (CurrToken.Type == TokenType.Powexp)
                    {
                        log("PowExp", sep1);
                        Pow(true);
                        if (CurrToken.EndIndex == MaxIndex)
                        {                            
                            log("\n", "\n");
                            if (CanGetNext())
                            {
                                While(true);
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //log("Syntax Error: Ожидалось ключевое слово \"end\".");
                    }
                }
                else
                {
                    //log("Syntax Error: Ожидалось ключевое слово \"do\".");
                }
            }
            else
            {
                //log("Syntax Error: Ожидалось ключевое слово \"while\".");
            }
        }
        private void Mult(bool get)
        {
            if (get) ChangeCurrentToken();
            if (CurrToken.Type == TokenType.ID)
            {
                log($"ID \"{CurrToken.Value}\"", sep1);
                Mult(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.MultOp)
            {
                log($"MultOp \"{CurrToken.Value}\"", sep1);
                Mult(true);
                ChangeCurrentToken();
            }
            else
            {
                log("Syntax Error: Ожидался идентификатор.");
            }
        }
        private void Pow(bool get)
        {
            if (get) ChangeCurrentToken();     
            
            if (CurrToken.Type == TokenType.ID )
            {
                log($"ID \"{CurrToken.Value}\"", sep1);
                
                Pow(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.ID && NextToken.Type != TokenType.Powexp)
            {
                
                Rer(false);
            }
            //else if (CurrToken.Type == TokenType.PowOp && NextToken.Type == TokenType.PowOp)
            //{
            //    log($"PowOp \"{CurrToken.Value}\"", sep1);
            //    log("Syntax Error: Ожидался идентификатор.");
            //}
            else if (CurrToken.Type == TokenType.PowOp)
            {
                log($"PowOp \"{CurrToken.Value}\"", sep1);
                Pow(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.MultOp && CurrToken.Type == TokenType.ArithOp)
            {
                log("Bsssss.");
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
                log($"ID \"{CurrToken.Value}\"", sep1);
                Exp(true);
                ChangeCurrentToken();
            }
            else if (CurrToken.Type == TokenType.ArithOp)
            {
                log($"ExpOp \"{CurrToken.Value}\"", sep1);
                Exp(true);
                ChangeCurrentToken();
            }
            else
            {
                log("Syntax Error: Ожидался идентификатор.");
            }
        }
        
        private void LogExpr(bool get)
        {
            if (get) ChangeCurrentToken();

            log("<>", sep1);

            RelExpr(false);
            if (CurrToken.Type == TokenType.Null)
            {
                log("", sep1);
                LogExpr(true);
            }
        }
        private void RelExpr(bool get, bool isFirstOrSecond = true)
        {
            if (get) ChangeCurrentToken();

            log("<>", sep1);

            Operand(false);
            ChangeCurrentToken();
            if (CurrToken.Type == TokenType.Null)
            {
                log($" \"{CurrToken.Value}\"", sep1);

                RelExpr(true, false);
            }
            else if (isFirstOrSecond)
            {
                log("Syntax Error: Ожидалось хотя бы одно законченное логическое выражение.");
            }
        }
        

        private void ArithExpr(bool get)
        {
            if (get) ChangeCurrentToken();
            log("<ArithExpr>", sep1);
            Operand(false);
            ChangeCurrentToken();
            if (CurrToken.Type == TokenType.ArithOp)
            {
                log($"ArithOperator \"{CurrToken.Value}\"", sep1);
                ArithExpr(true);
            }
        }
        private void PowExpr(bool get)
        {
            if (get) ChangeCurrentToken();
            log("<PowExpr>", sep1);
            Operand(false);
            ChangeCurrentToken();
            if (CurrToken.Type == TokenType.PowOp)
            {
                log($"PowOperator \"{CurrToken.Value}\"", sep1);
                PowExpr(true);
            }
        }
        private void MultExpr(bool get)
        {
            if (get) ChangeCurrentToken();
            log("<MultExpr>", sep1);
            Operand(false);
            ChangeCurrentToken();
            if (CurrToken.Type == TokenType.MultOp)
            {
                log($"MultOperator \"{CurrToken.Value}\"", sep1);
                MultExpr(true);
            }
        }
        private void Operand(bool get)
        {
            if (get) ChangeCurrentToken();

            log("<Operand>", sep1);

            switch (CurrToken.Type)
            {
                case TokenType.ID:
                    log($"ID \"{CurrToken.Value}\"", sep1);
                    break;
                default:
                    log("Syntax Error: Ожидался идентификатор.");
                    break;
            }
        }
    }
}
