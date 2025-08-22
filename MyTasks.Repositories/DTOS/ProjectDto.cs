﻿namespace MyTasks.Repositories.DTOS
{
    public record ProjectDto(
     Guid Id,
     string Name,
     string? Description,
     int TaskCount,
     int CompletedTasks);
}