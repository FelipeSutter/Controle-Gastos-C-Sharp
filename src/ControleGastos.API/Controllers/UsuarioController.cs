﻿using ControleGastos.API.Contracts.Usuario;
using ControleGastos.API.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous] // Essa anotação diz que qualquer pessoa sem autentição consegue utilizar esse endpoint
        public async Task<IActionResult> Add(UsuarioRequestContract contract)
        {
            try
            {
                return Created("", await _usuarioService.Add(contract)); // a função espera retornar uma string e um objeto
            } catch (Exception ex) {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _usuarioService.GetAll()); 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _usuarioService.GetById(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("id")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(long id, UsuarioRequestContract contract)
        {
            try
            {
                return Ok(await _usuarioService.Update(id, contract));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("id")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _usuarioService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}