//using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            config.CreateMap<Entity.DocumentCategory, DocumentCategory>();
            config.CreateMap<DocumentCategory, Entity.DocumentCategory>();

            config.CreateMap<Document, Entity.DocumentDetail>()
                .ForMember(destination => destination.Description, source => source.MapFrom(s => s.Description));
            ;
            config.CreateMap<Entity.DocumentDetail, Document>();
        }
    }
}
