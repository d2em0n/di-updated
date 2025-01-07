namespace TagsCloudContainer
{
    public class LabelAttribute(string labelText) : Attribute
    {
        public string LabelText { get; set; } = labelText;
    }
}
