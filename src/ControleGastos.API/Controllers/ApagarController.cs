using ControleGastos.API.Contracts.Apagar;
using ControleGastos.API.Contracts.ModelError;
using ControleGastos.API.Domain.Services.Interfaces;
using ControleGastos.API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers {
    public class ApagarController : BaseController
    {

        private readonly IApagarService _apagarService;

        public ApagarController(IApagarService apagarService) {
            _apagarService = apagarService;
        }

        [HttpPost]
        [Authorize] // Essa anotação diz que precisa de autenticação para utilizar esse endpoint 
        public async Task<IActionResult> Add(ApagarRequestContract contract)
        {
            try
            {
                return Created("", await _apagarService.Add(contract, GetLoggedUserId())); // a função espera retornar uma string e um objeto
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
                return Ok(await _apagarService.GetAll(GetLoggedUserId())); 
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
                return Ok(await _apagarService.GetById(id, GetLoggedUserId()));
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
        public async Task<IActionResult> Update(long id, ApagarRequestContract contract)
        {
            try
            {
                return Ok(await _apagarService.Update(id, contract, GetLoggedUserId()));
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
                await _apagarService.Delete(id, GetLoggedUserId());
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
