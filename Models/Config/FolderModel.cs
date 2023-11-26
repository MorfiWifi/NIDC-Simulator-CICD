using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FolderModel:BaseModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessage = null, ErrorMessageResourceType = typeof(ResourceFiles.Validation))]
        [Display(Name = nameof(Title), ResourceType = typeof(ResourceFiles.Validation))]
        public string Title { set; get; }        
        [Display(Name = nameof(IsPublic), ResourceType = typeof(ResourceFiles.Validation))]
        public bool IsPublic { set; get; } = false;
        public override string ToString()
        {
            return Title;
        }
    }
}
