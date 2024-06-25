using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Provider
{
    public interface IJwtTokenProvider
    {
        string GenerateAccessToken(JwtTokenBodyInfo bodyInfo);
        string GenerateRefreshToken();
    }
}
