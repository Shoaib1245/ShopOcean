using Service.Admin.APIs.AdminDBContext;
using Service.Admin.APIs.Features.ChatSystem.Core;
using Service.Shared.DbFactoryClass;
using Service.Shared.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata;

namespace Service.Admin.APIs.Features.ChatSystem.Service
{
    public class ChatSystemService:GenericRepository<ChatSystemModel,AdminDBContextt>
    {
        private readonly AdminDBContextt _Admindbcontext;
        public ChatSystemService(AdminDBContextt Admindbcontext,DbFactory<AdminDBContextt> dbFactory) : base(dbFactory)
        {
            _Admindbcontext = Admindbcontext;
        }
        public async Task<int> MessageSave(ChatSystemModel model)
        {
            model.IsDeleted = false;
            await _Admindbcontext.Tbl_ChatSystem.AddAsync(model);
            return await _Admindbcontext.SaveChangesAsync();
        }

        public async Task<List<ChatSystemModel>> getmessages(ChatSystemModel obj)
        {
            var parameter = new
            {
                UserName = obj.UserName,
                UserType = obj.UserType
            };
            var data = await GetDatabyId<ChatSystemModel>("select * from Tbl_ChatSystem where UserName=@UserName AND UserType=@UserType", parameter);
            return data.ToList();
        }

    }
}
