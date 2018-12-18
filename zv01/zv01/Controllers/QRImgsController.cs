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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using zv01.Data;
using zv01.Models;

namespace zv01.Controllers
{
    public class QRImgsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private IConfiguration _configuration;

        //public QRImgsController(IConfiguration Configuration)
        //{
        //    _configuration = Configuration;
        //}

        public IActionResult Create()
        {


            return View();
        }
        public IActionResult Details()
        {
            //List<Evento> eventList = await _context.Evento.(x=>x.EventName).ToListAsync();

            return View();
        }

        public IActionResult QrChecker(string codigoQr)
        {
            return View();
        }

        //    [HttpPost("UploadFiles")]
        //    public async Task<IActionResult> PostQr(List<IFormFile> files)
        //    {
        //        var uploadSuccess = false;

        //        foreach (var formFile in files)
        //        {
        //            if (formFile.Length <= 0)
        //            {
        //                continue;
        //            }

        //            // NOTE: uncomment either OPTION A or OPTION B to use one approach over another

        //            // OPTION A: convert to byte array before upload
        //            //using (var ms = new MemoryStream())
        //            //{
        //            //    formFile.CopyTo(ms);
        //            //    var fileBytes = ms.ToArray();
        //            //    uploadSuccess = await UploadToBlob(formFile.FileName, fileBytes, null);

        //            //}

        //            // OPTION B: read directly from stream for blob upload      
        //            using (var stream = formFile.OpenReadStream())
        //            {
        //                uploadSuccess = await UploadToBlob(formFile.FileName, null, stream);
        //            }

        //        }

        //        if (uploadSuccess)
        //            return View("UploadSuccess");
        //        else
        //            return View("UploadError");
        //    }

        //    private async Task<bool> UploadToBlob(string filename, byte[] imageBuffer = null, Stream stream = null)
        //    {
        //        CloudStorageAccount storageAccount = null;
        //        CloudBlobContainer cloudBlobContainer = null;
        //        string storageConnectionString = _configuration["AzureStorageConfig"];

        //        // Check whether the connection string can be parsed.
        //        if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
        //        {
        //            try
        //            {
        //                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
        //                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

        //                // Create a container called 'uploadblob' and append a GUID value to it to make the name unique. 
        //                cloudBlobContainer = cloudBlobClient.GetContainerReference("uploadblob" + Guid.NewGuid().ToString());
        //                await cloudBlobContainer.CreateAsync();

        //                // Set the permissions so the blobs are public. 
        //                BlobContainerPermissions permissions = new BlobContainerPermissions
        //                {
        //                    PublicAccess = BlobContainerPublicAccessType.Blob
        //                };
        //                await cloudBlobContainer.SetPermissionsAsync(permissions);

        //                // Get a reference to the blob address, then upload the file to the blob.
        //                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);

        //                if (imageBuffer != null)
        //                {
        //                    // OPTION A: use imageBuffer (converted from memory stream)
        //                    await cloudBlockBlob.UploadFromByteArrayAsync(imageBuffer, 0, imageBuffer.Length);
        //                }
        //                else if (stream != null)
        //                {
        //                    // OPTION B: pass in memory stream directly
        //                    await cloudBlockBlob.UploadFromStreamAsync(stream);
        //                }
        //                else
        //                {
        //                    return false;
        //                }

        //                return true;
        //            }
        //            catch (StorageException ex)
        //            {
        //                return false;
        //            }
        //            finally
        //            {
        //                // OPTIONAL: Clean up resources, e.g. blob container
        //                //if (cloudBlobContainer != null)
        //                //{
        //                //    await cloudBlobContainer.DeleteIfExistsAsync();
        //                //}
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }

        //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //    public IActionResult Error()
        //    {
        //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }


        public QRImgsController(ApplicationDbContext context)
        {
            _context = context;
        }


        //    // GET: QRImgs/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var qRImg = await _context.QRImg
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (qRImg == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(qRImg);
        //    }

        //    // GET: QRImgs/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: QRImgs/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("Id,QRUrl")] QRImg qRImg)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(qRImg);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(qRImg);
        //    }

        //    // GET: QRImgs/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var qRImg = await _context.QRImg.FindAsync(id);
        //        if (qRImg == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(qRImg);
        //    }

        //    // POST: QRImgs/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,QRUrl")] QRImg qRImg)
        //    {
        //        if (id != qRImg.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(qRImg);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!QRImgExists(qRImg.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(qRImg);
        //    }

        //    // GET: QRImgs/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var qRImg = await _context.QRImg
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (qRImg == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(qRImg);
        //    }

        //    // POST: QRImgs/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var qRImg = await _context.QRImg.FindAsync(id);
        //        _context.QRImg.Remove(qRImg);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool QRImgExists(int id)
        //    {
        //        return _context.QRImg.Any(e => e.Id == id);
        //    }
    }
}
