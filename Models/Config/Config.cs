using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config
{
    public class ConfigModel:BaseModel
    {
        public string Title { set; get; }            
        public FolderModel Folder { set; get; }
        [Required(ErrorMessage ="*")]
        public Guid? FolderId { set; get; }
    }
    
}
