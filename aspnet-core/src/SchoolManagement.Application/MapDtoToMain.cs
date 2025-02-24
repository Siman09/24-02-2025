using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;

namespace SchoolManagement
{
    public class MapDtoToMain<TEntity,TEntityDto>:SchoolManagementAppServiceBase, IMapDtoToMain<TEntity, TEntityDto>
    {
        public MapDtoToMain()
        {
            
        }
        public TEntityDto MapToEntityDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntityDto>(entity);
        }
    }
}
