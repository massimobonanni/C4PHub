using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace C4PHub.Web.Models.C4P
{
    public class AddModel
    {
        public bool IsUserAuthenticated { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Url is required")]
        [DataType(DataType.Url)]
        [Display(Name = "C4P Url")]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Published is required")]
        [Display(Name = "User")]
        public string UserPublished { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Add automatically if complete")]
        public bool AddIfComplete { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Overwrite the Call for Paper if exists")]
        public bool OverwriteIfExists { get; set; }
    }
}
