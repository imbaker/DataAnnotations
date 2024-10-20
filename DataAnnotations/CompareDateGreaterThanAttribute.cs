namespace DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public sealed class CompareDateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _targetProperty;

        public CompareDateGreaterThanAttribute(string targetProperty)
        {
            _targetProperty = targetProperty;            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentValue = (DateTime)value;
            
            PropertyInfo targetProperty = validationContext.ObjectType.GetProperty(_targetProperty);
            
            if (targetProperty.GetValue(validationContext.ObjectInstance) != null)
            {
                DateTime compareDate = (DateTime)targetProperty.GetValue(validationContext.ObjectInstance);

                if (currentValue < compareDate)
                {
                    return new ValidationResult(ErrorMessage, new [] { validationContext.MemberName });
                }
            }
            
            return ValidationResult.Success;
        }
    }
}