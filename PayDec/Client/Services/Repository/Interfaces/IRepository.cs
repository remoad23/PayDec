using PayDec.Shared.Model.Interfaces;
using System.Numerics;

namespace PayDec.Client.Services.Repository.Interfaces
{
    public interface IRepository
    {
        public virtual  Task<List<IPDObject>> IndexAsync(Type type) => throw new NotImplementedException();

        public virtual Task<IPDObject> GetAsync(int id, Type type) => throw new NotImplementedException();

        public virtual Task PostAsync(IPDObject obj) => throw new NotImplementedException();

        public virtual async Task PostListAsync(List<IPDObject> obj) => throw new NotImplementedException();

        public virtual Task PutAsync(IPDObject obj) => throw new NotImplementedException();

        public virtual Task DeleteAsync(IPDObject obj) => throw new NotImplementedException();

        public virtual async Task Pay(string serializedObj) => throw new NotImplementedException();

        public virtual async Task<string> GetBalance(string contractAdress) => throw new NotImplementedException();
    }
}
