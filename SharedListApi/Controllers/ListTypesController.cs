using Microsoft.AspNetCore.Mvc;
using SharedListApi.Applications.ListTypesApplication;
using System.Collections.Generic;

namespace SharedListApi.Controllers
{
    [Route("api/0_1/ListTypes")]
    [ApiController]
    public class ListTypesController : ControllerBase
    {
        private readonly IListTypesApplication _listTypesApplication;

        public ListTypesController(IListTypesApplication listTypesApplication)
        {
            _listTypesApplication = listTypesApplication;
        }

        [HttpGet()]
        public IEnumerable<ListType> Read(string id = null, int skip = 0, int take = 10)
        {
            return _listTypesApplication.Read(id, skip, take);
        }

        [HttpPost]
        public ActionResult Create([FromBody] ListType listType)
        {
            var newListType = _listTypesApplication.Create(listType);

            return Created("api/0_1/ListTypes?id=" + newListType.Id, newListType);
        }

        [HttpPut()]
        public ActionResult Update([FromBody] ListType listType)
        {
            var changedList = _listTypesApplication.Update(listType);

            return Ok(changedList);
        }

        [HttpDelete()]
        public void Delete(string id)
        {
            _listTypesApplication.Delete(id);
        }
    }
}

