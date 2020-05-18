using System.Collections.Generic;

namespace SharedListApi.Applications.Languages
{
    public class LanguagesApplication : ILanguagesApplication
    {
        public IEnumerable<Language> List()
        {
            return new Language[] {
                    new Language("", "Any"),
                    new Language("sv", "Sveska"),
                    new Language("en", "English")
                };
        }
    }
}
