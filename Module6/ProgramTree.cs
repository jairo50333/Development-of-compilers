using System.Collections.Generic;

namespace ProgramTree
{
    public enum AssignType { Assign, AssignPlus, AssignMinus, AssignMult, AssignDivide };

    public class Node // базовый класс для всех узлов    
    {
    }

    public class ExprNode : Node // базовый класс для всех выражений
    {
    }

    public class IdNode : ExprNode
    {
        public string Name { get; set; }
        public IdNode(string name) { Name = name; }
    }

    public class IntNumNode : ExprNode
    {
        public int Num { get; set; }
        public IntNumNode(int num) { Num = num; }
    }

    public class StatementNode : Node // базовый класс для всех операторов
    {
    }

    public class AssignNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode Expr { get; set; }
        public AssignType AssOp { get; set; }
        public AssignNode(IdNode id, ExprNode expr, AssignType assop = AssignType.Assign)
        {
            Id = id;
            Expr = expr;
            AssOp = assop;
        }
    }

    public class CycleNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public CycleNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class BlockNode : StatementNode
    {
        public List<StatementNode> StList = new List<StatementNode>();
        public BlockNode(StatementNode stat)
        {
            Add(stat);
        }
        public void Add(StatementNode stat)
        {
            StList.Add(stat);
        }
    }

    public class WhileNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode Stat { get; set; }
        public WhileNode(ExprNode expr, StatementNode stat)
        {
            Expr = expr;
            Stat = stat;
        }
    }

    public class RepeatNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public BlockNode StatList { get; set; }
        public RepeatNode(BlockNode stat_list, ExprNode expr)
        {
            StatList = stat_list;
            Expr = expr;
        }
    }

    public class ForNode : StatementNode
    {
        public IdNode Id { get; set; }
        public ExprNode IdExpr { get; set; }
        public ExprNode ToExpr { get; set; }
        public StatementNode Stat { get; set; }
        public ForNode(IdNode id, ExprNode id_expr, ExprNode to_expr, StatementNode stat)
        {
            Id = id;
            IdExpr = id_expr;
            ToExpr = to_expr;
            Stat = stat;
        }
    }

    public class WriteNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public WriteNode(ExprNode expr)
        {
            Expr = expr;
        }
    }

    public class IfNode : StatementNode
    {
        public ExprNode Expr { get; set; }
        public StatementNode ThenState { get; set; }
        public StatementNode ElseState { get; set; }
        public IfNode(ExprNode expr, StatementNode then_state, StatementNode else_state = null)
        {
            Expr = expr;
            ThenState = then_state;
            ElseState = else_state;
        }
    }

    public class VarDefNode : StatementNode
    {
        public List<IdNode> id_list { get; set; }
        public VarDefNode(IdNode id)
        {
            Add(id);
        }
        public void Add(IdNode id)
        {
            id_list.Add(id);
        }
    }

    public class BinaryNode : ExprNode
    {
        public ExprNode Left { get; set; }
        public ExprNode Right { get; set; }
        public char Oper { get; set; }
        public BinaryNode(ExprNode left, ExprNode right, char oper)
        {
            Left = left;
            Right = right;
            Oper = oper;
        }
    }


}