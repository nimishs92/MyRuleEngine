using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyRuleEngine
{
    public class ExpressionTreeHandler
    {
        private static ExpressionTreeNode CreateTree(string testExpr)
        {
            string[] TestExprArr = System.Text.RegularExpressions.Regex.Split(testExpr, @"[<=>&]");
            System.Text.RegularExpressions.MatchCollection TestExprOperandCollection = System.Text.RegularExpressions.Regex.Matches(testExpr, @"[<=>&]");

            List<string> InfixExpr = new List<string>();
            int OperandCount = 0;
            for (int i = 0; i < TestExprArr.Length; i++)
            {
                InfixExpr.Add(TestExprArr[i]);
                if (OperandCount < TestExprOperandCollection.Count)
                {
                    InfixExpr.Add(TestExprOperandCollection[OperandCount++].Value);
                }
            }
            List<string> PostFix = new List<string>();
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < InfixExpr.Count; i++)
            {
                if (isOperator(InfixExpr[i]))
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(InfixExpr[i]);
                    }
                    else
                    {
                        if (GetPrecedence(InfixExpr[i]) > GetPrecedence(stack.Peek()))
                        {
                            PostFix.Add(stack.Pop());
                            stack.Push(InfixExpr[i]);
                        }
                        else
                        {
                            stack.Push(InfixExpr[i]);
                        }
                    }
                }
                else
                {
                    PostFix.Add(InfixExpr[i]);
                }
            }

            while (stack.Count != 0)
            {
                PostFix.Add(stack.Pop());
            }

            ExpressionTreeNode rootNode = new ExpressionTreeNode();
            Stack<ExpressionTreeNode> operatorStack = new Stack<ExpressionTreeNode>();
            for (int i = 0; i < PostFix.Count; i++)
            {
                ExpressionTreeNode t = new ExpressionTreeNode(), t1, t2;
                if (i % 3 == 0)
                {
                    t = new ExpressionTreeNode();
                    t.expression = Expression.Parameter(typeof(string), PostFix[i]);
                }
                else if (i % 3 == 1)
                {
                    t = new ExpressionTreeNode();
                    t.expression = Expression.Constant(PostFix[i]);
                }
                else if (i % 3 == 2)
                {
                    t = new ExpressionTreeNode();
                }

                if (!isOperator(PostFix[i]))
                {
                    operatorStack.Push(t);
                }
                else
                {
                    t1 = operatorStack.Pop();
                    t2 = operatorStack.Pop();

                    t.rightNode = t1;
                    t.leftNode = t2;

                    if (PostFix[i] == "=")
                    {
                        t.expression = Expression.Equal(t.leftNode.expression, t.rightNode.expression);
                        t.NodeType = "BinaryOperator";
                    }
                    else if (PostFix[i] == "&")
                    {
                        t.expression = Expression.AndAlso(Expression.Parameter(typeof(bool), "leftParam"), Expression.Parameter(typeof(bool), "rightParam"));
                        t.NodeType = "ConditionalOperator";
                    }

                    operatorStack.Push(t);
                }
            }

            rootNode = operatorStack.Pop();
            return rootNode;
        }

        //private static Expression GetInfixExpressionFromString(string expr)
        //{
        //    new 
        //}

        private static int GetPrecedence(string oper)
        {
            System.Text.RegularExpressions.Regex comparisonRegex = new System.Text.RegularExpressions.Regex(@"[<=>]");
            if (comparisonRegex.IsMatch(oper))
            {
                return 1;
            }
            System.Text.RegularExpressions.Regex logicalRegex = new System.Text.RegularExpressions.Regex(@"[&]");
            if (logicalRegex.IsMatch(oper))
            {
                return 2;
            }
            return 0;
        }

        private static bool isOperator(string expr)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[<=>&]");
            return regex.IsMatch(expr);
        }
    }
}
