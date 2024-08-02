using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoItemController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        /* To save name and store it in a cookie (1 day). */
        public IActionResult SaveName(string userName)
        {       
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1) 
                };
                Response.Cookies.Append("UserName", userName, options);
                return RedirectToAction("Index");
        }
        /* createNew To do item page*/
        public IActionResult Create()
        {
            return View();
        }

        /*create and save new item*/
        public IActionResult newitem() 
        {

            return View();
        }

        [HttpPost]
        public IActionResult newitem(Item item)
        {
            TempData["createnew"] = $"Created {item.Title} successfuly";
            var result= context.Items.Add(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /*to Show the list of item*/
        public IActionResult Index() 
        {
            string userName = Request.Cookies["UserName"];
            ViewBag.UserName = userName;
            var result= context.Items.ToList();
            return View(result);
        }

        /*to Edit Item **************/
       public IActionResult Edit(int id)
        {
            var result = context.Items.Find(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            TempData["update"] = $"data update successfuly";
            context.Items.Update(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        /*To delete Item*****************/
        public IActionResult Delete(int id)
        {
            var result = context.Items.Find(id);
            if(result != null)
            {
                context.Items.Remove(result);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Notfound", "Home");
            }
        }
    }
}
