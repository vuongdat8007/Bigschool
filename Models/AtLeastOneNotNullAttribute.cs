using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    public class AtLeastOneNotNullAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public AtLeastOneNotNullAttribute(params string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (var propertyName in _propertyNames)
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                if (property == null)
                {
                    return new ValidationResult($"Property '{propertyName}' not found.");
                }

                var propertyValue = property.GetValue(validationContext.ObjectInstance);
                if (propertyValue != null)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("At least one of the specified properties must have a non-null value.");
        }
    }
}