﻿namespace Watchdog.Entities
{
    public class NumericRule : Rule
    {
        public double NumericValue { get; }

        public NumericRule(double numericValue, RuleKind ruleKind) : base(ruleKind)
        {
            NumericValue = numericValue;
        }
    }
}
