using ControleGastos.API.Contracts.ModelError;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleGastos.API.Controllers;
[Route("[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase {

    protected long GetLoggedUserId() {
        var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        long.TryParse(id, out long userId); // Isso serve para criar uma variavel dentro do método e poder utilizar fora dele,
                                           // como se tivesse criado do lado de fora
        return userId;
    }

    protected ModelErrorContract BadRequest(Exception ex) {
        return new ModelErrorContract {
            StatusCode = 400,
            Title = "Bad Request",
            Message = ex.Message,
            DateTime = DateTime.Now,
        };
    }

    protected ModelErrorContract NotFound(Exception ex) {
        return new ModelErrorContract {
            StatusCode = 404,
            Title = "Not Found",
            Message = ex.Message,
            DateTime = DateTime.Now,
        };
    }

    protected ModelErrorContract Unauthorized(Exception ex) {
        return new ModelErrorContract {
            StatusCode = 401,
            Title = "Unauthorized",
            Message = ex.Message,
            DateTime = DateTime.Now,
        };
    }

}
