using CMS.MODEL.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.MODEL.Library
{
    public class LibrarycardModel
    {
        public string Id { get; set; }
    }
    public class LibrarycardEditModel : DropDownListModel
    {
        public string LibraryCardSerialNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
