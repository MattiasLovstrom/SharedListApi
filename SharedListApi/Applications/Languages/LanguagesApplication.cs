using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
