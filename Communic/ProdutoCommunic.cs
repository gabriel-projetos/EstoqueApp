using Interfaces;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Communic
{
    public class ProdutoCommunic : IProduto
    {
        const string URL = "http://192.168.200.106:3000/api/";
        static readonly HttpClient _client = new HttpClient();
        public ProdutoCommunic()
        {

        }

        public async Task CreateProduct(Produto produto)
        {
            try
            {
                var response = await _client.PostAsync($"{URL}produtos", new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"Erro: {response.StatusCode}, {response.RequestMessage}");
                    }
                }
                else
                {
                    throw new Exception($"Erro: {response.StatusCode}, {response.RequestMessage}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteProducts(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"{URL}produtos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"Erro: {response.StatusCode}, {response.RequestMessage}");
                    }
                }
                else
                {
                    throw new Exception($"Erro: {response.StatusCode}, {response.RequestMessage}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Produto>> GetProducts()
        {
            try
            {
                var response = await _client.GetAsync($"{URL}produtos");
                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<List<Produto>>(await response.Content.ReadAsStringAsync());
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateProducts(Produto produto)
        {
            try
            {
                var response = await _client.PutAsync($"{URL}produtos", new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"Erro: {response.StatusCode}, {response.RequestMessage}");
                    }
                }
                else
                {
                    throw new Exception($"Erro: {response.StatusCode}, {response.RequestMessage}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
