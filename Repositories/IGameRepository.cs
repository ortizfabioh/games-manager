using Games_ASPNET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int qtt);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string developer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
