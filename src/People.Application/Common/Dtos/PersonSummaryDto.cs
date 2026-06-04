namespace People.Application.Common.Dtos;

public record PersonSummaryDto(
    int Id,
    string Name,
    string Role,
    string Department,
    string Email,
    string Status);
