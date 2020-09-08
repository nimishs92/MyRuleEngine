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
            Payment payment = new Payment();
            payment.PaymentType = PaymentType.PHY_PROD;
            ruleHandler.ExecuteRules(payment);
        }
    }
}
