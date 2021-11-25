using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pagination.Models
{
    public class PaginateModel
    {
        public decimal Total { get; set; }  // toplam veri sayisi
        public int ActivePage { get; set; }
        public int TotalCeilNumber
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((Total / NumberOfObjectsPerPage)));
            }
        } // Total / TotalPageSizeCount ceil ile yukari yuvarliyorsun ORN; 11/10 => 1.... (Ceil) => 2
        public List<int> List { get; set; }
        public decimal NumberOfObjectsPerPage { get; set; } = 10;  //kac adet veri olacak

        public PaginateModelClick PaginateModelClickEvent { get; set; }
    }

    public class PaginateModelClick
    {
        public bool IsJavaScript { get; set; }
        public string Url { get; set; }
        public string PaginateClickName { get; set; }
    }
}
