using Games_ASPNET.Entities;
using Games_ASPNET.Exceptions;
using Games_ASPNET.InputModel;
using Games_ASPNET.Repositories;
using Games_ASPNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int qtt)
        {
            var result = await _gameRepository.Get(page, qtt);
            return result.Select(result => new GameViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Developer = result.Developer,
                Price = result.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var result = await _gameRepository.Get(id);

            if (result == null)
                return null;

            return new GameViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Developer = result.Developer,
                Price = result.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var result = await _gameRepository.Get(game.Name, game.Developer);
            if (result.Count() > 0)
                throw new GameAlreadyRegisteredException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var result = await _gameRepository.Get(id);

            if (result == null)
                throw new GameNotRegisteredException();

            result.Name = game.Name;
            result.Developer = game.Developer;
            result.Price = game.Price;

            await _gameRepository.Update(result);
        }

        public async Task Update(Guid id, double price)
        {
            var result = await _gameRepository.Get(id);

            if (result == null)
                throw new GameNotRegisteredException();

            result.Price = price;

            await _gameRepository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            var result = await _gameRepository.Get(id);

            if (result == null)
                throw new GameNotRegisteredException();

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
