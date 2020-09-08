using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyRuleEngine
{
    public class ExpressionTreeNode
    {
        public Expression expression { get; set; }
        public ExpressionTreeNode leftNode { get; set; }
        public ExpressionTreeNode rightNode { get; set; }

        public string NodeType { get; set; }

        public bool Evaluation { get; set; }
    }
}
