using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Sitecore.Data.Validators;
using Sitecore.Data.Items;

namespace MySite.Validators
{
    [Serializable]
    public class MaxSliderHeightValidator : Sitecore.Data.Validators.StandardValidator
    {
        public MaxSliderHeightValidator() { }
        public MaxSliderHeightValidator(SerializationInfo info, StreamingContext context) : base (info, context)
            { }

        protected override ValidatorResult Evaluate()
        {
            Item item = base.GetItem();

            int maxHeight = Int16.Parse(item.Fields["Max Slide Height"].ToString());

            return (maxHeight > 1280 || maxHeight < 0) ? ValidatorResult.CriticalError : ValidatorResult.Valid;
        }

        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.CriticalError);
        }

        public override string Name {
            get { return "Max height cannot exceed 1280 or be less than 0"; }
        }
    }
}