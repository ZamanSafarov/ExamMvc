using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Concretes;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _repository;
    private readonly IWebHostEnvironment _env;
    public TeamService(ITeamRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    public async Task CreateTeamAsync(Team team)
    {
        if (team.ImageFile is null)
        {
            throw new FileRequiredException("File Cannot Be Null!");
        }
        team.ImageUrl =  _env.ImageAdd("uploads\\teams",team.ImageFile,"team");

       await _repository.AddEntityAsync(team);
        await _repository.CommitAsync();
    }

    public void DeleteTeam(int id)
    {
        var team = _repository.GetEntity(x => x.DeletedDate == null && x.Id == id);
        if (team is null)
        {
            throw new EntityNullException("Team Not Found!");
        }
        _repository.Commit();
    }

    public List<Team> GetAllTeam(Func<Team, bool>? func = null)
    {
        return _repository.GetAllEntity(func);
    }

    public Task<IPagedList<Team>> GetPagedTeamsAsync(int pageIndex, int pageSize)
    {
        return _repository.GetPagedTeamsAsync(pageIndex, pageSize);
    }

    public Team GetTeam(Func<Team, bool>? func = null)
    {
        return _repository.GetEntity(func);
    }

    public void UpdateTeam(int id, Team team)
    {
        var oldTeam = _repository.GetEntity(x => x.DeletedDate == null && x.Id == id);
        if (oldTeam is null)
        {
            throw new EntityNullException("Team Not Found!");
        }

        if (team.ImageFile is not null)
        {
            team.ImageUrl = _env.ImageAdd("uploads\\teams", team.ImageFile, "team");
            _env.ArchiveImage("uploads\\teams",oldTeam.ImageUrl);
            oldTeam.ImageUrl = team.ImageUrl;
        }
        oldTeam.XUrl = team.XUrl;
        oldTeam.Name = team.Name;
        oldTeam.FacebookUrl = team.FacebookUrl;
        oldTeam.Position = team.Position;
        oldTeam.LinkedUrl = team.LinkedUrl;

        _repository.Commit();
    }
}
