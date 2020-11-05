using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanford.ShopGolf.Business.Entity;

namespace Stanford.ShopGolf.Business
{
    public class Service
    {
        /// <summary>
        /// Hàm lấy danh sách chủ đề theo mã chủ đề cha
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="IsDESC">Sắp xếp giảm dần</param>
        /// <returns></returns>
        public static List<ChuDe> GetListChuDeTheoParentId(int ParentId, bool IsDESC)
        {
            List<ChuDe> lstChuDe = DataProvider.Entities.ChuDes.Where(c => c.MaCha == ParentId).ToList();

            if(IsDESC)
            {
                lstChuDe = lstChuDe.OrderByDescending(c => c.TenChuDe).ToList();
            }
            else
            {
                lstChuDe = lstChuDe.OrderBy(c => c.TenChuDe).ToList();
            }
            return lstChuDe;
        }

        /// <summary>
        /// Lấy danh sách chủ đề
        /// </summary>
        /// <param name="Sep"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public static List<ChuDe> GetListParent(string Sep, int ParentId)
        {
            List<ChuDe> ret = new List<ChuDe>();

            List<ChuDe> list = GetListChuDeTheoParentId(ParentId, true);
            foreach (ChuDe o in list)
            {
                ChuDe Item = new ChuDe();
                Item.TenChuDe = Sep + o.TenChuDe.TrimEnd();
                Item.Id = o.Id;
                Item.MaCha = o.MaCha;

                ret.Add(Item);
                ret.AddRange(GetListParent("..." + Sep, o.Id));
            }
            return ret;
        }

		public static List<Menu> GetListMenuTheoParentId(int ParentId, bool IsDESC)
		{
			List<Menu> lstChuDe = DataProvider.Entities.Menus.Where(c => c.ParentId == ParentId).ToList();

			if (IsDESC)
			{
				lstChuDe = lstChuDe.OrderByDescending(c => c.TenItem).ToList();
			}
			else
			{
				lstChuDe = lstChuDe.OrderBy(c => c.TenItem).ToList();
			}
			return lstChuDe;
		}

		public static List<Menu> GetListParentMenu(string Sep, int ParentId)
		{
			List<Menu> ret = new List<Menu>();

			List<Menu> list = GetListMenuTheoParentId(ParentId, true);
			foreach (Menu o in list)
			{
				Menu Item = new Menu();
				Item.TenItem = Sep + o.TenItem.TrimEnd();
				Item.Id = o.Id;
				Item.ParentId = o.ParentId;

				ret.Add(Item);
				ret.AddRange(GetListParentMenu("..." + Sep, o.Id));
			}
			return ret;
		}
	}
}
