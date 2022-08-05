using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()           // generic constraint
    {  // class -> referans tip olmali  IEntity -> IEntity veya IEntity'i implemente eden bir nesne olmali (new'lenebilir olmaali)
        List<T> GetAll(Expression<Func<T, bool>> filter = null);    // tum urunler uzerinde filtre uygulamamizi saglayan yapi (delege arastir)
        T Get(Expression<Func<T, bool>> filter = null);             // filtre verirsek filtre uygulayacak vermezsek direkt getirecek
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
