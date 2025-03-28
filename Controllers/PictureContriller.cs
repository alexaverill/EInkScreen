using Microsoft.AspNetCore.Mvc;

namespace EInkFrame.Controllers;

[ApiController]
[Route("[controller]")]
public class PictureController : ControllerBase
{
    private readonly ILogger<PictureController> _logger;

    private readonly IScreenController _screen;
    public PictureController(ILogger<PictureController> logger, IScreenController screen)
    {
        _logger = logger;
        _screen = screen;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
    {
        long size = files.Sum(f => f.Length);
        Console.WriteLine(files.Count());
        var formFile = files[0];
        //note this is only ever runnin internal to network, not too worried about malicious attempts at the mometn
        var imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
        Directory.CreateDirectory(imageFolder);
        var filePath = Path.Combine(imageFolder, formFile.FileName);
        Console.WriteLine(filePath);
        using (var stream = System.IO.File.Create(filePath))
        {
            await formFile.CopyToAsync(stream);
        }
        Console.WriteLine("Setting Screen");
        _screen.SetImage(filePath);
        //Call Inky to write image to 

        return Ok(new { count = files.Count, size });
    }
}
