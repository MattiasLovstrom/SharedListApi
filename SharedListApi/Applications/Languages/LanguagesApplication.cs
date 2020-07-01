using System.Collections.Generic;

namespace SharedListApi.Applications.Languages
{
    public class LanguagesApplication : ILanguagesApplication
    {
        public IEnumerable<Language> List()
        {
            var languages = new List<Language>
            {
                new Language("", "Any")
            };

            languages.AddRange(new LanguageRepository().Read(null, 0, 1000));

            return languages;
        }
    }
}
