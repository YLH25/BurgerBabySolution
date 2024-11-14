using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BurgerBabyApi.Models.EFModel;
using System.Text.Json.Serialization;
using System.Text.Json;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;


namespace BurgerBabyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImgController : Controller
    {
        private readonly BurgerBabyApiContext _context;

        public ImgController(BurgerBabyApiContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("index")]
        // GET: Img
        public async Task<JsonResult> Index()
        {
            var imgs = await _context.Imgs.ToListAsync();
            var imgfiles = new List<byte[]>();
            foreach (var img in imgs)
            {
                var filePath = Path.Combine("..", "BurgerBaby", "wwwroot","savedata", img.SaveName) ;
                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
                imgfiles.Add(imageBytes);
            }
            return new JsonResult(new { imgs, imgfiles });
        }

    }
}