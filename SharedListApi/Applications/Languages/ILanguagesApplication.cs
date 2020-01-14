using System.Collections.Generic;

namespace SharedListApi.Applications.Languages
{
    public interface ILanguagesApplication
    {
        IEnumerable<Language> List();
    }
}