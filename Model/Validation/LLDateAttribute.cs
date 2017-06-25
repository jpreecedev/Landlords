namespace Model.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LLDateAttribute : RangeAttribute
    {
        public LLDateAttribute() : base(typeof(DateTime), "1/1/1900", "6/6/2079")
        {
        }
    }
}