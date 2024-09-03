using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Workspace.Entities;

namespace Workspace.DAL
{
    public class InviteRepository(ILogger<InviteRepository> logger, IConfiguration configuration) 
        : BaseRepository(logger, configuration), IInviteRepository
    {
        private readonly ILogger _loggerr = logger;


        public async Task<InviteDTO?> CheckInviteAsync(Guid id)
        {
            //Guid guid = Guid.Empty;
            try
            {
                var sql = "SELECT * FROM public.check_invite(@martid)";
                //var param = new { martid = id };

                var param = new DynamicParameters();
                param.Add("@martid", id);

                //var result = await QuerySingleAsync<InviteDTO>(sql, param);

                return await QuerySingleAsync<InviteDTO>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в CheckInviteAsync");
                return new InviteDTO();
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
        public IAsyncEnumerable<InviteRollDTO> GetAllInvitesAsync()
        {
            var sql = "select * from public.get_all_invites()";

            return Query<InviteRollDTO>(sql);
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
