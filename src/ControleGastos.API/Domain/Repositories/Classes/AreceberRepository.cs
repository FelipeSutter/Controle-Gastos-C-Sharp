using ControleGastos.API.Data;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Domain.Repositories.Classes;

public class AreceberRepository : IAreceberRepository
{

    private readonly ApplicationContext _context;

    // Cria a injeção de dependendia do applicationContext
    public AreceberRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Areceber> Add(Areceber entity)
    {
        // Adiciona em memória, mas não salva as mudanças no banco
        await _context.Areceber.AddAsync(entity);
        // Salva as mudanças no banco
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Areceber?> Update(Areceber entity)
    {
        Areceber entityBanco = await _context.Areceber
            .Where(u => u.Id == entity.Id)
            .FirstOrDefaultAsync();

        // Recupera o valor que foi achado da entidade e troca os valores dela pelos valores passados em entity, atualizando-os
        _context.Entry(entityBanco).CurrentValues.SetValues(entity);
        _context.Update<Areceber>(entityBanco);

        await _context.SaveChangesAsync();

        return entityBanco;

    }

    public async Task Delete(Areceber entity)
    {
        // Delete lógico, só altera a data de inativação
        entity.DataInativacao = DateTime.Now;
        await Update(entity);
    }

    public async Task<IEnumerable<Areceber?>> GetAll()
    {
        // Recupera todos os usuários e ordena eles baseado no id
        return await _context.Areceber.AsNoTracking()
            .OrderBy(u => u.Id)
            .ToListAsync();
    }

    public async Task<Areceber?> GetById(long id)
    {
        return await _context.Areceber.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    // Pega o id do usuário que está na entidade e compara com o id que está vindo no parâmetro
    public async Task<IEnumerable<Areceber>> GetByIdUsuario(long id) {
        return await _context.Areceber.AsNoTracking()
            .Where(u => u.IdUsuario == id)
            .OrderBy(u => u.Id)
            .ToListAsync();
    }
}
