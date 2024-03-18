using ControleGastos.API.Contracts.NaturezaDeLancamento;
using ControleGastos.API.Domain.Services.Interfaces;
using ControleGastos.API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers {
    public class NaturezaDeLancamentoController : BaseController
    {

        private readonly INaturezaDeLancamentoService _naturezaDeLancamentoService;

        public NaturezaDeLancamentoController(INaturezaDeLancamentoService naturezaDeLancamentoService) {
            _naturezaDeLancamentoService = naturezaDeLancamentoService;
        }

        [HttpPost]
        [Authorize] // Essa anotação diz que precisa de autenticação para utilizar esse endpoint 
        public async Task<IActionResult> Add(NaturezaDeLancamentoRequestContract contract)
        {
            try
            {
                return Created("", await _naturezaDeLancamentoService.Add(contract, GetLoggedUserId())); // a função espera retornar uma string e um objeto
            } catch (BadRequestException ex) {
                return BadRequest(BadRequest(ex));
            } catch (Exception ex) {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _naturezaDeLancamentoService.GetAll(GetLoggedUserId())); 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                return Ok(await _naturezaDeLancamentoService.GetById(id, GetLoggedUserId()));
            } catch (NotFoundException ex) {
                return NotFound(NotFound(ex));
            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("id")]
        [Authorize]
        public async Task<IActionResult> Update(long id, NaturezaDeLancamentoRequestContract contract)
        {
            try
            {
                return Ok(await _naturezaDeLancamentoService.Update(id, contract, GetLoggedUserId()));
            } catch (NotFoundException ex) {
                return NotFound(NotFound(ex));
            } catch (BadRequestException ex) {
                return BadRequest(BadRequest(ex));
            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("id")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _naturezaDeLancamentoService.Delete(id, GetLoggedUserId());
                return NoContent();
            } catch (NotFoundException ex) {
                return NotFound(NotFound(ex));
            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
