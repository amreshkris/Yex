using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEXEntities;

namespace YEXDataProvider
{
    public class YEXDBContext : DbContext
    {        
        public YEXDBContext() : base("name=YEXDBContext")
        {

        }
        public IDbSet<StockDetail> stocks { get; set; }
        public IDbSet<OrderDetail> orderHistory { get; set; }
    }

    
}
