using System;

namespace People.Application.Common.Dtos;

public record PersonDetailDto(
    int Id,
    string Name,
    string Role,
    string Department,
    string Email,
    string Status,
    DateTimeOffset LastUpdatedAtUtc,
    string Source);
