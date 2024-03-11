using ControleGastos.API.Data;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Domain.Repositories.Classes;

public class UsuarioRepository : IUsuarioRepository
{

    private readonly ApplicationContext _context;

    // Cria a injeção de dependendia do applicationContext
    public UsuarioRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Usuario> Add(Usuario entity)
    {
        // Adiciona em memória, mas não salva as mudanças no banco
        await _context.Usuarios.AddAsync(entity);
        // Salva as mudanças no banco
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Usuario?> Update(Usuario entity)
    {
        Usuario entityBanco = await _context.Usuarios
            .Where(u => u.Id == entity.Id)
            .FirstOrDefaultAsync();

        // Recupera o valor que foi achado da entidade e troca os valores dela pelos valores passados em entity, atualizando-os
        _context.Entry(entityBanco).CurrentValues.SetValues(entity);
        _context.Update<Usuario>(entityBanco);

        await _context.SaveChangesAsync();

        return entityBanco;

    }

    public async Task Delete(Usuario entity)
    {
        // Dessa forma deleta fisicamente a entidade
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Usuario?>> GetAll()
    {
        // Recupera todos os usuários e ordena eles baseado no id
        return await _context.Usuarios.AsNoTracking()
            .OrderBy(u => u.Id)
            .ToListAsync();
    }

    public async Task<Usuario?> GetByEmail(string email)
    {
        // Retorna o usuario cujo email é igual ao email passado
        // o AsNoTracking serve para trazer somente o usuário, sem nada vinculado a ele
        return await _context.Usuarios.AsNoTracking()
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetById(long id)
    {
        return await _context.Usuarios.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

}
