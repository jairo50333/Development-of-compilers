using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    public class CommonlyUsedVarVisitor : AutoVisitor
    {
        private Dictionary<string, int> varDict = new Dictionary<string, int>();

        public override void VisitVarDefNode(VarDefNode w)
        {
            foreach (var v in w.vars)
            {
                varDict.Add(v.Name, 0);
                v.Visit(this);
            }
        }

        public override void VisitIdNode(IdNode id)
        {
            ++varDict[id.Name];
        }

        public string mostCommonlyUsedVar()
        {
            return varDict.First(el => el.Value.Equals(varDict.Values.Max())).Key;
        }
    }
}
