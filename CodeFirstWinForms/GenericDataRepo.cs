using CodeFirstWinForms.Datamodel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CodeFirstWinForms
{
    public class GenericDataRepo<T> : IGenericData<T> where T : class
    {
        private DataContext db = new DataContext();
        public IList<T> GetAll<T>() where T : class
        {
            return db.Set<T>().ToList();
        }
        public void Add(T items)
        {
            this.db.Set<T>().Add(items);
            this.db.SaveChanges();
        }
        public void Update(T items)
        {
            db.Entry(items).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(T items) 
        {
            var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                db.Set<T>().Remove(items);
                db.SaveChanges();
            }
        }
    }
}
    