using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using test.Models;

namespace test.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ShowCode([FromBody]string text)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.RedirectStandardError = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        System.IO.File.WriteAllText("test.cpp", text);

        var compile = $"g++ test.cpp -o test && test.exe";
        cmd.StandardInput.WriteLine(compile);
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();

        string output = cmd.StandardOutput.ReadToEnd();
        output += cmd.StandardError.ReadToEnd();
        string returnValue = "";
        string[] trash = new string[] {"Microsoft", "(c)", "C:\\", "\r"};
        cmd.WaitForExit();
        Console.WriteLine(output);
        foreach(var str in output.Split("\n"))
        {
            bool valid = true;
            foreach(var i in trash)
            {
                if(str.StartsWith(i))
                {
                    valid = false;
                    break;
                }
            }
            if(valid)
            {
                returnValue += str + '\n';
            }
        }
        
        return Json(returnValue);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
