using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResponseModel:BaseModel
    {
        public string Message { set; get; }
        public bool Success { set; get; }
    }

    public class BaseModel
    {
        public Guid? Id { set; get; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
