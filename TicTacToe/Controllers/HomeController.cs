using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
