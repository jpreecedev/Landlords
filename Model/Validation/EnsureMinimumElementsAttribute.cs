﻿namespace Model.Validation
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minElements;

        public EnsureMinimumElementsAttribute(int minElements)
        {
            _minElements = minElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count >= _minElements;
            }
            if (list == null)
            {
                return true;
            }
            return false;
        }
    }
}