using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZSZ.Service
{
   public class BaseService<T> where T :BaseEntity
    {
        private MyContext my;
        public BaseService(MyContext my)
        {
            this.my = my;
        }
        /// <summary>
        /// 获取所有没有软删除的数据(?)
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return my.Set<T>().Where(s => s.IsDeleted == false);
        }
        ///<summary>
        ///获取总数据条数
        /// </summary>
        /// <remarks></remarks>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }
        ///<summary>
        ///分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <remarks></remarks>
        public IQueryable<T> GetPagedData(int startIndex,int count)
        {
            return GetAll().OrderBy(m => m.CreateDateTime).Skip(startIndex).Take(count);
        }
        /// <summary>
        /// 查找id=id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(m => m.Id == id).FirstOrDefault();
        }
        ///<summary>
        ///是否删除（软删除）
        /// </summary>
        /// <remarks></remarks>
        public bool MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            int b = my.SaveChanges();
            if(b>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
