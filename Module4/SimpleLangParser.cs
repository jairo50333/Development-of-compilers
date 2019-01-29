﻿using System;
using System.Collections.Generic;
using System.Text;
using SimpleLexer;
namespace SimpleLangParser
{
    public class ParserException : System.Exception
    {
        public ParserException(string msg)
            : base(msg)
        {
        }

    }

    public class Parser
    {
        private SimpleLexer.Lexer l;

        public Parser(SimpleLexer.Lexer lexer)
        {
            l = lexer;
        }

        public void Progr()
        {
            Block();
        }

        public void Expr()
        {
            E();
        }

        public void Assign() 
        {
            l.NextLexem();  // пропуск id
            if (l.LexKind == Tok.ASSIGN)
            {
                l.NextLexem();
            }
            else {
                SyntaxError(":= expected");
            }
            Expr();
        }

        public void StatementList() 
        {
            Statement();
            while (l.LexKind == Tok.SEMICOLON)
            {
                l.NextLexem();
                Statement();
            }
        }

        public void Statement() 
        {
            switch (l.LexKind)
            {
                case Tok.BEGIN:
                    {
                        Block(); 
                        break;
                    }
                case Tok.CYCLE:
                    {
                        Cycle(); 
                        break;
                    }
                case Tok.WHILE:
                    {
                        While();
                        break;
                    }
                case Tok.FOR:
                    {
                        For();
                        break;
                    }
                case Tok.IF:
                    {
                        If();
                        break;
                    }
                case Tok.ID:
                    {
                        Assign();
                        break;
                    }
                default:
                    {
                        SyntaxError("Operator expected");
                        break;
                    }
            }
        }

        public void Block() 
        {
            l.NextLexem();    // пропуск begin
            StatementList();
            if (l.LexKind == Tok.END)
            {
                l.NextLexem();
            }
            else
            {
                SyntaxError("end expected");
            }
        }

        public void Cycle() 
        {
            l.NextLexem();  // пропуск cycle
            Expr();
            Statement();
        }

        public void While()
        {
            l.NextLexem();// пропуск while
            Expr();
            if (l.LexKind == Tok.DO)
                l.NextLexem();
            else
                SyntaxError("do expected");
            Statement();      
        }

        public void For()
        {
            l.NextLexem(); //пропуск for 
            Assign();
            if (l.LexKind == Tok.TO)
            {
                l.NextLexem();
            }
            else
            {
                SyntaxError("to expected");
            }
            Expr();
            if (l.LexKind == Tok.DO)
            {
                l.NextLexem();
            }
            else
            {
                SyntaxError("do expected");
            }
            Statement();
        }

        public void If()
        {
            l.NextLexem(); //пропуск if 
            Expr();
            if (l.LexKind == Tok.THEN)
            {
                l.NextLexem();
            }
            else
            {
                SyntaxError("then expected");
            }
            Statement();
            if (l.LexKind == Tok.ELSE)
            {
                l.NextLexem();
                Statement();
            }
        }
        
        //E ::= T A
        public void E()
        {
            T();
            A();
        }

        //T ::= M B
        public void T()
        {
            M();
            B();
        }

        //M ::= id | num | (E)
        public void M()
        {
            if (l.LexKind == Tok.ID || l.LexKind == Tok.INUM)
            {
                    l.NextLexem();
             }
             else if (l.LexKind == Tok.LEFT_BRACKET)
            {
                l.NextLexem();
                Expr();
                if (l.LexKind == Tok.RIGHT_BRACKET)
                {
                    l.NextLexem();
                }
                else
                {
                    SyntaxError(") expected");
                }
            }
            else
            {
                SyntaxError("expression expected");
            }
        }

        //A::= ε | + T A | - T A
        public void A()
        {
            while (l.LexKind == Tok.PLUS || l.LexKind == Tok.MINUS)
            {
                l.NextLexem();
                T();
                A();
            }
        }

        //B ::= ε | * M B | / M B
        public void B()
        {
            while (l.LexKind == Tok.MULT || l.LexKind == Tok.DIVISION)
            {
                l.NextLexem();
                M();
                B();
            }
        }

        public void SyntaxError(string message) 
        {
            var errorMessage = "Syntax error in line " + l.LexRow.ToString() + ":\n";
            errorMessage += l.FinishCurrentLine() + "\n";
            errorMessage += new String(' ', l.LexCol - 1) + "^\n";
            if (message != "")
            {
                errorMessage += message;
            }
            throw new ParserException(errorMessage);
        }
   
    }
}
