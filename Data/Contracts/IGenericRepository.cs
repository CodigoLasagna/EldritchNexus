namespace Data.Contracts;

public interface IGenericRespository<T> where T : class
{
    int Create(T entity);
    int Read(int Id);
    int Update(T entity, int Id);
    int Delete(int Id);
}
