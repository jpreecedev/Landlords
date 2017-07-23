namespace Model.DataTypes
{
    public class LLDataType
    {
        public LLDataType()
        {
            
        }

        public LLDataType(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            Value = key;
            Text = key.SplitByCapitals();
        }

        public string Value { get; set; }

        public string Text { get; set; }
    }
}