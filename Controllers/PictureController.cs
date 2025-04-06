using Microsoft.AspNetCore.Mvc;

namespace EInkFrame.Controllers;

using System.Text.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

[ApiController]
[Route("[controller]")]
public class PictureController : ControllerBase
{
    private readonly ILogger<PictureController> _logger;
    private string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
    private readonly IScreenController _screen;
    public PictureController(ILogger<PictureController> logger, IScreenController screen)
    {
        _logger = logger;
        _screen = screen;
        //ensure we have a image folder
        Directory.CreateDirectory(imageFolder);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> UploadAndResize(List<IFormFile> files)
    {
        var finalWidth = 600;
        var finalHeight = 448;
        using MemoryStream imageStream = new();
        await files[0].CopyToAsync(imageStream);
        imageStream.Position = 0;
        using var image = Image.Load(imageStream);
        image.Mutate(img => img.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Pad,
            Size = new Size() { Width = finalWidth, Height = finalHeight }
        }));
        var filePath = Path.Combine(imageFolder, files[0].FileName);
        image.Save(filePath);
        _screen.SetImage(filePath);
        return Ok();
    }
    [Route("list")]
    [HttpGet]
    public IActionResult GetPictures()
    {
        var images = Directory.GetFiles(imageFolder);
        var imageList = new List<HostedImage>();
        foreach (var image in images)
        {
            Console.WriteLine(image);
            var hosted = new HostedImage()
            {
                hostedPath = image.Replace(AppDomain.CurrentDomain.BaseDirectory, ""),
                filePath = image
            };
            Console.WriteLine(JsonSerializer.Serialize(hosted));
            imageList.Add(hosted);
        }

        return Ok(JsonSerializer.Serialize(imageList));
    }
    [Route("set")]
    [HttpPost]
    public IActionResult SetImage(string path)
    {
        if (System.IO.File.Exists(path))
        {
            _screen.SetImage(path);
            return Ok();
        }
        return BadRequest();
    }

}
