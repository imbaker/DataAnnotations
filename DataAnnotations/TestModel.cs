namespace DataAnnotations
{
    using System;

    public class TestModel
    {
        public DateTime StartDate { get; set; }
        
        [CompareDateGreaterThan(nameof(StartDate), ErrorMessage = "EndDate cannot be greater than the StartDate")]
        public DateTime EndDate { get; set; }
        
    }
}