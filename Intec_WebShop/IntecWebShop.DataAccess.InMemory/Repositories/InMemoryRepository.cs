using IntecWebShop.Core.Interfaces;
using IntecWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace IntecWebShop.DataAccess.InMemory.Repositories
{
    // classe de classe (T)
    //where T derive de la classe base
    // derive de l'interface (click droit sur la classe, extract interface)
    public class InMemoryRepository<T> : IRepository<T> where T:BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;     // determine le nom de la classe utilisée
            items = cache[className] as List<T>;
            if (items==null)
            {
                items = new List<T>();          // complete avec les infos
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T classe)
        {
            items.Add(classe);
        }

        public void Update(T classe)
        {
            var classeToUpdate = items.FindIndex(a => a.Id == classe.Id);
            if (classeToUpdate== -1)
            {
                throw new Exception(className + " Not Found");
            }
            items[classeToUpdate] = classe;
        }

        public T FindById(string Id)
        {
            T classe = items.Find(a => a.Id == Id);

            if (classe!=null)
            {
                return classe;
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public bool Delete(string Id)
        {
            T classeToDelete = items.Find(a => a.Id == Id);
            if (classeToDelete!=null)
            {
                return items.Remove(classeToDelete);
                
            }
            else
            {
                throw new Exception(className + " Not Found");
            }
        }
    }
}
