using ECommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           var number1 = "1".ToInt32();
            Debug.WriteLineIf(number1 == 1, number1);

            var order1 = new Order()
            {
                Title = "Samsung Mobile",
                PlacedOn = DateTime.Now.AddDays(-10),
                Price = 12000.0F
            };

            bool isValidOrder =  order1.IsValid();
            Debug.WriteLine(isValidOrder);

            bool isAllValid = order1.IsAllValid();
            Debug.WriteLine(isAllValid);

           //Assert.IsNotNull(isAllValid);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Func<String, int, bool> predicate = (str, index) => str.Length == index;
            String[] words = { "orange", "apple", "Article", "elephant", "star", "and" };
            IEnumerable<String> aWords = words.Where(predicate).Select(str => str);

            foreach (String word in aWords)
            {
                Console.WriteLine(word);
                ViewBag.Message += word;
            }

            List<object> tasks = new List<object>() {
                
            };
            
            //Parallel.ForEach(employeeList, e => ProcessEmployee(e));


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }

    public static class MyExtension
    {
        public static Int32 ToInt32(this string stringInput)
        {
            
            return Convert.ToInt32(stringInput);
        }
    }
}