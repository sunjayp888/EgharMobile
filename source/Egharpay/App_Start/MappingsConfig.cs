using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Egharpay.Business.Models;
using Egharpay.Models;

namespace Egharpay.App_Start
{
    public class MappingsConfig
    {
        public static IMapper Initialize()
        {
            var mapperConfiguration = new MapperConfiguration(Configure);

            return mapperConfiguration.CreateMapper();
        }

        private static void Configure(IMapperConfigurationExpression config)
        {

        }
    }
}
