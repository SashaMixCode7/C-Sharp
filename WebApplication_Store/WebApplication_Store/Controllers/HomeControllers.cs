using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication_Store.Models;


namespace WebApplication_Store.Controllers
{


    //[Route("api/[controller]/[action]")]
    [Route("{controller=Home}/{action=index}")]
    public class HomeController : Controller
    {
        
        private readonly ApplicationModel _context;
        private readonly string _token = "";
        public HomeController(ApplicationModel context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
         return View(await _context.Good.ToListAsync());
        }

        /*[HttpPost]
        public async Task<IActionResult> Index([FromBody] Dictionary<int, int>  goods_count )//Получение запроса от клиента
        {
            try
            {
                if (goods_count.Count > 0)
                {
                    string send_TG_bot = "Заказ - количество:";
                    DateTime date = DateTime.Now;

                    foreach (var por in goods_count)
                    {
                        Goods? goods = await _context.Good.FirstOrDefaultAsync(p => p.Id == por.Key);
                        send_TG_bot = $"(send_TG_bot) {por.Key}  - {por.Value};\n";
                    }
                    send_TG_bot = $"{send_TG_bot} Время заказа - {date};\n";
                    SendMessage(send_TG_bot);
                    await _context.SaveChangesAsync();
                }
                return View(await _context.Good.ToListAsync());
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { success = false, error = ex.Message });

            }
        }*/
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] Dictionary<int, int> goods_count)
        {
            try
            {
                if (goods_count.Count > 0)
                {
                    string send_TG_bot = "Заказ - количество:\n";

                    foreach (var por in goods_count)
                    {
                        Goods? goods = await _context.Good.FirstOrDefaultAsync(p => p.Id == por.Key);
                        send_TG_bot += $"{goods?.Name}: {por.Value}\n";
                    }

                    DateTime date = DateTime.Now;
                    send_TG_bot += $"Время заказа - {date}";

                    SendMessage(send_TG_bot);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }



        private async Task SendMessage(string text)
        {
            // Формирование URL для вызова метода sendMessage  
            string url = $"https://api.telegram.org/bot{_token}/sendMessage";

            // Создание объекта с параметрами запроса  
            var payload1 = new
            {
                chat_id = 7735526956,
                text = text.Trim()
            };

            

            string json1 = JsonSerializer.Serialize(payload1);
            //Console.WriteLine(json1);
            
            // Отправка POST-запроса с помощью HttpClient  
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
            }
        }
    }
}