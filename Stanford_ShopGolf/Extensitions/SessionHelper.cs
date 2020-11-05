using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Extensitions
{
	public class SessionHelper
	{
		private const string SESSION_GIO_HANG_FOR_ADD = "SESSION_GIO_HANG_FOR_ADD";

		public static void SetSessionGioHangForAdd (object obj)
		{
			HttpContext.Current.Session[SESSION_GIO_HANG_FOR_ADD] = obj;
		}
		public static object GetSessionGioHangForAdd()
		{
			return HttpContext.Current.Session[SESSION_GIO_HANG_FOR_ADD];
		}

	}
}