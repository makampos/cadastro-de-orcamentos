using orcamento_crud.Models;
using System.Collections.Generic;


namespace orcamento_crud.Interface
{
    public interface IClient
    {
        public IEnumerable<Client> GetlAllClients();
        public void AddClient(Client client);
        public void DeleteClient(int? id);
        public void UpdateClient(Client client);
        public Client GetClient(int? id);
    }
}
