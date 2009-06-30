using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzAdmin.web.Models
{
    public class ConnectionParameterModel : IValidatingModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public bool IsValid
        {
            get { return this.GetRuleViolations().Count() == 0; }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(this.Key))
            {
                yield return new RuleViolation("Parameter key required", "Key");
            }

            if (String.IsNullOrEmpty(this.Value))
            {
                yield return new RuleViolation("Parameter value required", "Value");
            }
        }
    }
}
