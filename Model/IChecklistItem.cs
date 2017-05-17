namespace Model
{
    using System;

    public interface IChecklistItem
    {   
        string DisplayText { get; set; }

        string Key { get; set; }

        bool IsExpanded { get; set; }

        string Template { get; set; }

        bool IsCompleted { get; set; }
    }
}