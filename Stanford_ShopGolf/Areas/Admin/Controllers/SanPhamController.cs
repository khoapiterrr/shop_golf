using Stanford.ShopGolf.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using PagedList;
using Stanford_ShopGolf.Areas.Admin.Models;
using static Stanford_ShopGolf.Areas.Admin.CommonAdmin.ConstantData;

namespace Stanford_ShopGolf.Areas.Admin.Controllers
{
    public class SanPhamController : AdminBaseController
    {
        // GET: SanPham

        public ActionResult DanhSach(SanPhamSearchModel searchModel)
        {
            IQueryable<SanPham> lstSanPham = DataProvider.Entities.SanPhams.Where(p => p.DaXoa != true);
            if (!string.IsNullOrEmpty(searchModel.q))
            {
                lstSanPham = lstSanPham.Where(p => p.TenSanPham.Contains(searchModel.q) || p.MoTa.Contains(searchModel.q) || p.MaSanPham.Contains(searchModel.q));
            }
            if (searchModel.ddlGioiTinh.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.GenderName.Contains(searchModel.ddlGioiTinh.Value.ToString()));
            }

            if (searchModel.ddlChuDe.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.ChuDeId == searchModel.ddlChuDe.Value);
            }

            if (searchModel.ddlLoai.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.LoaiId == searchModel.ddlLoai.Value);
            }

            if (searchModel.ddlNhanHieu.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.BrandId == searchModel.ddlNhanHieu.Value);
            }
            //giá từ

            if (searchModel.giatu.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.GiaBan >= searchModel.giatu.Value);
            }
            //giá đến
            if (searchModel.giaden.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.GiaBan <= searchModel.giaden.Value);
            }
            ViewBag.pageSize = searchModel.pageSize;
            ViewBag.q = searchModel.q;
            ViewBag.GioiTinh = searchModel.ddlGioiTinh;
            ViewBag.ChuDe = searchModel.ddlChuDe;
            ViewBag.NhanHieu = searchModel.ddlNhanHieu;
            ViewBag.giatu = searchModel.giatu;
            ViewBag.giaden = searchModel.giaden;

            LoadDdl();
            return View(lstSanPham.OrderByDescending(p => p.NgayCapNhat).ToPagedList(searchModel.page.HasValue ? searchModel.page.Value : 1, searchModel.pageSize.HasValue ? (int)searchModel.pageSize : 10));
        }

        public ActionResult DanhSachDaXoa(SanPhamSearchModel searchModel)
        {
            IQueryable<SanPham> lstSanPham = DataProvider.Entities.SanPhams.Where(p => p.DaXoa == true);
            if (!string.IsNullOrEmpty(searchModel.q))
            {
                lstSanPham = lstSanPham.Where(p => p.TenSanPham.Contains(searchModel.q) || p.MoTa.Contains(searchModel.q) || p.MaSanPham.Contains(searchModel.q));
            }
            if (searchModel.ddlGioiTinh.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.GenderId == searchModel.ddlGioiTinh.Value);
            }

            if (searchModel.ddlChuDe.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.ChuDeId == searchModel.ddlChuDe.Value);
            }

            if (searchModel.ddlLoai.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.LoaiId == searchModel.ddlLoai.Value);
            }

            if (searchModel.ddlNhanHieu.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.BrandId == searchModel.ddlNhanHieu.Value);
            }
            //giá từ

            if (searchModel.giatu.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.GiaBan >= searchModel.giatu.Value);
            }
            //giá đến
            if (searchModel.giaden.HasValue)
            {
                lstSanPham = lstSanPham.Where(p => p.GiaBan <= searchModel.giaden.Value);
            }

            ViewBag.pageSize = searchModel.pageSize;
            ViewBag.q = searchModel.q;
            ViewBag.GioiTinh = searchModel.ddlGioiTinh;
            ViewBag.ChuDe = searchModel.ddlChuDe;
            ViewBag.NhanHieu = searchModel.ddlNhanHieu;
            ViewBag.giatu = searchModel.giatu;
            ViewBag.giaden = searchModel.giaden;

            LoadDdl();
            return View(lstSanPham.OrderByDescending(p => p.NgayCapNhat).ToPagedList(searchModel.page.HasValue ? searchModel.page.Value : 1, searchModel.pageSize.HasValue ? (int)searchModel.pageSize : 10));
        }

        /// <summary>
        /// Hàm để load các thuộc tính của sản phẩm lên giao diện thêm mới, sửa
        /// Author          Date            Comments
        /// DangBQ      30/09/2019  Tạo mới
        /// </summary>
        /// <param name="objSP">Đối tượng sản phẩm nếu có để selected trong TH sửa</param>
        private void LoadDropDownList(SanPham objSP = null)
        {
            //Lấy danh sách thương hiệu
            List<Brand> lstBrand = DataProvider.Entities.Brands.ToList();
            ViewBag.ddlBrand = new SelectList(lstBrand, "Id", "BrandName", objSP.BrandId.HasValue ? objSP.BrandId.Value : 0);

            //Lấy danh sách chủ đề
            var lstChuDe = Service.GetListParent("...", 0);

            ViewBag.ddlChuDe = new SelectList(lstChuDe, "Id", "TenChuDe", objSP.ChuDeId.HasValue ? objSP.ChuDeId.Value : 0);

            List<string> lstSizeChose = new List<string>();

            if (objSP != null && objSP.SanPham_Size != null)
            {
                lstSizeChose = objSP.SanPham_Size.Where(p => p.SizeId.HasValue).Select(p => p.SizeId.Value.ToString()).ToList();
            }

            var lstSize = DataProvider.Entities.Sizes.ToList();
            ViewBag.ddlSize = new MultiSelectList(lstSize, "Id", "SizeName", lstSizeChose);

            #region Thuận tay

            List<SelectListItem> lstHand = new List<SelectListItem>();
            SelectListItem item = new SelectListItem() { Value = "Tay phải", Text = "Tay phải" };
            lstHand.Add(item);

            item = new SelectListItem() { Value = "Tay trái", Text = "Tay trái" };
            lstHand.Add(item);

            #endregion Thuận tay

            List<string> lstHandChoose = new List<string>();
            if (objSP != null && !string.IsNullOrEmpty(objSP.RightLeftHand))
            {
                lstHandChoose = objSP.RightLeftHand.Split(';').ToList();
            }

            ViewBag.ddlHand = new MultiSelectList(lstHand, "Value", "Text", lstHandChoose);

            //Hiển thị danh sách quốc gia
            var lstQuocGia = DataProvider.Entities.QuocGias.ToList();

            ViewBag.ddlQuocGia = new SelectList(lstQuocGia, "Id", "TenQuocGia");

            //Hiển thị danh sách trạng thái
            var lstTrangThai = DataProvider.Entities.TrangThais.ToList();

            ViewBag.ddlTrangThai = new SelectList(lstTrangThai, "Id", "TenTrangThai");

            #region Giới tính

            IEnumerable<SelectListItem> lstGender = DataProvider.Entities.Genders.Select(p => new SelectListItem
            {
                Text = p.GenderName,
                Value = p.Id.ToString()
            }).ToList();

            List<string> lstGenderChose = new List<string>();

            if (objSP != null && !string.IsNullOrEmpty(objSP.GenderName))
            {
                lstGenderChose = objSP.GenderName.Split(';').ToList();
            }

            ViewBag.ddlGender = new MultiSelectList(lstGender, "Value", "Text", lstGenderChose);

            #endregion Giới tính

            var lstLoai = DataProvider.Entities.LoaiSanPhams.ToList();
            ViewBag.ddlLoai = new SelectList(lstLoai, "Id", "TenLoai");

            //Color
            var lstColor = DataProvider.Entities.Colors.ToList();
            List<string> lstColorChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Color != null)
            {
                lstColorChoose = objSP.SanPham_Color.Where(p => p.ColorId.HasValue).Select(p => p.ColorId.Value.ToString()).ToList();
            }
            ViewBag.ddlColor = new MultiSelectList(lstColor, "Id", "ColorName", lstColorChoose);

            //Width
            List<string> lstWidthChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Width != null)
            {
                lstWidthChoose = objSP.SanPham_Width.Where(p => p.WidthId.HasValue).Select(p => p.WidthId.Value.ToString()).ToList();
            }
            var lstWidth = DataProvider.Entities.Widths.ToList();
            ViewBag.ddlWidth = new MultiSelectList(lstWidth, "Id", "WidthName", lstWidthChoose);

            #region Các thuộc tính của gậy

            //Độ cứng của gậy
            var lstFlex = DataProvider.Entities.Flexes.ToList();
            List<string> lstFlexChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Flex != null)
            {
                lstFlexChoose = objSP.SanPham_Flex.Where(p => p.FlexId.HasValue).Select(p => p.FlexId.Value.ToString()).ToList();
            }
            ViewBag.ddlFlex = new MultiSelectList(lstFlex, "Id", "FlexName", lstFlexChoose);

            //Độ (góc) loft của gậy
            var lstLoft = DataProvider.Entities.Lofts.ToList();
            List<string> lstLoftChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Loft != null)
            {
                lstLoftChoose = objSP.SanPham_Loft.Where(p => p.LoftId.HasValue).Select(p => p.LoftId.Value.ToString()).ToList();
            }
            ViewBag.ddlLoft = new MultiSelectList(lstLoft, "Id", "LoftDegress", lstLoftChoose);

            //Góc bounce của gậy
            var lstBounce = DataProvider.Entities.Bounces.ToList();
            List<string> lstBounceChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Bounce != null)
            {
                lstBounceChoose = objSP.SanPham_Bounce.Where(p => p.BounceId.HasValue).Select(p => p.BounceId.Value.ToString()).ToList();
            }
            ViewBag.ddlBounce = new MultiSelectList(lstBounce, "Id", "BounceName", lstBounceChoose);

            //Length size của gậy
            List<string> lstLengthChoose = new List<string>();

            if (objSP != null && objSP.SanPham_LengthSize != null)
            {
                lstLengthChoose = objSP.SanPham_LengthSize.Where(p => p.LengthId.HasValue).Select(p => p.LengthId.Value.ToString()).ToList();
            }

            var lstLength = DataProvider.Entities.LengthSizes.ToList();
            ViewBag.ddlLength = new MultiSelectList(lstLength, "Id", "LengthSize1", lstLengthChoose);

            //Shaft của gậy
            List<string> lstShaftChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Shaft != null)
            {
                lstShaftChoose = objSP.SanPham_Shaft.Where(p => p.ShaftId.HasValue).Select(p => p.ShaftId.Value.ToString()).ToList();
            }

            var lstShaft = DataProvider.Entities.Shafts.ToList();
            ViewBag.ddlShaft = new MultiSelectList(lstShaft, "Id", "ShaftName", lstShaftChoose);

            #endregion Các thuộc tính của gậy

            #region Các thuộc tính áo

            var lstWaist = DataProvider.Entities.Waists.ToList();
            List<string> lstWaistChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Waist != null)
            {
                lstWaistChoose = objSP.SanPham_Waist.Where(p => p.WaistId.HasValue).Select(p => p.WaistId.Value.ToString()).ToList();
            }
            ViewBag.ddlWaist = new MultiSelectList(lstWaist, "Id", "WaistName", lstWaistChoose);

            var lstInseam = DataProvider.Entities.Inseams.ToList();
            List<string> lstInseamChoose = new List<string>();

            if (objSP != null && objSP.SanPham_Inseam != null)
            {
                lstInseamChoose = objSP.SanPham_Inseam.Where(p => p.InseamId.HasValue).Select(p => p.InseamId.Value.ToString()).ToList();
            }
            ViewBag.ddlInseam = new MultiSelectList(lstInseam, "Id", "InseamName", lstInseamChoose);

            #endregion Các thuộc tính áo
        }

        private void LoadDdl()
        {
            ViewBag.ddlChuongTrinhUuDai = DataProvider.Entities.ChuongTrinhUuDais.Select(x => new SelectListItem
            {
                Text = x.TenChuongTrinh,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.ddlChuDe = Service.GetListParent("...", 0).Select(p => new SelectListItem
            {
                Text = p.TenChuDe,
                Value = p.Id.ToString()
            }).ToList();
            ViewBag.ddlLoai = DataProvider.Entities.LoaiSanPhams.Select(p => new SelectListItem
            {
                Text = p.TenLoai,
                Value = p.Id.ToString()
            }).ToList();
            ViewBag.ddlNhanHieu = DataProvider.Entities.Brands.Select(p => new SelectListItem
            {
                Text = p.BrandName,
                Value = p.Id.ToString()
            }).ToList();

            ViewBag.ddlGioiTinh = DataProvider.Entities.Genders.Select(p => new SelectListItem
            {
                Text = p.GenderName,
                Value = p.Id.ToString()
            }).ToList();
        }

        public ActionResult Sua(int id)
        {
            SanPham objSanPham = DataProvider.Entities.SanPhams.Where(p => p.Id == id).First();

            LoadDropDownList(objSanPham);

            return View(objSanPham);
        }

        /// <summary>
        /// Hàm xử lý cập nhật thông tin sản phẩm
        /// Author                  Date                Comments
        /// DangBQ              04/10/2019      Tạo mới
        /// </summary>
        /// <param name="objSanPham"></param>
        /// <param name="SizeId"></param>
        /// <param name="GenderId1"></param>
        /// <param name="ColorId"></param>
        /// <param name="HandId"></param>
        /// <param name="FlexId"></param>
        /// <param name="LoftId"></param>
        /// <param name="BounceId"></param>
        /// <param name="LengthId"></param>
        /// <param name="ShaftId"></param>
        /// <param name="WidthId"></param>
        /// <param name="WaistId"></param>
        /// <param name="InseamId"></param>
        /// <param name="fileUpload"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Sua(SanPham objSanPham, List<string> SizeId, List<string> GenderId1, List<string> ColorId, List<string> HandId, List<string> FlexId, List<string> LoftId, List<string> BounceId, List<string> LengthId, List<string> ShaftId, List<string> WidthId, List<string> WaistId, List<string> InseamId, HttpPostedFileBase fileUpload, HttpPostedFileBase[] files)
        {
            SanPham objSP = DataProvider.Entities.SanPhams.Where(p => p.Id == objSanPham.Id).First();

            if (objSanPham != null)
            {
                //Gán thông tin cho sản phẩm
                objSanPham.NgayCapNhat = DateTime.Now;
                //Nếu chọn ảnh đại diện
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    fileUpload.SaveAs(Server.MapPath("~/Content/Images/SanPham/" + fileUpload.FileName));
                    objSanPham.AnhDaiDien = fileUpload.FileName;
                }
                else
                {
                    objSanPham.AnhDaiDien = objSP.AnhDaiDien;
                }
                if (HandId != null)
                {
                    string thuanTay = string.Join(";", HandId);
                    objSanPham.RightLeftHand = thuanTay;
                }

                if (GenderId1 != null)
                {
                    string gioiTinh = string.Join(";", GenderId1);
                    objSanPham.GenderName = gioiTinh;
                }

                #region Thêm ảnh sản phẩm

                List<AnhSanPham> lstAnhSP = new List<AnhSanPham>();
                foreach (var item in files)
                {
                    var objAnhSanPham = new AnhSanPham();

                    if (item != null && item.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(item.FileName);

                        item.SaveAs(Server.MapPath("~/Content/Images/SanPham/AnhSP/" + fileName));

                        objAnhSanPham.Avatar = fileName;
                        objAnhSanPham.SanPham = objSP;
                        objAnhSanPham.SanPhamId = objSP.Id;
                        objAnhSanPham.TenAnh = fileName;
                        lstAnhSP.Add(objAnhSanPham);
                    }
                }

                if (lstAnhSP != null && lstAnhSP.Count > 0)
                {
                    objSP.AnhSanPhams = lstAnhSP;
                    DataProvider.Entities.AnhSanPhams.AddRange(lstAnhSP);
                }

                #endregion Thêm ảnh sản phẩm

                #region Thêm các size của sản phẩm

                if (SizeId != null && SizeId.Count > 0)
                {
                    //Xóa cũ đi
                    if (objSP.SanPham_Size != null && objSP.SanPham_Size.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Size.RemoveRange(objSP.SanPham_Size);
                    }

                    for (int i = 0; i < SizeId.Count; i++)
                    {
                        SanPham_Size objSize = new SanPham_Size();
                        objSize.SanPhamId = objSP.Id;
                        objSize.SizeId = int.Parse(SizeId[i]);
                        DataProvider.Entities.SanPham_Size.Add(objSize);
                    }
                }

                #endregion Thêm các size của sản phẩm

                #region Thêm các màu sắc của sản phẩm

                if (ColorId != null && ColorId.Count > 0)
                {
                    if (objSP.SanPham_Color != null && objSP.SanPham_Color.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Color.RemoveRange(objSP.SanPham_Color);
                    }

                    for (int i = 0; i < ColorId.Count; i++)
                    {
                        SanPham_Color obj = new SanPham_Color();
                        int colorId = 0;
                        int.TryParse(ColorId[i], out colorId);

                        obj.ColorId = colorId;
                        obj.SanPham = objSP;

                        //Thêm màu cho sp
                        DataProvider.Entities.SanPham_Color.Add(obj);
                    }
                }

                #endregion Thêm các màu sắc của sản phẩm

                #region Thêm các Flex của sản phẩm

                if (FlexId != null && FlexId.Count > 0)
                {
                    if (objSP.SanPham_Flex != null && objSP.SanPham_Flex.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Flex.RemoveRange(objSP.SanPham_Flex);
                    }

                    for (int i = 0; i < FlexId.Count; i++)
                    {
                        SanPham_Flex obj = new SanPham_Flex();
                        int flexId = 0;
                        int.TryParse(FlexId[i], out flexId);
                        obj.FlexId = flexId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Flex.Add(obj);
                    }
                }

                #endregion Thêm các Flex của sản phẩm

                #region Thêm các Loft của sản phẩm

                if (LoftId != null && LoftId.Count > 0)
                {
                    if (objSP.SanPham_Loft != null && objSP.SanPham_Loft.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Loft.RemoveRange(objSP.SanPham_Loft);
                    }

                    for (int i = 0; i < LoftId.Count; i++)
                    {
                        SanPham_Loft obj = new SanPham_Loft();
                        int loftId = 0;
                        int.TryParse(LoftId[i], out loftId);
                        obj.LoftId = loftId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Loft.Add(obj);
                    }
                }

                #endregion Thêm các Loft của sản phẩm

                #region Thêm các Bounce của sản phẩm

                if (BounceId != null && BounceId.Count > 0)
                {
                    if (objSP.SanPham_Bounce != null && objSP.SanPham_Bounce.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Bounce.RemoveRange(objSP.SanPham_Bounce);
                    }

                    for (int i = 0; i < BounceId.Count; i++)
                    {
                        SanPham_Bounce obj = new SanPham_Bounce();
                        int bounceId = 0;
                        int.TryParse(BounceId[i], out bounceId);
                        obj.BounceId = bounceId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Bounce.Add(obj);
                    }
                }

                #endregion Thêm các Bounce của sản phẩm

                #region Thêm các Length của sản phẩm

                if (LengthId != null && LengthId.Count > 0)
                {
                    if (objSP.SanPham_LengthSize != null && objSP.SanPham_LengthSize.Count > 0)
                    {
                        DataProvider.Entities.SanPham_LengthSize.RemoveRange(objSP.SanPham_LengthSize);
                    }

                    for (int i = 0; i < LengthId.Count; i++)
                    {
                        SanPham_LengthSize obj = new SanPham_LengthSize();
                        int lengthId = 0;
                        int.TryParse(LengthId[i], out lengthId);
                        obj.LengthId = lengthId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_LengthSize.Add(obj);
                    }
                }

                #endregion Thêm các Length của sản phẩm

                #region Thêm các Shaft của sản phẩm

                if (ShaftId != null && ShaftId.Count > 0)
                {
                    if (objSP.SanPham_Shaft != null && objSP.SanPham_Shaft.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Shaft.RemoveRange(objSP.SanPham_Shaft);
                    }

                    for (int i = 0; i < ShaftId.Count; i++)
                    {
                        SanPham_Shaft obj = new SanPham_Shaft();
                        int shaftId = 0;
                        int.TryParse(ShaftId[i], out shaftId);
                        obj.ShaftId = shaftId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Shaft.Add(obj);
                    }
                }

                #endregion Thêm các Shaft của sản phẩm

                #region Thêm các Width của sản phẩm

                if (WidthId != null && WidthId.Count > 0)
                {
                    if (objSP.SanPham_Width != null && objSP.SanPham_Width.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Width.RemoveRange(objSP.SanPham_Width);
                    }

                    for (int i = 0; i < WidthId.Count; i++)
                    {
                        SanPham_Width obj = new SanPham_Width();
                        int widthId = 0;
                        int.TryParse(WidthId[i], out widthId);
                        obj.WidthId = widthId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Width.Add(obj);
                    }
                }

                #endregion Thêm các Width của sản phẩm

                #region Thêm các Waist của sản phẩm

                if (WaistId != null && WaistId.Count > 0)
                {
                    if (objSP.SanPham_Waist != null && objSP.SanPham_Waist.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Waist.RemoveRange(objSP.SanPham_Waist);
                    }

                    for (int i = 0; i < WaistId.Count; i++)
                    {
                        SanPham_Waist obj = new SanPham_Waist();
                        int waistId = 0;
                        int.TryParse(WaistId[i], out waistId);
                        obj.WaistId = waistId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Waist.Add(obj);
                    }
                }

                #endregion Thêm các Waist của sản phẩm

                #region Thêm các Inseam của sản phẩm

                if (InseamId != null && InseamId.Count > 0)
                {
                    if (objSP.SanPham_Inseam != null && objSP.SanPham_Inseam.Count > 0)
                    {
                        DataProvider.Entities.SanPham_Inseam.RemoveRange(objSP.SanPham_Inseam);
                    }

                    for (int i = 0; i < InseamId.Count; i++)
                    {
                        SanPham_Inseam obj = new SanPham_Inseam();
                        int inseamId = 0;
                        int.TryParse(InseamId[i], out inseamId);
                        obj.InseamId = inseamId;
                        obj.SanPham = objSP;
                        DataProvider.Entities.SanPham_Inseam.Add(obj);
                    }
                }

                #endregion Thêm các Inseam của sản phẩm

                objSanPham.NgayCapNhat = DateTime.Now;
                objSanPham.NgayTao = objSP.NgayTao;
                DataProvider.Entities.Entry(objSP).CurrentValues.SetValues(objSanPham);
                //Lưu lại thông tin
                DataProvider.Entities.SaveChanges();

                return RedirectToAction("DanhSach");
            }

            LoadDropDownList(objSanPham);

            return View();
        }

        public ActionResult ThemMoi()
        {
            LoadDropDownList(new SanPham());
            return View();
        }

        /// <summary>
        /// Hàm xử lý thêm mới sản phẩm
        /// Author          Date            Comments
        /// DangBQ      30/09/2019  Tạo mới
        /// </summary>
        /// <param name="objSanPham">Đối tượng sản phẩm cần thêm</param>
        /// <param name="SizeId"></param>
        /// <param name="GenderId1"></param>
        /// <param name="ColorId"></param>
        /// <param name="HandId"></param>
        /// <param name="FlexId"></param>
        /// <param name="LoftId"></param>
        /// <param name="BounceId"></param>
        /// <param name="LengthId"></param>
        /// <param name="ShaftId"></param>
        /// <param name="WidthId"></param>
        /// <param name="WaistId"></param>
        /// <param name="InseamId"></param>
        /// <param name="fileUpload"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemMoi(SanPham objSanPham, List<string> SizeId, List<string> GenderId1, List<string> ColorId, List<string> HandId, List<string> FlexId, List<string> LoftId, List<string> BounceId, List<string> LengthId, List<string> ShaftId, List<string> WidthId, List<string> WaistId, List<string> InseamId, HttpPostedFileBase fileUpload, HttpPostedFileBase[] files)
        {
            if (objSanPham != null)
            {
                //Gán thông tin cho sản phẩm
                objSanPham.NgayTao = DateTime.Now;
                objSanPham.NgayCapNhat = DateTime.Now;
                NguoiDung objUser = (NguoiDung)Session[ConstantData.USER_SESSION];

                if (objUser != null)
                    objSanPham.NguoiTaoId = objUser.Id;

                //Nếu chọn ảnh đại diện
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    fileUpload.SaveAs(Server.MapPath("~/Content/Images/SanPham/" + fileUpload.FileName));
                    objSanPham.AnhDaiDien = fileUpload.FileName;
                }
                if (HandId != null)
                {
                    string thuanTay = string.Join(";", HandId);
                    objSanPham.RightLeftHand = thuanTay;
                }

                if (GenderId1 != null)
                {
                    string gioiTinh = string.Join(";", GenderId1);
                    objSanPham.GenderName = gioiTinh;
                }

                #region Thêm ảnh sản phẩm

                List<AnhSanPham> lstAnhSP = new List<AnhSanPham>();
                foreach (var item in files)
                {
                    var objAnhSanPham = new AnhSanPham();

                    if (item != null && item.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(item.FileName);

                        item.SaveAs(Server.MapPath("~/Content/Images/SanPham/AnhSP/" + fileName));

                        objAnhSanPham.Avatar = fileName;
                        objAnhSanPham.SanPham = objSanPham;
                        objAnhSanPham.TenAnh = fileName;
                        lstAnhSP.Add(objAnhSanPham);
                    }
                }

                if (lstAnhSP != null && lstAnhSP.Count > 0)
                {
                    objSanPham.AnhSanPhams = lstAnhSP;
                }

                //Thêm vào model
                DataProvider.Entities.SanPhams.Add(objSanPham);

                #endregion Thêm ảnh sản phẩm

                #region Thêm các size của sản phẩm

                if (SizeId != null && SizeId.Count > 0)
                {
                    for (int i = 0; i < SizeId.Count; i++)
                    {
                        SanPham_Size objSize = new SanPham_Size();
                        objSize.SanPhamId = objSanPham.Id;
                        objSize.SizeId = int.Parse(SizeId[i]);
                        DataProvider.Entities.SanPham_Size.Add(objSize);
                    }
                }

                #endregion Thêm các size của sản phẩm

                #region Thêm các màu sắc của sản phẩm

                if (ColorId != null && ColorId.Count > 0)
                {
                    for (int i = 0; i < ColorId.Count; i++)
                    {
                        SanPham_Color obj = new SanPham_Color();
                        int colorId = 0;
                        int.TryParse(ColorId[i], out colorId);

                        obj.ColorId = colorId;
                        obj.SanPham = objSanPham;

                        //Thêm màu cho sp
                        DataProvider.Entities.SanPham_Color.Add(obj);
                    }
                }

                #endregion Thêm các màu sắc của sản phẩm

                #region Thêm các Flex của sản phẩm

                if (FlexId != null && FlexId.Count > 0)
                {
                    for (int i = 0; i < FlexId.Count; i++)
                    {
                        SanPham_Flex obj = new SanPham_Flex();
                        int flexId = 0;
                        int.TryParse(FlexId[i], out flexId);
                        obj.FlexId = flexId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Flex.Add(obj);
                    }
                }

                #endregion Thêm các Flex của sản phẩm

                #region Thêm các Loft của sản phẩm

                if (LoftId != null && LoftId.Count > 0)
                {
                    for (int i = 0; i < LoftId.Count; i++)
                    {
                        SanPham_Loft obj = new SanPham_Loft();
                        int loftId = 0;
                        int.TryParse(LoftId[i], out loftId);
                        obj.LoftId = loftId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Loft.Add(obj);
                    }
                }

                #endregion Thêm các Loft của sản phẩm

                #region Thêm các Bounce của sản phẩm

                if (BounceId != null && BounceId.Count > 0)
                {
                    for (int i = 0; i < BounceId.Count; i++)
                    {
                        SanPham_Bounce obj = new SanPham_Bounce();
                        int bounceId = 0;
                        int.TryParse(BounceId[i], out bounceId);
                        obj.BounceId = bounceId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Bounce.Add(obj);
                    }
                }

                #endregion Thêm các Bounce của sản phẩm

                #region Thêm các Length của sản phẩm

                if (LengthId != null && LengthId.Count > 0)
                {
                    for (int i = 0; i < LengthId.Count; i++)
                    {
                        SanPham_LengthSize obj = new SanPham_LengthSize();
                        int lengthId = 0;
                        int.TryParse(LengthId[i], out lengthId);
                        obj.LengthId = lengthId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_LengthSize.Add(obj);
                    }
                }

                #endregion Thêm các Length của sản phẩm

                #region Thêm các Shaft của sản phẩm

                if (ShaftId != null && ShaftId.Count > 0)
                {
                    for (int i = 0; i < ShaftId.Count; i++)
                    {
                        SanPham_Shaft obj = new SanPham_Shaft();
                        int shaftId = 0;
                        int.TryParse(ShaftId[i], out shaftId);
                        obj.ShaftId = shaftId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Shaft.Add(obj);
                    }
                }

                #endregion Thêm các Shaft của sản phẩm

                #region Thêm các Width của sản phẩm

                if (WidthId != null && WidthId.Count > 0)
                {
                    for (int i = 0; i < WidthId.Count; i++)
                    {
                        SanPham_Width obj = new SanPham_Width();
                        int widthId = 0;
                        int.TryParse(WidthId[i], out widthId);
                        obj.WidthId = widthId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Width.Add(obj);
                    }
                }

                #endregion Thêm các Width của sản phẩm

                #region Thêm các Waist của sản phẩm

                if (WaistId != null && WaistId.Count > 0)
                {
                    for (int i = 0; i < WaistId.Count; i++)
                    {
                        SanPham_Waist obj = new SanPham_Waist();
                        int waistId = 0;
                        int.TryParse(WaistId[i], out waistId);
                        obj.WaistId = waistId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Waist.Add(obj);
                    }
                }

                #endregion Thêm các Waist của sản phẩm

                #region Thêm các Inseam của sản phẩm

                if (InseamId != null && InseamId.Count > 0)
                {
                    for (int i = 0; i < InseamId.Count; i++)
                    {
                        SanPham_Inseam obj = new SanPham_Inseam();
                        int inseamId = 0;
                        int.TryParse(InseamId[i], out inseamId);
                        obj.InseamId = inseamId;
                        obj.SanPham = objSanPham;
                        DataProvider.Entities.SanPham_Inseam.Add(obj);
                    }
                }

                #endregion Thêm các Inseam của sản phẩm

                //Lưu lại thông tin
                DataProvider.Entities.SaveChanges();

                return RedirectToAction("Sua", new { id = objSanPham.Id });
            }

            LoadDropDownList();

            return View();
        }

        private IEnumerable<SelectListItem> GetSizeList()
        {
            // Initialization.
            SelectList lstobj = null;

            try
            {
                // Loading.
                var list = DataProvider.Entities.Sizes
                                  .Select(p =>
                                            new SelectListItem
                                            {
                                                Value = p.Id.ToString(),
                                                Text = p.SizeName
                                            });

                // Setting.
                lstobj = new SelectList(list, "Value", "Text");
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // info.
            return lstobj;
        }

        public ActionResult DuyetAll(int? trangthai, List<int> lstChecked)
        {
            try
            {
                if (lstChecked != null && lstChecked.Count > 0)
                {
                    var lstSP = DataProvider.Entities.SanPhams.Where(p => lstChecked.Contains(p.Id)).ToList();
                    if (trangthai == 1)
                    {
                        foreach (var item in lstSP)
                        {
                            item.DaDuyet = true;
                            item.NgayCapNhat = DateTime.Now;
                            item.NgayDuyet = DateTime.Now;
                        }
                    }
                    else if (trangthai == 2)
                    {
                        foreach (var item in lstSP)
                        {
                            item.DaDuyet = false;
                            item.NgayCapNhat = DateTime.Now;
                            item.NgayDuyet = DateTime.Now;
                        }
                    }

                    DataProvider.Entities.SaveChanges();
                }
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.SuccessGet,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DuyetOne(int? id)
        {
            try
            {
                if (id.HasValue && id > 0)
                {
                    var item = DataProvider.Entities.SanPhams.Find(id);
                    item.DaDuyet = !item.DaDuyet;
                    item.NgayCapNhat = DateTime.Now;
                    item.NgayDuyet = DateTime.Now;
                    DataProvider.Entities.SaveChanges();
                }
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.SuccessGet,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteAll(int? trangthai, List<int> lstChecked)
        {
            try
            {
                if (lstChecked != null && lstChecked.Count > 0)
                {
                    var lstSP = DataProvider.Entities.SanPhams.Where(p => lstChecked.Contains(p.Id)).ToList();
                    if (trangthai == 1)//xóa
                    {
                        foreach (var item in lstSP)
                        {
                            item.DaXoa = true;
                        }
                    }
                    else if (trangthai == 2)//bỏ xóa
                    {
                        foreach (var item in lstSP)
                        {
                            item.DaXoa = false;
                        }
                    }

                    DataProvider.Entities.SaveChanges();
                }
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.SuccessDelete,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse
                {
                    Message = ResponseMessage.Error,
                    Success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PhucHoi(int id, int type)
        {
            if (id > 0 && type > 0)
            {
                if (type == 1)//phục hồi
                {
                    var sp = DataProvider.Entities.SanPhams.Find(id);
                    sp.DaXoa = false;
                    DataProvider.Entities.SaveChanges();
                    return RedirectToAction("DanhSachDaXoa", "SanPham");
                }
                else // xóa
                {
                    var sp = DataProvider.Entities.SanPhams.Find(id);
                    sp.DaXoa = true;
                    DataProvider.Entities.SaveChanges();
                    return RedirectToAction("DanhSach", "SanPham");
                }
            }
            return RedirectToAction("DanhSach", "SanPham");
        }
    }
}