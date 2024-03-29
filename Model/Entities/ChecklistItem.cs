﻿namespace Model.Entities
{
    using System;
    using Validation;

    public class ChecklistItem : BaseModel
    {
        [RequiredGuid]
        public Guid ChecklistId { get; set; }

        public string DisplayText { get; set; }

        public string Key { get; set; }

        public bool IsExpanded { get; set; }

        public string Template { get; set; }

        public int Order { get; set; }

        public string Payload { get; set; }
    }
}