using Stanford.ShopGolf.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanford.ShopGolf.Business
{
    public class DataProvider
    {
        /// <summary>
        /// Khai báo thuộc tính làm việc với db bằng EF
        /// </summary>
        private static ShopGolfEntities _Entities = null;
        public static ShopGolfEntities Entities
        {
            get
            {
                if(_Entities == null)
                {
                    _Entities = new ShopGolfEntities();
                }
                return _Entities;
            }

            set
            {
                _Entities = value;
            }
        }
    }
}
