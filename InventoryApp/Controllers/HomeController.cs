using System.Diagnostics;
using System.Linq;
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
            return View(room);    
        }
        
    }

    
    public IActionResult Delete(int id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            Item item = db.Items.Where(item => item.Id.Equals(id)).FirstOrDefault();

            db.Items.Remove(item);

            db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(EditDto editItem)
    {


        using (ApplicationContext db = new ApplicationContext())
        {
            
            var item = db.Items.Where(item => item.Id == editItem.Id).FirstOrDefault();

            if (editItem.Name != "")
            {
                item.Name = editItem.Name;
            }
            
            if (editItem.Picture != "")
            {
                item.Picture = editItem.Picture;
            }
            
            if (editItem.Quantity != 0)
            {
                item.Quantity = editItem.Quantity;
            }
            Console.WriteLine(editItem.RoomNumber);
            Console.WriteLine(item.RoomNumber);
            if (editItem.RoomNumber != String.Empty)
            {
                item.RoomNumber = editItem.RoomNumber;
            }


            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewData["Id"] = id;
        
        return View();
    }

    public class EditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string RoomNumber { get; set; }
        public string Picture { get; set; }
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