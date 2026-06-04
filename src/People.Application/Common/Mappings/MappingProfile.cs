using AutoMapper;
using People.Application.Common.Dtos;
using People.Domain.Entities;

namespace People.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonSummaryDto>();
        CreateMap<Person, PersonDetailDto>()
            .ConstructUsing(src => new PersonDetailDto(
                src.Id,
                src.Name,
                src.Role,
                src.Department,
                src.Email,
                src.Status,
                src.LastUpdatedAtUtc,
                "azure-sql-service")); // Source is hardcoded for this MS
    }
}
