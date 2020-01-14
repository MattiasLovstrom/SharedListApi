using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SharedListApi.Applications.ListCollection;

namespace SharedListApi.Controllers
{
    [Route("api/0_1/ListCollections")]
    [ApiController]
    public class ListCollectionsController : ControllerBase
    {
        private readonly IListCollectionsApplication _listCollectionsApplication;

        public ListCollectionsController(IListCollectionsApplication listCollectionsApplication)
        {
            _listCollectionsApplication = listCollectionsApplication;
        }

        [HttpGet()]
        public IEnumerable<ListCollection> ListCollections(string id = "")
        {
            return _listCollectionsApplication.List();
        }

        [HttpPost]
        public CreatedResult Post([FromBody] ListCollection collection)
        {
            var newCollection = _listCollectionsApplication.Create(collection.Name);

            return Created("/api/0_1/ListCollections?id=" + newCollection.Id, newCollection);
        }

        [HttpPut()]
        public void Put(string id, [FromBody] ListCollection value)
        {
            
        }

        [HttpDelete()]
        public void Delete(string id)
        {
            _listCollectionsApplication.Delete(id);
        }
    }
}

