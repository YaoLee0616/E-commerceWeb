using System.Collections.Generic;
using System.Linq;

namespace E_commerceWeb.Models
{
    public interface ITypeRepository
    {
        void CreateTestData();
        IQueryable<Type> SelectAll();
        Type SelectTypeById(int id);
        void UpdateTypeById(Type type);
        void DeleteTypeById(int id);
        void AddType(Type type);
    }
}
