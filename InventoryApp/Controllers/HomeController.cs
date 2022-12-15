using System.Diagnostics;
using InventoryApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Models;
using InventoryApp.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult AddItem()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddItem(AddItemDto addItemDto)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var item = new Item {Name = addItemDto.Name,
                RoomNumber = addItemDto.RoomNumber,
                Quantity = addItemDto.Quantity};

            db.Items.Add(item);
            db.SaveChanges();
        }
        
        

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            List<Room> rooms = db.Rooms.ToList();
            return View(rooms);
        }

        
    }
    
    public IActionResult Detail(string roomNum)
    {
        
        using (ApplicationContext db = new ApplicationContext())
        {
            List<Item> room = db.Items.Where(item => item.RoomNumber == roomNum).ToList();
            Console.WriteLine(room.Count);
            return View(room);    
        }
        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}