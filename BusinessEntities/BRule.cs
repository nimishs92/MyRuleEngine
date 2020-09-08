using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessEntities
{
    public class BRule
    {
        public ParameterExpression ParamExpr { get; set; }
        public ConstantExpression ConstExpr { get; set; }

        public string Value { get; set; }

        public bool Evaluation { get; set; }

        public delegate void RuleAction();

        public RuleAction RuleActionInstance;

        public BRule(string Constant, string Param)
        {
            this.ConstExpr = Expression.Constant(Constant, typeof(string));
            this.ParamExpr = Expression.Parameter(typeof(string), Param);

        }

        public bool EvaluateRule(string value)
        {
            BinaryExpression binaryExpression = Expression.Equal(this.ParamExpr, this.ConstExpr);
            Expression<Func<string, bool>> expression = Expression.Lambda<Func<string, bool>>(binaryExpression, new ParameterExpression[] { this.ParamExpr });
            bool result = expression.Compile()(value);
            return result;
        }
    }
}
