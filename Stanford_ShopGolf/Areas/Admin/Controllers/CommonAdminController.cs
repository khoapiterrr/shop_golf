using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class CommonAdminAdminController : AdminBaseController
    {
        // GET: CommonAdmin
        public JsonResult UploadSimpleFile()
        {
            if (Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = Request.Files["file"];
                var folder = Request["folderName"];
                if (httpPostedFile != null && folder != null)
                {
                    folder = AppDomain.CurrentDomain.BaseDirectory + folder;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    HttpPostedFileBase file = httpPostedFile;

                    file.SaveAs($"{folder}/{file.FileName}");
                    return Json(new JsonResponse()
                    {
                        Success = true,
                        Message = ConstantData.ResponseMessage.SuccessFile
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new JsonResponse()
            {
                Success = false,
                Message = ConstantData.ResponseMessage.Error
            }, JsonRequestBehavior.AllowGet);
        }
    }
}