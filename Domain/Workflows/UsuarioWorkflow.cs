using System;
using System.Collections.Generic;
using System.Security.Claims;
using UStart.Domain.Commands;
using UStart.Domain.Contracts.Repositories;
using UStart.Domain.Entities;
using UStart.Domain.Helpers;
using UStart.Domain.Helpers.TokenHelper;
using UStart.Domain.Results;
using UStart.Domain.UoW;

namespace UStart.Domain.Workflows
{
    public class UsuarioWorkflow : WorkflowBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenHelper _tokenHelper;

        public UsuarioWorkflow(IUsuarioRepository usuarioRepository, TokenHelper tokenHelper, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _tokenHelper = tokenHelper;
            _unitOfWork = unitOfWork;
        }

        public LoginResult Login(LoginCommand command)
        {
            string hash = Md5Helper.GenerateHash($"{command.Email}:{command.Senha}");

            Usuario usuario = _usuarioRepository.Login(hash);
            if (usuario == null)
            {
                AddError("Usuário", "Usuário ou senha inválidos", command.Email);
            }

            if (usuario != null)
            {
                LoginResult loginResult = new LoginResult();
                string name = usuario.Nome != null ? usuario.Nome : "";

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, name));
                claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
                claims.Add(new Claim("usuarioid", usuario.Id.ToString()));

                string token = _tokenHelper.GenerateToken(claims.ToArray());
                loginResult.Token = token;

                return loginResult;

            }

            return null;
        }

        public void Add(UsuarioCommand command)
        {
            try
            {
                var emailEmUso = _usuarioRepository.GetByEmail(command.Email);
                if (emailEmUso != null)
                {
                    AddError("Email", "E-mail já cadastrado em outro usuário.");
                }

                ValidacoesUsuarios(command);

                if (this.IsValid() == false)
                    return;

                var usuario = new Usuario(command);
                _usuarioRepository.Add(usuario);
                _unitOfWork.Commit();
            }
            catch (System.Exception exp)
            {
                AddException("Usuário", exp);
            }

        }

        public void Update(Guid id, UsuarioCommand command)
        {
            try
            {
                var usuario = _usuarioRepository.GetById(id);
                if (usuario == null)
                {
                    AddError("Usuário", "Usuário não encontrado", id);
                }

                ValidacoesUsuarios(command);

                if (this.IsValid() == false)
                    return;

                usuario.Update(command);
                _usuarioRepository.Update(usuario);
                _unitOfWork.Commit();

            }
            catch (System.Exception exp)
            {
                AddException("Usuário", exp);
            }
        }

        private void ValidacoesUsuarios(UsuarioCommand command)
        {
            if (command.Senha != null && command.Senha.Length < 4)
            {
                AddError("Senha", "Senha informada deve conter 4 caracteres.");
            }
        }

    }
}
