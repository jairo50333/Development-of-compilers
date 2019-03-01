using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class MaxNestCyclesVisitor : AutoVisitor
    {
        private int CurNest = 0;
        public int MaxNest = 0;
        public override void VisitCycleNode(CycleNode c)
        {
            ++CurNest;
            MaxNest = Math.Max(CurNest, MaxNest);
            c.Stat.Visit(this);
            --CurNest;
        }
    }
}
