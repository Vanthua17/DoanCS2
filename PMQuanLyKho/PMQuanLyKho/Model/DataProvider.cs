using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyKho.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins { 
            get {
                if (_ins == null)
                    _ins = new DataProvider();
                return _ins; 
            } 
            set {
                _ins = value; 
            } 
        }

        public PMQuanLyKhoEntities DB { get; set; }

        private DataProvider() { 
            DB = new PMQuanLyKhoEntities();
        }

    }
}
