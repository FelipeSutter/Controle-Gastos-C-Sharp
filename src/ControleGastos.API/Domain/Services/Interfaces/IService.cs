namespace ControleGastos.API.Domain.Services.Interfaces;

public interface IService {

	// Toda classe que implementar essa interface terá que dizer qual o request, response e o Id
	/// <summary>
	/// Interface genérica para criação de serviços do tipo CRUD
	/// </summary>
	/// <typeparam name="Request">Contrato de request</typeparam>
	/// <typeparam name="Response">Contrato de response</typeparam>
	/// <typeparam name="Id">Tipo do Id</typeparam>
	public interface IService<Request, Response, Id> where Request : class {
		// Recupera tudo de um usuário específico
		Task<IEnumerable<Response>> GetAll(long idUsuario);

		// Primeiro id é da entidade, segunda é para verificar se o que está sendo retornado é do usuario ou não
		Task<Response> GetById(Id id, long idUsuario);

		// Request da entidade, e id do usuário logado
		Task<Response> Add(Request request, long idUsuario);
		
		// Mesma coisa que o adicionar, mas tem que passar o id da entidade também
		Task<Response> Update(Id id, Request request, long idUsuario);
		
		// Deleta a entidade pelo id, e sempre do id que está logado
		Task Delete(Id id, long idUsuario);

    }

}
