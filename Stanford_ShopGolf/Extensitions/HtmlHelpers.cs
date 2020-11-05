using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Extensitions
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Hàm lấy thông tin chính sách trong cấu hình
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string LayChinhSach(this HtmlHelper helper)
        {
            string chinhSach = "";
            CauHinh objCauHinh = DataProvider.Entities.CauHinhs.SingleOrDefault();
            if (objCauHinh != null)
            {
                chinhSach = objCauHinh.ChinhSach;
            }

            return chinhSach;
        }

        public static string LayTenChuDe(this HtmlHelper helper, int chuDeId)
        {
            string tenChuDe = "";
            ChuDe objCag = DataProvider.Entities.ChuDes.Find(chuDeId);
            if (objCag != null)
            {
                tenChuDe = objCag.TenChuDe;
            }
            return tenChuDe;
        }
    }
}