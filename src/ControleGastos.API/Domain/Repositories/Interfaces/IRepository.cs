namespace ControleGastos.API.Domain.Repositories.Interfaces;

// TypeOfClass é o tipo da classe que implementará a interface e Id é o tipo do Id da interface
// Essa interface serve como um contrato para todos os repositories, todos eles terão no mínimo os métodos registrados nessa interface
public interface IRepository<TypeOfClass, Id> where TypeOfClass : class
{

    Task<IEnumerable<TypeOfClass?>> GetAll();
    Task<TypeOfClass?> GetById(Id id);
    Task<TypeOfClass> Add(TypeOfClass entity);
    Task<TypeOfClass?> Update(TypeOfClass entity);
    Task Delete(TypeOfClass entity);
}
