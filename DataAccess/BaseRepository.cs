using DataAccess.DapperModels;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseRepository<T> :IDisposable  where T : BaseModel 
    {
        protected readonly string _tableName;
        protected IDbConnection _connection { get; set; }

        public BaseRepository(string tableName, ConnectionFactory connectionFactory)
        {
            _tableName = tableName;
            _connection = connectionFactory.CreateConnection("BlackJackConnectionString");
        }
        public virtual async Task<IEnumerable<T>> All()
        {
           var result = await _connection.GetAllAsync<T>();
            return result;
        }

        public virtual async Task<string> Add(T item)
        {
            item.Id = Guid.NewGuid();
            var result =  await _connection.InsertAsync(item);
            return item.Id.ToString();
        }

        public virtual async Task<bool> Update(List<T> items)
        {
            return await _connection.UpdateAsync(items);
        }


        public virtual async Task<bool> Update(T item)
        {
           return await _connection.UpdateAsync(item);
        }

        public async Task DeleteList(List<Guid> idToDelete)
        {
            if (idToDelete == null || idToDelete.Count < 1)
            {
                return;
            }

            var IdsForDelete = new List<T>();

            foreach (var id in idToDelete)
            {
                var itemForDelete = (T)Activator.CreateInstance(typeof(T), new object[] { });
                itemForDelete.Id = id;
                IdsForDelete.Add(itemForDelete);
            }

            await _connection.DeleteAsync(IdsForDelete);
        }

        public virtual async Task<int> Remove(T item)
        {
            return  await _connection.ExecuteAsync("DELETE FROM " + _tableName + " WHERE Id=@Id", new { item.Id });
        }

        public virtual async Task<int> RemoveById(Guid id)
        {
            return await _connection.ExecuteAsync("DELETE FROM " + _tableName + " WHERE Id=@Id", new { Id = id });
        }

        public virtual async Task<T> FindById(Guid id)
        {
            var result = await _connection.QueryFirstAsync<T>("SELECT TOP(1) * FROM " + _tableName + " WHERE Id=@Id", new { Id = id });
            return result;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
