using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class ExprComplexityVisitor : AutoVisitor
    {
        // список должен содержать сложность каждого выражения, встреченного при обычном порядке обхода AST

        private List<int> complexityList = new List<int>();

        public override void VisitBinOpNode(BinOpNode binop)
        {

            if (binop.Op == '+' || binop.Op == '-')
            {
                complexityList[complexityList.Count - 1] += 1;
            }
            else if (binop.Op == '*' || binop.Op == '/')
            {
                complexityList[complexityList.Count - 1] += 3;
            }

            binop.Left.Visit(this);
            binop.Right.Visit(this);
        }

        public override void VisitAssignNode(AssignNode a)
        {
            complexityList.Add(0);
            a.Expr.Visit(this);
        }

        public override void VisitCycleNode(CycleNode c)
        {
            complexityList.Add(0);
            c.Expr.Visit(this);
            c.Stat.Visit(this);
        }

        public override void VisitWriteNode(WriteNode w)
        {
            complexityList.Add(0);
            w.Expr.Visit(this);
        }

        public List<int> getComplexityList()
        {
            return complexityList;
        }

    }
}
