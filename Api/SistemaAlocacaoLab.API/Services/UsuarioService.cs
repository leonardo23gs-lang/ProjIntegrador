using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAlocacaoLab.API.DTOs.Usuario;
using SistemaAlocacaoLab.API.Models;
using SistemaAlocacaoLab.API.Repositories;

namespace SistemaAlocacaoLab.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(u => MapToResponseDto(u));
        }

        public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;
            return MapToResponseDto(usuario);
        }

        public async Task<UsuarioResponseDto> CreateAsync(UsuarioRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NomeUsuario))
                throw new ArgumentException("O nome do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.EmailUsuario))
                throw new ArgumentException("O e-mail é obrigatório.");

            if (await _repository.EmailJaExisteAsync(dto.EmailUsuario))
                throw new ArgumentException("Este e-mail já está cadastrado.");

            if (!await _repository.PerfilExisteAsync(dto.IdPerfil))
                throw new ArgumentException("Perfil informado não encontrado.");

            var usuario = new Usuario
            {
                NomeUsuario = dto.NomeUsuario,
                EmailUsuario = dto.EmailUsuario,
                SenhaUsuario = dto.SenhaUsuario,
                IdPerfil = dto.IdPerfil
            };

            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();

            var criado = await _repository.GetByIdAsync(usuario.IdUsuario);
            return MapToResponseDto(criado!);
        }

        public async Task<UsuarioResponseDto?> UpdateAsync(int id, UsuarioRequestDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            if (string.IsNullOrWhiteSpace(dto.NomeUsuario))
                throw new ArgumentException("O nome do usuário é obrigatório.");

            // Verifica e-mail duplicado ignorando o próprio usuário
            var emailEmUso = await _repository.EmailJaExisteAsync(dto.EmailUsuario);
            if (emailEmUso && usuario.EmailUsuario != dto.EmailUsuario)
                throw new ArgumentException("Este e-mail já está cadastrado.");

            if (!await _repository.PerfilExisteAsync(dto.IdPerfil))
                throw new ArgumentException("Perfil informado não encontrado.");

            usuario.NomeUsuario = dto.NomeUsuario;
            usuario.EmailUsuario = dto.EmailUsuario;
            usuario.SenhaUsuario = dto.SenhaUsuario;
            usuario.IdPerfil = dto.IdPerfil;

            await _repository.UpdateAsync(usuario);
            await _repository.SaveChangesAsync();

            var atualizado = await _repository.GetByIdAsync(id);
            return MapToResponseDto(atualizado!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return false;

            await _repository.DeleteAsync(usuario);
            await _repository.SaveChangesAsync();

            return true;
        }

        private UsuarioResponseDto MapToResponseDto(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                NomeUsuario = usuario.NomeUsuario,
                EmailUsuario = usuario.EmailUsuario,
                IdPerfil = usuario.IdPerfil,
                TipoPerfil = usuario.Perfil?.TipoPerfil ?? ""
            };
        }
    }
}