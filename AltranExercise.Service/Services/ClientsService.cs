using AltranExercise.Common.Infraestructure;
using AltranExercise.Data.Repositories;
using AltranExercise.Service.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AltranExercise.Service.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _repository;
        private readonly IMapper _mapper;
        private readonly string _secretKey;

        public ClientsService(IOptions<AppSettingsOption> options, IMapper mapper, IClientsRepository repository)
        {
            this._secretKey = options.Value.Secret;
            this._repository = repository;
            this._mapper = mapper;
        }

        public AuthenticationTokenDto Authenticate(string clientEmail)
        {
            var user = this._repository.GetClientByEmail(clientEmail);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            var authenticationTokenResult = new AuthenticationTokenDto();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            authenticationTokenResult.Email = clientEmail;
            authenticationTokenResult.Role = user.Role;
            authenticationTokenResult.Token = tokenHandler.WriteToken(token);

            return authenticationTokenResult;
        }

        public ClientDto GetClientById(string clientId)
        {
            var result = this._repository.GetClientById(clientId);

            return this._mapper.Map<ClientDto>(result);
        }

        public ClientDto GetClientByName(string clientName)
        {
            var result = this._repository.GetClientByName(clientName);

            return this._mapper.Map<ClientDto>(result);
        }
    }
}
