using System;
using System.Collections.Generic;
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
        public IEnumerable<ListCollection> ListCollections(string id = "", int skip = 0, int take = 10)
        {
            return _listCollectionsApplication.List(id, skip, take);
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

