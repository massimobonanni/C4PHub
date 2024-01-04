using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace C4PHub.Web.Models.C4P
{
    public class AddModel
    {
        public bool IsUserAuthenticated { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Url is required")]
        [RegularExpression(@$"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$", ErrorMessage = "Url is not a valid url")]
        [Display(Name ="C4P Url")]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Published is required")]
        [Display(Name = "User")]
        public string UserPublished { get; set; }
    }
}
