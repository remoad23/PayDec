using PayDec.Client.Services.Repository.Interfaces;
using PayDec.Shared.Model.Interfaces;
using System.Net.Http.Json;
using System.Numerics;

namespace PayDec.Client.Services.Repository
{
    public class Repository : IRepository
    {
        private HttpClient Client { get; set; }
        public Repository(HttpClient client)
        {
            this.Client = client;
        }

        public async Task<IEnumerable<IPDObject>> IndexAsync(Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(type);
            object list = Activator.CreateInstance(listType);

            var result = await Client.GetFromJsonAsync($"/{type.Name}s", list.GetType());
            
            return result as IEnumerable<IPDObject>;
        }

        public async Task<IPDObject> GetAsync(int id,Type type)
        {
            var result = await Client.GetFromJsonAsync($"/{type.Name}",type);


            return result as IPDObject;
        }

        public async Task<bool> PostAsync(IPDObject obj)
        {
            var result = await Client.PostAsJsonAsync($"/{obj.GetType().Name}/Create",obj);
            return true;
        }

        public async Task PostListAsync(string serializedObj,Type type)
        {
            var result = await Client.PostAsJsonAsync($"/{type.Name}s/Create", serializedObj);
        }

        public async Task Pay(string serializedObj)
        {
            var result = await Client.PostAsJsonAsync($"/Pay", serializedObj);
        }

        public async Task<long> GetBalance(string contractAdress)
        {
            var result = await Client.GetFromJsonAsync<long>($"/GetBalance?contractAdress={contractAdress}");
            return result;
        }

        public async Task<bool> PutAsync(IPDObject obj)
        {
            var result = await Client.PutAsJsonAsync($"/{obj.GetType().Name}/Change",obj);
            return true;
        }


        public async Task<bool> DeleteAsync(IPDObject obj)
        {
            var result = Client.DeleteAsync($"/{obj.GetType().Name}/Delete");
            return true;
        }


    }
}
