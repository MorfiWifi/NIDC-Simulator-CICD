using Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseData
{
    public class OrganizationModel : BaseModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessage = null, ErrorMessageResourceType = typeof(ResourceFiles.Validation))]
        [Display(Name = nameof(Title), ResourceType = typeof(ResourceFiles.Validation))]
        public string Title { set; get; }
        public string LogoFileName { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessage = null, ErrorMessageResourceType = typeof(ResourceFiles.Validation))]
        [Display(Name = nameof(UniqueUrl), ResourceType = typeof(ResourceFiles.Validation))]
        public string UniqueUrl { set; get; }
        public List<AccountModel> Admins { set; get; } = new List<AccountModel>();
        public UploadedFile Logo{ get; set; }

    }

    public class UploadedFile
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class SaveOrganiztionModel:OrganizationModel
    {
         
    }
}
