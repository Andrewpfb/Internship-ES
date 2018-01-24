using MapsProject.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MapsProject.Service.Interfaces;
using MapsProject.Service.Infrastructure;
using AutoMapper;
using System;

namespace MapsProject.WEB.Controllers
{

    public class TestController : ApiController
    {
        private IMapObjectService mapObjectService;

        public TestController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        public string Get()
        {
            try
            {
                IEnumerable<Service.Models.MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllApprovedMapObjects("");
                //Mapper.Initialize(cfg => cfg.CreateMap<Service.Models.MapObjectDTO, MapObjectViewModel>());
                //var mapObjects = Mapper
                //    .Map<IEnumerable<Service.Models.MapObjectDTO>, List<MapObjectViewModel>>(mapObjectsDTOs);
                // return productRepo.hello();
                return mapObjectsDTOs.ToList()[0].Status;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
