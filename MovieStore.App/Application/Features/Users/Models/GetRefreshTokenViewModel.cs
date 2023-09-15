using Application.Features.Tokens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Models
{
    public class GetRefreshTokenViewModel
    {
        public Token RefreshToken { get; set; }
    }
}
