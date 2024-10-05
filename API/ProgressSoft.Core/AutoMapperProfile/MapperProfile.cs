using AutoMapper;
using ProgressSoft.Core.Dtos;
using ProgressSoft.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressSoft.Core.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CardReader, CardReaderDto>().ReverseMap();
            CreateMap<CreateCardReaderDto, CardReader>()
              // Ignore the Photo field since it's handled manually
              .ForMember(dest => dest.Photo, opt => opt.Ignore());
        }
    }
}
