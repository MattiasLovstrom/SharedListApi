namespace SharedListApi.Applications.Languages
{
    public class Language
    {
        public Language(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; set; }
        public string Name { get; set; }
    }
}