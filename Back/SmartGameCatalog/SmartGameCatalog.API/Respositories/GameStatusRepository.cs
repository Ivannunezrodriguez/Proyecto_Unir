using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Data;
using Microsoft.EntityFrameworkCore;

using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{

    public class GameStatusRepository
{
    private readonly SmartGameDbContext _context;
    public GameStatusRepository(SmartGameDbContext context) { _context = context; }
    
    /// <summary>
    /// Obtiene todos los registros de estados de juegos.
    /// </summary>
    public async Task<IEnumerable<GameStatus>> GetAll() => await _context.GameStatuses.ToListAsync();
    
    /// <summary>
    /// Obtiene un estado de juego por su ID.
    /// </summary>
    public async Task<GameStatus?> GetById(int id) => await _context.GameStatuses.FindAsync(id);
    
    /// <summary>
    /// Agrega un nuevo estado de juego a la base de datos.
    /// </summary>
    public async Task<GameStatus> Create(GameStatus gameStatus) 
    {
        _context.GameStatuses.Add(gameStatus); 
        await _context.SaveChangesAsync(); 
        return gameStatus; 
    }
    
    /// <summary>
    /// Actualiza la información de un estado de juego existente.
    /// </summary>
    public async Task Update(GameStatus gameStatus) 
    {
        _context.GameStatuses.Update(gameStatus); 
        await _context.SaveChangesAsync(); 
    }
    
    /// <summary>
    /// Elimina un estado de juego de la base de datos.
    /// </summary>
    public async Task Delete(int id) 
    {
        var gameStatus = await _context.GameStatuses.FindAsync(id); 
        if (gameStatus != null) 
        { 
            _context.GameStatuses.Remove(gameStatus); 
            await _context.SaveChangesAsync(); 
        } 
    }
}}