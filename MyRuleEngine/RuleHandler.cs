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
            BRule Rule = new BRule(PaymentType.PHY_PROD.ToString(), new Payment().PaymentType.GetType().Name);
            Packaging packaging = new Packaging();
            Rule.RuleActionInstance += packaging.GeneratePackagingSlip;
            Rule.RuleActionInstance += packaging.CommissionToAgent;
            bRules.Add(Rule);
        }

        public void ExecuteRules(Payment payment)
        {   
            foreach (var bRule in bRules)
            {
                var value = payment.GetType().GetProperty(bRule.ParamExpr.Name).GetValue(payment).ToString();
                if (bRule.EvaluateRule(payment.PaymentType.ToString()))
                {
                    bRule.RuleActionInstance();
                }
            }
        }
    }
}
