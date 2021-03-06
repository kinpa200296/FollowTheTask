﻿using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Role.ViewModels;
using FollowTheTask.BLL.Services.Team.ViewModels;
using FollowTheTask.BLL.Services.User.ViewModels;
using FollowTheTask.DAL.Repositories.User;
using FollowTheTask.TransferObjects.Role.DataObjects;
using FollowTheTask.TransferObjects.Team.DataObjects;
using FollowTheTask.TransferObjects.User.DataObjects;
using FollowTheTask.TransferObjects.User.Queries;

namespace FollowTheTask.BLL.Services.User
{
    public class UserService : ModelService<UserDto, UserViewModel>, IUserService
    {
        private readonly IUserRepository _repository;


        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public QueryResult<UserDto> GetUserDto(UserQuery query)
        {
            return RunQuery<UserQuery, UserDto>(_repository, query);
        }

        public async Task<QueryResult<UserDto>> GetUserDtoAsync(UserQuery query)
        {
            return await RunQueryAsync<UserQuery, UserDto>(_repository, query);
        }

        public QueryResult<UserViewModel> GetUser(UserQuery query)
        {
            return RunQuery<UserQuery, UserDto>(_repository, query).MapTo<UserViewModel>();
        }

        public async Task<QueryResult<UserViewModel>> GetUserAsync(UserQuery query)
        {
            return (await RunQueryAsync<UserQuery, UserDto>(_repository, query)).MapTo<UserViewModel>();
        }

        public ListQueryResult<RoleDto> GetUserRolesDtos(UserRolesQuery query)
        {
            return RunListQuery<UserRolesQuery, RoleDto>(_repository, query);
        }

        public async Task<ListQueryResult<RoleDto>> GetUserRolesDtosAsync(UserRolesQuery query)
        {
            return await RunListQueryAsync<UserRolesQuery, RoleDto>(_repository, query);
        }

        public ListQueryResult<RoleViewModel> GetUserRoles(UserRolesQuery query)
        {
            return RunListQuery<UserRolesQuery, RoleDto>(_repository, query).MapTo<RoleViewModel>();
        }

        public async Task<ListQueryResult<RoleViewModel>> GetUserRolesAsync(UserRolesQuery query)
        {
            return (await RunListQueryAsync<UserRolesQuery, RoleDto>(_repository, query)).MapTo<RoleViewModel>();
        }

        public ListQueryResult<TeamInfoDto> GetUserTeamsDtos(UserTeamsQuery query)
        {
            return RunListQuery<UserTeamsQuery, TeamInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<TeamInfoDto>> GetUserTeamsDtosAsync(UserTeamsQuery query)
        {
            return await RunListQueryAsync<UserTeamsQuery, TeamInfoDto>(_repository, query);
        }

        public ListQueryResult<TeamInfoViewModel> GetUserTeams(UserTeamsQuery query)
        {
            return RunListQuery<UserTeamsQuery, TeamInfoDto>(_repository, query).MapTo<TeamInfoViewModel>();
        }

        public async Task<ListQueryResult<TeamInfoViewModel>> GetUserTeamsAsync(UserTeamsQuery query)
        {
            return (await RunListQueryAsync<UserTeamsQuery, TeamInfoDto>(_repository, query)).MapTo<TeamInfoViewModel>();
        }

        public ListQueryResult<TeamInfoDto> GetLeaderTeamsDtos(LeaderTeamsQuery query)
        {
            return RunListQuery<LeaderTeamsQuery, TeamInfoDto>(_repository, query);
        }

        public async Task<ListQueryResult<TeamInfoDto>> GetLeaderTeamsDtosAsync(LeaderTeamsQuery query)
        {
            return await RunListQueryAsync<LeaderTeamsQuery, TeamInfoDto>(_repository, query);
        }

        public ListQueryResult<TeamInfoViewModel> GetLeaderTeams(LeaderTeamsQuery query)
        {
            return RunListQuery<LeaderTeamsQuery, TeamInfoDto>(_repository, query).MapTo<TeamInfoViewModel>();
        }

        public async Task<ListQueryResult<TeamInfoViewModel>> GetLeaderTeamsAsync(LeaderTeamsQuery query)
        {
            return (await RunListQueryAsync<LeaderTeamsQuery, TeamInfoDto>(_repository, query)).MapTo<TeamInfoViewModel>();
        }

        public QueryResult<CreateIssueAllowedDto> IsCreateIssueAllowed(CreateIssueAllowedQuery query)
        {
            return RunQuery<CreateIssueAllowedQuery, CreateIssueAllowedDto>(_repository, query);
        }

        public async Task<QueryResult<CreateIssueAllowedDto>> IsCreateIssueAllowedAsync(CreateIssueAllowedQuery query)
        {
            return await RunQueryAsync<CreateIssueAllowedQuery, CreateIssueAllowedDto>(_repository, query);
        }
    }
}