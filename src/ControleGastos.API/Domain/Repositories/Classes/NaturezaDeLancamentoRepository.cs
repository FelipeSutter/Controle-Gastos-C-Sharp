using ControleGastos.API.Data;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Domain.Repositories.Classes;

public class NaturezaDeLancamentoRepository : INaturezaDeLancamentoRepository
{

    private readonly ApplicationContext _context;

    // Cria a injeção de dependendia do applicationContext
    public NaturezaDeLancamentoRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<NaturezaDeLancamento> Add(NaturezaDeLancamento entity)
    {
        // Adiciona em memória, mas não salva as mudanças no banco
        await _context.Naturezas.AddAsync(entity);
        // Salva as mudanças no banco
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<NaturezaDeLancamento?> Update(NaturezaDeLancamento entity)
    {
        NaturezaDeLancamento entityBanco = await _context.Naturezas
            .Where(u => u.Id == entity.Id)
            .FirstOrDefaultAsync();

        // Recupera o valor que foi achado da entidade e troca os valores dela pelos valores passados em entity, atualizando-os
        _context.Entry(entityBanco).CurrentValues.SetValues(entity);
        _context.Update<NaturezaDeLancamento>(entityBanco);

        await _context.SaveChangesAsync();

        return entityBanco;

    }

    public async Task Delete(NaturezaDeLancamento entity)
    {
        // Delete lógico, só altera a data de inativação
        entity.DataInativacao = DateTime.Now;
        await Update(entity);
    }

    public async Task<IEnumerable<NaturezaDeLancamento?>> GetAll()
    {
        // Recupera todos os usuários e ordena eles baseado no id
        return await _context.Naturezas.AsNoTracking()
            .OrderBy(u => u.Id)
            .ToListAsync();
    }

    public async Task<NaturezaDeLancamento?> GetById(long id)
    {
        return await _context.Naturezas.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    // Pega o id do usuário que está na entidade e compara com o id que está vindo no parâmetro
    public async Task<IEnumerable<NaturezaDeLancamento>> GetByIdUsuario(long id) {
        return await _context.Naturezas.AsNoTracking()
            .Where(u => u.IdUsuario == id)
            .OrderBy(u => u.Id)
            .ToListAsync();
    }
}
