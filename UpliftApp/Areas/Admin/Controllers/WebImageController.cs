using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Uplift.DataAccess.Data.IRepository;
using Uplift.Models;

namespace UpliftApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class WebImageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebImageController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            WebImages imageObj = new WebImages();
            if (id == null)
            {

            }
            else
            {
                imageObj = _unitOfWork.WebImage.Get(id.Value);
                if (imageObj == null)
                {
                    return NotFound();
                }
            }
            return View(imageObj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id, WebImages imageObj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {

                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    imageObj.Picture = p1;
                }

                if (imageObj.Id == 0)
                {
                    _unitOfWork.WebImage.Add(imageObj);
                }
                else
                {
                    var imageFromDb = _unitOfWork.WebImage.Get(id);

                    imageFromDb.Name = imageObj.Name;
                    if (files.Count > 0)
                    {
                        imageFromDb.Picture = imageObj.Picture;
                    }
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(imageObj);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.WebImage.GetAll() });

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var webImage = _unitOfWork.WebImage.Get(id);
            if (webImage == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.WebImage.Remove(webImage);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Successfully deleting" });

        }
        #endregion
    }
}
