using BusinessEntities;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRuleEngine
{
    public class RuleHandler
    {
        List<BRule> bRules = new List<BRule>();
        public void InitializeRule()
        {
            BRule Rule = new BRule(PaymentType.PHY_PROD.ToString());
            Packaging packaging = new Packaging();
            Rule.RuleActionInstance += packaging.GeneratePackagingSlip;
            Rule.RuleActionInstance += packaging.CommissionToAgent;
            bRules.Add(Rule);
        }

        public void ExecuteRules()
        {
            Payment payment = new Payment();
            payment.PaymentType = PaymentType.PHY_PROD;
            foreach (var bRule in bRules)
            {
                if (bRule.EvaluateRule(payment.PaymentType.ToString()))
                {
                    bRule.RuleActionInstance();
                }
            }
        }
    }
}
