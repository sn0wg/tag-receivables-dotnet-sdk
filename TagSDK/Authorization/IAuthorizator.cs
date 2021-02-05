using System;
using System.Threading.Tasks;
using TagSDK.Models.Enums;

namespace TagSDK.Authorization
{
    public interface IAuthorizator
    {
        Task<String> GetToken(Profile profile, bool refresh = false);
    }
}
