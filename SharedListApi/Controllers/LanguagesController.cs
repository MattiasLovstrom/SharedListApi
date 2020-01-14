using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedListApi.Applications.Languages;

namespace SharedListApi.Controllers
{
    [Route("api/0_1/Languages")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguagesApplication _languagesApplication;

        public LanguagesController(ILanguagesApplication languagesApplication)
        {
            _languagesApplication = languagesApplication;
        }


        [HttpGet()]
        public IEnumerable<Language> Languages()
        {
            return _languagesApplication.List();
        }
    }
}

