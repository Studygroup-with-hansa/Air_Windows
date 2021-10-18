namespace Studygroup_with_Hansa.Models
{
    public class ParamModel
    {
        public ParamModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}