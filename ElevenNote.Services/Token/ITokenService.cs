using ElevenNote.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services.Token
{
    public interface ITokenService 
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest model);
    }
}
