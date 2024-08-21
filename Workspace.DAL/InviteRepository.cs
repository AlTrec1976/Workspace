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
            try
            {
                var sql = "SELECT * FROM public.check_invite(@martid)";

                var param = new DynamicParameters();
                param.Add("@martid", id);

                var result = await QuerySingleAsync<Guid>(sql, param);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CheckInviteAsync");
                throw;
            }
        }

        public async Task<InviteDTO> CreateInviteAsync(InviteDTO inviteDTO)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CreateInviteAsync");
                throw;
            }
        }
        
        public async Task AcceptInviteAsync(InviteDetailDTO inviteDetailDTO)
        {
            try
            {
                using var connection = GetConnection();

                var sql = "public.create_invite_detail";

                var param = new DynamicParameters();
                param.Add("@users_comments", inviteDetailDTO.Comments);
                param.Add("@inviteid", inviteDetailDTO.InviteID);
                param.Add("@userid", inviteDetailDTO.UserID);

                await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в AcceptInviteAsync");
                throw;
            }
        }
        
        //Данный метод возвращает все пришлашения для юреза, который захочет 
        //участвовать в проекте
        public async Task<IEnumerable<InviteRollDTO>> GetAllInvitesAsync()
        {
            try
            {
                var sql = "select * from public.get_all_invites()";
            
                return await QueryAsync<InviteRollDTO>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetAllInvitesAsync");
                throw;
            }
        }

        //Данный метод возвращает пользователей которые согласились участвоать в mart
        public async Task<IEnumerable<InviteRollDTO>> GetAcceptedInvitesAsync(Guid martid)
        {
            try
            {
                var sql = "SELECT * FROM public.get_invite_users(@wmart_id)";

                var param = new DynamicParameters();
                param.Add("@wmart_id", martid);

                return await QueryAsync<InviteRollDTO>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в GetAcceptedInvitesAsync");
                throw;
            }
        }
    }
}
