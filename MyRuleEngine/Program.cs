using BusinessEntities;
using System;

namespace MyRuleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RuleHandler ruleHandler = new RuleHandler();
            ruleHandler.InitializeRule();
            ruleHandler.ExecuteRules();
        }
    }
}
