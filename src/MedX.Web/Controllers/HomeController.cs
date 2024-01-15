using MedX.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedX.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
