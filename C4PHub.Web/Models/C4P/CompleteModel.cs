using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace C4PHub.Web.Models.C4P
{
    public class CompleteModel
    {
        public bool IsUserAuthenticated { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Url is required")]
        [DataType(DataType.Url)]
        [Display(Name = "C4p Url")]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Name is required")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Location is required")]
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Date is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "C4P Expired Date is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "C4P Expired Date")]
        public DateTime? ExpiredDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Published is required")]
        [Display(Name = "User")]
        public string UserPublished { get; set; }

    }
}
