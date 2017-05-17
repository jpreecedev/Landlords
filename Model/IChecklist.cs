namespace Model
{
    public interface IChecklist
    {
        bool IsPropertyMandatory { get; set; }

        bool IsAvailableDownstream { get; set; }

        string Image { get; set; }
    }
}