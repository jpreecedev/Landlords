namespace Model
{
    public class Checklist : BaseModel, IChecklist
    {
        public bool IsPropertyMandatory { get; set; }

        public bool IsAvailableDownstream { get; set; }

        public string Image { get; set; }
    }
}