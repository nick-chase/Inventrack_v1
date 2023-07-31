using System.Collections.Generic;
using System;

/*
 * ValidationUI()
 * Nick Chase
 * Validaiton for UI form follows 2 truths
 * 1) All required fields are filled
 * 2) All reqiered integer are positive
 */
namespace Inventrack_v1.Validation
{
    public class ValidationUI : AbstractValidator<ItemDTO>
    {
        public override List<string> Validate(ItemDTO item)
        {
            List<string> errors = new List<string>();

            // validation for required fields
            if (string.IsNullOrEmpty(item.Name))
            {
                errors.Add("Item Name missing");
            } else if (string.IsNullOrEmpty(item.CostUnit))
            {
                errors.Add("Unit cost is required");
            }
            // Range validation
            if (item.Quantity <= 0)
            {
                errors.Add("Quantity must be more then zero");
            }

            // Format validation
           /* if (item.ExpDate <= DateTime.Now)
            {
                errors.Add("Expiration date must be a future date.");
            }*/

            return errors;
        }
    }
}

