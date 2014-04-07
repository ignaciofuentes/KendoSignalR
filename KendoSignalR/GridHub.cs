using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoSignalR
{
    public class GridHub : Hub
    {
        private GamesDbEntities db;
        public GridHub()
        {
            db = new GamesDbEntities();
        }

        public IEnumerable<GameViewModel> Read()
        {
            return from game in db.Games
                   select new GameViewModel { Id = game.Id, Name = game.Name, Developer = game.Developer };
        }

        public void Update(GameViewModel gameVm)
        {
            var g = db.Games.Where(game => game.Id == gameVm.Id).SingleOrDefault();
            if (g != null)
            {
                g.Name = gameVm.Name;
                g.Developer = gameVm.Developer;
                db.SaveChanges();
                Clients.Others.update(gameVm);
            }

        }

        public void Destroy(GameViewModel gameVm)
        {
            var g = db.Games.Where(game => game.Id == gameVm.Id).SingleOrDefault();
            if (g != null)
            {
                db.Games.Remove(g);
                db.SaveChanges();
                Clients.Others.destroy(gameVm);
            }
        }

        public GameViewModel Create(GameViewModel gameVm)
        {
            var g = new Game { Name = gameVm.Name, Developer = gameVm.Developer };
            db.Games.Add(g);
            db.SaveChanges();
            gameVm.Id = g.Id;
            Clients.Others.create(gameVm);
            return gameVm;
        }
    }
}