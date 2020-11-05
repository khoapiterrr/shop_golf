using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Areas.Admin.Models
{
    public class MultiSelectDropDownViewModel
    {
        /// <summary>
        /// Gets or sets choose multiple countries property.
        /// </summary>
        [Required]
        [Display(Name = "Choose Multiple Sizes")]
        public List<int> SelectedMultiSizeId { get; set; }

        /// <summary>
        /// Gets or sets selected countries property.
        /// </summary>
        public List<Size> SelectedSizeLst { get; set; }
    }
}