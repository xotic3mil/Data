﻿using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRespository : IBaseRepository<ProjectEntity>
{
    public Task<IEnumerable<ProjectEntity>> GetProjectsWithDetails();

    public Task<IEnumerable<ProjectEntity>> SearchProjectsAsync(string? search);

}
