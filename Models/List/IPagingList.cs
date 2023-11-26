using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.List
{
    public interface IPagingList<T>
    {
        public IList<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; }
    }
}
