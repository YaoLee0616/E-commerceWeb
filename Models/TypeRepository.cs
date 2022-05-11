using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceWeb.Models
{
    public class TypeRepository : ITypeRepository
    {
        private AppDbContext context;

        // private string[] strs =  new string[] {"手机","折叠屏手机","曲面屏手机","平板电脑","笔记本电脑"};

        public TypeRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public void CreateTestData()
        {
            context.Types.RemoveRange(context.Types);
            // bool[] visited = new bool[strs.Length];
            Random rd = new Random();
            for(int i = 1; i <= 10; i++)
            {
                /*int num = rd.Next(0, strs.Length);
                while (visited[num])
                {
                    num = rd.Next(0, strs.Length);
                }
                visited[num] = true;*/
                Type type = new Type();
                //type.TypeName = strs[num];
                //type.TypeDescription = strs[num];
                type.TypeName = "类别"+i;
                type.TypeDescription = "描述"+i;
                context.Types.Add(type);    
            }
           context.SaveChanges();
        }

        public IQueryable<Type> SelectAll()
        {
            return context.Types.Include(e => e.Goods);
        }

        public void UpdateTypeById(Type type)
        {
            context.Types.Update(type);
            context.SaveChanges();
        }

        public void DeleteTypeById(int id)
        {
            Type type = context.Types.Find(id);
            context.Types.Remove(type);
            if(type.Goods != null)
            {
                context.Goods.RemoveRange(type.Goods);
            }
            context.SaveChanges();
        }

        public void AddType(Type type)
        {
            context.Types.Add(type);
            context.SaveChanges();
        }

        public Type SelectTypeById(int id)
        {
            return context.Types.Find(id);
        }
    }
}
