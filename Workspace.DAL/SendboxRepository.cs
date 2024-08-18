using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using Workspace.Entities;

namespace Workspace.DAL
{
    public class SendboxRepository(ILogger<MartRepository> logger, IConfiguration configuration)
                    : BaseRepository(logger, configuration), ISendboxRepository
    {
        private readonly ILogger _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task CreateSendboxAsync(SendboxDTO sendboxDTO)
        {
            using var connection = GetConnection();

            var sql = "public.create_sendbox";

            var param = new DynamicParameters();
            param.Add("@inviteid", sendboxDTO.InviteId);
            param.Add("@martid", sendboxDTO.MartId);
            param.Add("@userid", sendboxDTO.UserId);

            await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<SendboxFullDTO>> GetUsersAsync(Guid martId)
        {
            var sql = "SELECT * FROM public.get_sendbox_users(@wmart_id)";

            try
            {
                var param = new { wmart_id = martId };
                return await QueryAsync<SendboxFullDTO>(sql, param);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выбое пользователей из песочницы по Id");
                throw;
            }
        }
    }
}