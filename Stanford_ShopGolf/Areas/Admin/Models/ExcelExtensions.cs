using OfficeOpenXml;
using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
	public static class ExcelExtensions
	{
		/*

		//Hàm chuyển về danh sách card
		public static List<Card> toListCard(this ExcelPackage package)
		{
			List<Card> cards = new List<Card>();
			ExcelWorksheet worksheet = package.Workbook.Worksheets.First();

			//for (var rowNumber = 2; rowNumber < worksheet.Dimension.End.Row; rowNumber++)
			//{
			//	var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
			//	Card card = new Card();

			//	return cards;

			//}
			DataTable dt = toDataTable(package);
			foreach (DataRow item in dt.Rows)
			{
				Card card = new Card();
				card.Id = int.Parse(item[0] + "");
				card.Number = item[1].ToString();
				card.BSX = item[2].ToString();
				card.BsContainer = item[3].ToString();
				card.NameUser = item[4].ToString();
				card.Gate_Id = int.Parse(item[5]+"");
				string Status = (item[6] + "").Trim().ToLower();
				if (Status == "đã đăng ký")
				{
					card.Status = true;
				}
				else if(Status == "chưa đăng ký")
				{
					card.Status = false;
				}

				//thêm vào danh sách mới
				cards.Add(card);
				
			}
			return cards;

		}
		public static DataTable toDataTable(this ExcelPackage package)
		{
			DataTable dt = new DataTable();
			ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
			// Đọc tất cả các header
			foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
			{
				dt.Columns.Add(firstRowCell.Text);
			}
			// Đọc tất cả data bắt đầu từ row thứ 2
			for (var rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
			{
				// Lấy 1 row trong excel để truy vấn
				var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
				// tạo 1 row trong data table
				var newRow = dt.NewRow();
				foreach (var cell in row)
				{
					newRow[cell.Start.Column - 1] = cell.Text;
				}
				dt.Rows.Add(newRow);
			}
			return dt;
		}
        */
	}
}