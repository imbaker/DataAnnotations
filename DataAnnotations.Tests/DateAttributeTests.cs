
namespace DataAnnotations.Tests
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class DateAttributeTests
    {
        [Theory]
        [ClassData(typeof(DateDataGenerator))]
        public void IsValid_Works(DateAttribute.AllowedDates allowedDates, 
            (DateTime testDateTime, bool result) yesterday, 
            (DateTime testDateTime, bool result) today,
            (DateTime testDateTime, bool result) endOfToday, 
            (DateTime testDateTime, bool result) tomorrow)
        {
            var sut = new DateAttribute(allowedDates);
        
            sut.IsValid(yesterday.testDateTime).Should().Be(yesterday.result);
            sut.IsValid(today.testDateTime).Should().Be(today.result);
            sut.IsValid(endOfToday.testDateTime).Should().Be(endOfToday.result);
            sut.IsValid(tomorrow.testDateTime).Should().Be(tomorrow.result);
        }
    }

    public class DateDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
        {
            new object[]
            {
                DateAttribute.AllowedDates.IsBeforeToday,
                (TestDateTime: DateTime.Today.AddDays(-1), Result: true),
                (TestDateTime: DateTime.Today, Result: false),
                (TestDateTime: DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999),
                    Result: false),
                (TestDateTime: DateTime.Today.AddDays(1), Result: false)
            },
            new object[]
            {
                DateAttribute.AllowedDates.IsTodayAndBeforeToday,
                (TestDateTime: DateTime.Today.AddDays(-1), Result: true),
                (TestDateTime: DateTime.Today, Result: true),
                (TestDateTime: DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999),
                    Result: true),
                (TestDateTime: DateTime.Today.AddDays(1), Result: false)
            },
            new object[]
            {
                DateAttribute.AllowedDates.IsTodayOnly,
                (TestDateTime: DateTime.Today.AddDays(-1), Result: false),
                (TestDateTime: DateTime.Today, Result: true),
                (TestDateTime: DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999),
                    Result: true),
                (TestDateTime: DateTime.Today.AddDays(1), Result: false)
            },
            new object[]
            {
                DateAttribute.AllowedDates.IsTodayAndAfterToday,
                (TestDateTime: DateTime.Today.AddDays(-1), Result: false),
                (TestDateTime: DateTime.Today, Result: true),
                (TestDateTime: DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999),
                    Result: true),
                (TestDateTime: DateTime.Today.AddDays(1), Result: true)
            },
            new object[]
            {
                DateAttribute.AllowedDates.IsAfterToday,
                (TestDateTime: DateTime.Today.AddDays(-1), Result: false),
                (TestDateTime: DateTime.Today, Result: false),
                (TestDateTime: DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999),
                    Result: false),
                (TestDateTime: DateTime.Today.AddDays(1), Result: true)
            }
        };
        
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}