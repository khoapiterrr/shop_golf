using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.ViewModels
{
    public class SlideShowViewModel
    {
        public SlideShowViewModel()
        {

        }

        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Url { get; set; }

    }
}