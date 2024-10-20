namespace DataAnnotations.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FluentAssertions;
    using Models;
    using Xunit;

    public class CompareDateGreaterThanAttributeTests
    {
        [Fact]
        public void IsValid_ShouldReturnTrue_IfValueIsGreaterThanTarget()
        {
            var sut = new TestModel()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2)
            };

            var result = this.ValidateModel(sut);

            result.Should().BeEmpty();
        }

        [Fact]
        public void IsValid_ShouldReturnError_IfValueIsNotGreaterThanTarget()
        {
            var sut = new TestModel()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(-2)
            };
            
            var result = this.ValidateModel(sut);

            result.Should().ContainSingle();
            result.Should().ContainSingle(e => e.ErrorMessage == "EndDate cannot be greater than the StartDate");
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}