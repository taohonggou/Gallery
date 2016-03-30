using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using DataTrsfer;
using Tool;

namespace Server
{
    public class LocationServer:BaseService<Location>
    {
        public bool Add(LocationDt l)
        {
             base.Add(TransferObject.ConvertObjectByEntity<LocationDt, Location>(l));
             return Save() > 0;
        }
        public bool Update(LocationDt l)
        {
            base.Update(TransferObject.ConvertObjectByEntity<LocationDt, Location>(l));
            return Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }
    }
}
