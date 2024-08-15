using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Entities;

namespace Workspace.DAL
{
    public class InviteRepository : BaseRepository, IInviteRepository
    {
        private readonly ILogger _logger;

        public InviteRepository(ILogger<InviteRepository> logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            _logger = logger;
        }

        public async Task<Guid> CheckInviteAsync(Guid id)
        {
            var sql = "SELECT * FROM public.check_invite(@martid)";

            var param = new DynamicParameters();
            param.Add("@martid", id);

            var result = await QuerySingleAsync<Guid>(sql, param);

            //inviteDTO.Id = param.Get<Guid>("@invite_id");
            //inviteDTO.UserId = param.Get<Guid>("@userid");

            return result;
        }

        public async Task<InviteDTO> CreateInviteAsync(InviteDTO inviteDTO)
        {
            using var connection = GetConnection();

            var sql = "public.create_invite";

            var param = new DynamicParameters();
            param.Add("@inv_comments", inviteDTO.InviteText);
            param.Add("@martid", inviteDTO.MartId);
            param.Add("@isopened", inviteDTO.IsOppened);
            param.Add("@invite_id", dbType: DbType.Guid, direction: ParameterDirection.Output);
            param.Add("@userid", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

            inviteDTO.Id = param.Get<Guid>("@invite_id");
            inviteDTO.UserId = param.Get<Guid>("@userid");

            return inviteDTO;
        }
        
        public async Task AcceptInviteAsync(InviteDetailDTO inviteDetailDTO)
        {
            using var connection = GetConnection();

            var sql = "public.create_invite_detail";

            var param = new DynamicParameters();
            param.Add("@users_comments", inviteDetailDTO.Comments);
            param.Add("@inviteid", inviteDetailDTO.InviteID);
            param.Add("@userid", inviteDetailDTO.UserID);

            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
        }
        
        //Данный метод возвращает все пришлашения для юреза, который захочет 
        //участвовать в проекте
        public async Task<IEnumerable<InviteRollDTO>> GetAllInvitesAsync()
        {
            //using var connection = GetConnection();

            var sql = "select * from public.get_all_invites()";
            
            return await QueryAsync<InviteRollDTO>(sql);
        }

        //Данный метод возвращает пользователей которые согласились участвоать в mart
        public async Task<IEnumerable<InviteRollDTO>> GetAcceptedInvitesAsync(Guid martid)
        {
           // using var connection = GetConnection();
            var sql = "SELECT * FROM public.get_invite_users(@wmart_id)";

            var param = new DynamicParameters();
            param.Add("@wmart_id", martid);

            return await QueryAsync<InviteRollDTO>(sql, param);
        }
    }
}
