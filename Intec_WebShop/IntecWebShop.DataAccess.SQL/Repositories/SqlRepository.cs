using IntecWebShop.Core.Interfaces;
using IntecWebShop.DataAccess.SQL.Contexts;
using IntecWebShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntecWebShop.DataAccess.SQL.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {

        internal DataContext context;
        internal DbSet<T> dbset;                // dbset pour product and category

        public SqlRepository(DataContext cont)
        {
            this.context = cont;
            this.dbset = cont.Set<T>();
        }


        public IQueryable<T> Collection()
        {
            return dbset;               // renvoi le dbset de la classe concernee
        }

        public void Commit()
        {
            context.SaveChanges();          // save changes ares chaque manipulation
        }

        public bool Delete(string Id)
        {
            var t = FindById(Id);       // search the item via id
            if (context.Entry(t).State== EntityState.Detached)      // check state de la connexion
            {
                dbset.Attach(t);                    // attache item 
            }
            context.Entry(t).State = EntityState.Deleted;        // state of the item is deleted
            dbset.Remove(t);                        // delete the item
            Commit();
            return true;
        }

        public T FindById(string Id)
        {
            return dbset.Find(Id);          // search an item by id
        }

        public void Insert(T classe)
        {
            dbset.Add(classe);          // add new item
        }

        public void Update(T classe)
        {
            dbset.Attach(classe);
            context.Entry(classe).State = EntityState.Modified;         // state of item is modified (after update) 
            Commit();                                                   //save changes
        }
    }
}
