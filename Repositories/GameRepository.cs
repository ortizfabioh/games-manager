using Games_ASPNET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {

        };

        public Task<List<Game>> Get(int page, int qtt)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * qtt).Take(qtt).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            if (!games.ContainsKey(id))
                return null;

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string developer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Developer.Equals(developer)).ToList());
        }

        public Task<List<Game>> GetWithoutLambda(string name, string developer)
        {
            var result = new List<Game>();
            
            foreach(var game in games.Values)
            {
                if (game.Name.Equals(name) && game.Developer.Equals(developer))
                    result.Add(game);
            }
            return Task.FromResult(result);
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
