﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedListApi.Applications.SharedList;

namespace SharedListApi.Controllers
{
    [Route("api/0_1/Lists")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly ISharedListsApplication _sharedListsApplication;

        public ListsController(ISharedListsApplication sharedListsApplication)
        {
            _sharedListsApplication = sharedListsApplication;
        }

        [HttpGet()]
        public IEnumerable<SharedList> Read(string listCollectionId, string id = null, int skip = 0, int take = 10)
        {
            return _sharedListsApplication.Read(listCollectionId, id).Skip(skip).Take(take);
        }

        [HttpPost]
        public ActionResult Create([FromBody] SharedList list)
        {
            var newList = _sharedListsApplication.Create(list);

            return Created("api/0_1/Lists?id=" + newList.Id, newList);
        }

        [HttpPut()]
        public ActionResult Update([FromBody] SharedList list)
        {
            var changedList = _sharedListsApplication.Update(list);

            return Ok(changedList);
        }

        [HttpDelete()]
        public void Delete(string id)
        {
            _sharedListsApplication.Delete(id);
        }
    }
}

