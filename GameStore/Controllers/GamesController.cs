using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GameStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GameStore.Controllers
{
  public class GamesController : Controller
  {
    private readonly GameStoreContext _db;
    public GamesController(GameStoreContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Game> model = _db.Games.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Game game)
    {
      _db.Games.Add(game);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisGame = _db.Games
        .Include(game => game.JoinEntities)
        .ThenInclude(join => join.Customer)
        .FirstOrDefault(game => game.GameId == id);
      // ViewBag.CustomerGame = new SelectList(_db.CustomerGame, "GameId", "Returned");
      return View(thisGame);
    }
    public ActionResult Edit(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }
    [HttpPost]
    public ActionResult Edit(Game game)
    {
      _db.Entry(game).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      _db.Games.Remove(thisGame);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult CheckoutGame(int id)
    {  
      var thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "CustomerName");
      return View(thisGame);
    }

    [HttpPost]
    public ActionResult CheckoutGame(Game game, int CustomerId)
    {
      var joinGame = _db.Games.FirstOrDefault(entry => entry.GameId == game.GameId);
      joinGame.Inventory -= 1;
      if (CustomerId != 0)
      {
      _db.CustomerGame.Add(new CustomerGame() { CustomerId = CustomerId, GameId = game.GameId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}