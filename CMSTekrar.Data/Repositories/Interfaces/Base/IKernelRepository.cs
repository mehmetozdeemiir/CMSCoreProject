using CMSTekrar.Entity.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMSTekrar.Data.Repositories.Interfaces.Base
{
    public interface IKernelRepository<T> where T: class, IBaseEntity
    {

        //DIP'e uymadan yaptığımız projelerde tek bir interface oluşturmuştuk. Bu interfacedeki crud işlemlerini her varlık için oluşturduğumuz EfRepositorylere tek tek kalıtım vermiştir. UI katmanında da EfRepositoryleri çağırdımıştık.
        //Lakin DIP'e uyduğumuz bu projede Her varlık için ayrı ayrı Interface tanımlamamız gerekecek çünkü  DIP'e göre UI katmanında Interfaceler tanımlanacak.


        Task<List<T>> GetAll(); //Asenkron programlama yapmak istediğimiz metodlarımızı "Task" olarak işaretlenir.
        Task<List<T>> Get(Expression<Func<T, bool>> expression); //Expression<Func<T, bool>> expression nedir? çağırdığımızda bunu ilgili metodun içine istediğimiz linq sorgusunu yazmamızı sağlar(linqto sorgusunu dinamik yapar)
        Task<T> GetById(int id);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T> FindByDefault(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);



    }
}
