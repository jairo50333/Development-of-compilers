using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class CountCyclesOpVisitor : AutoVisitor
    {
        private int cycleCnt = 0;
        private int depth = 0;
        private int opCnt = 0;

        public int MidCount()
        {
            return cycleCnt == 0 ? 0 : opCnt / cycleCnt;
        }

        public override void VisitCycleNode(CycleNode c)
        {
            ++cycleCnt;
            --depth;
            c.Stat.Visit(this);
            ++depth;
        }

        public override void VisitAssignNode(AssignNode a)
        {
            if (depth < 0)
            {
                ++opCnt;
            }
        }

        public override void VisitVarDefNode(VarDefNode w)
        {
            if (depth < 0)
            {
                ++opCnt;
            }
        }

        public override void VisitWriteNode(WriteNode w)
        {
            if (depth < 0)
            {
                ++opCnt;
            }
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            if (depth < 0)
            {
                ++opCnt;
            }
        }

    }
}
