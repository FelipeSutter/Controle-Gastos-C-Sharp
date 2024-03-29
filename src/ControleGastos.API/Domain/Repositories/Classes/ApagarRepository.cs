﻿using ControleGastos.API.Data;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Domain.Repositories.Classes;

public class ApagarRepository : IApagarRepository
{

    private readonly ApplicationContext _context;

    // Cria a injeção de dependendia do applicationContext
    public ApagarRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Apagar> Add(Apagar entity)
    {
        // Adiciona em memória, mas não salva as mudanças no banco
        await _context.Apagar.AddAsync(entity);
        // Salva as mudanças no banco
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Apagar?> Update(Apagar entity)
    {
        Apagar entityBanco = await _context.Apagar
            .Where(u => u.Id == entity.Id)
            .FirstOrDefaultAsync();

        // Recupera o valor que foi achado da entidade e troca os valores dela pelos valores passados em entity, atualizando-os
        _context.Entry(entityBanco).CurrentValues.SetValues(entity);
        _context.Update<Apagar>(entityBanco);

        await _context.SaveChangesAsync();

        return entityBanco;

    }

    public async Task Delete(Apagar entity)
    {
        // Delete lógico, só altera a data de inativação
        entity.DataInativacao = DateTime.Now;
        await Update(entity);
    }

    public async Task<IEnumerable<Apagar?>> GetAll()
    {
        // Recupera todos os usuários e ordena eles baseado no id
        return await _context.Apagar.AsNoTracking()
            .OrderBy(u => u.Id)
            .ToListAsync();
    }

    public async Task<Apagar?> GetById(long id)
    {
        return await _context.Apagar.AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    // Pega o id do usuário que está na entidade e compara com o id que está vindo no parâmetro
    public async Task<IEnumerable<Apagar>> GetByIdUsuario(long id) {
        return await _context.Apagar.AsNoTracking()
            .Where(u => u.IdUsuario == id)
            .OrderBy(u => u.Id)
            .ToListAsync();
    }
}
