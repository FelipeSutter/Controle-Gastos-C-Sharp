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

}
