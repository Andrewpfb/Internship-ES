using MapsProject.Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace MapsProject.WEB.Controllers
{
    /// <summary>
    /// Controller for tags. Has a method for returning all categories that belong to confirmed objects.
    /// </summary>
    public class TagsController : ApiController
    {
        IMapObjectService mapObjectService;

        public TagsController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// Method for obtaining all tags of approved objects..
        /// </summary>
        /// <returns>IEnumerable(string) tags.</returns>
        public IEnumerable<string> Get()
        {
            return mapObjectService.GetAllTags();
        }
    }
}
