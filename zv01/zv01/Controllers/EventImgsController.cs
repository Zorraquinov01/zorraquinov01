using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using zv01.Data;
using zv01.Models;

namespace zv01.Controllers
{
    public class EventImgsController : Controller
    {
        private readonly AzureStorageConfig _storageConfig;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public EventImgsController(ApplicationDbContext context, IConfiguration Configuration, IOptions<AzureStorageConfig> storageConfig)
        {
            _storageConfig = storageConfig.Value;
            _context = context;
            _configuration = Configuration;


        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files, [Bind("id,ImgUrl")] EventImg eventImg, [Bind("Id,EventName,EventDate,Description,Place,AforoActual,AforoTotal,Visitas")] Evento evento, string time)
        {
            var uploadSuccess = false;

            foreach (var formFile in files)
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }

                // NOTE: uncomment either OPTION A or OPTION B to use one approach over another

                // OPTION A: convert to byte array before upload
                //using (var ms = new MemoryStream())
                //{
                //    formFile.CopyTo(ms);
                //    var fileBytes = ms.ToArray();
                //    uploadSuccess = await UploadToBlob(formFile.FileName, fileBytes, null);

                //}

                // OPTION B: read directly from stream for blob upload      
                using (var stream = formFile.OpenReadStream())
                {

                    uploadSuccess = await UploadFileToStorage(stream, formFile.FileName, _storageConfig, eventImg, evento, time);
                }

            }

            if (uploadSuccess)
            {
                return RedirectToAction("Create", "Eventos");
            }
            else
            {
                return View("UploadError");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig, [Bind("id,ImgUrl")] EventImg eventImg, [Bind("Id,EventName,EventDate,Description,Place,AforoActual,AforoTotal,Visitas")] Evento evento, string time)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(_storageConfig.EventImgContainer);

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromStreamAsync(fileStream);

            var blobUrl = blockBlob.Uri.AbsoluteUri;
            var img = new EventImg
            {
                ImgUrl = blobUrl
            };

            _context.Add(img);
            await _context.SaveChangesAsync();

            DateTimeOffset eventodate = evento.EventDate;
            var timeSpanVal = time.ToString().Split(':').Select(x => Convert.ToInt32(x)).ToList();
            TimeSpan ts = new TimeSpan(timeSpanVal[0], timeSpanVal[1], 00);
            evento.EventDate = eventodate.Add(ts);
            evento.Estado = _context.EstadoEventos.Single(x => x.Id == 1);
            evento.Imgs = img;

            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                
            }
            return await Task.FromResult(true);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // GET: EventImgs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventImg = await _context.EventImg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventImg == null)
            {
                return NotFound();
            }

            return View(eventImg);
        }

        // GET: EventImgs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventImgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImgUrl")] EventImg eventImg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventImg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventImg);
        }

        // GET: EventImgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventImg = await _context.EventImg.FindAsync(id);
            if (eventImg == null)
            {
                return NotFound();
            }
            return View(eventImg);
        }

        // POST: EventImgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImgUrl")] EventImg eventImg)
        {
            if (id != eventImg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventImg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventImgExists(eventImg.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventImg);
        }

        // GET: EventImgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventImg = await _context.EventImg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventImg == null)
            {
                return NotFound();
            }

            return View(eventImg);
        }

        // POST: EventImgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventImg = await _context.EventImg.FindAsync(id);
            _context.EventImg.Remove(eventImg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventImgExists(int id)
        {
            return _context.EventImg.Any(e => e.Id == id);
        }
    }
}
