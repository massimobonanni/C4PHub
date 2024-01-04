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
        [RegularExpression(@$"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$", ErrorMessage = "Url is not a valid url")]
        [Display(Name = "C4p Url")]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Name is required")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Location is required")]
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "C4P Expired Date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "C4P Expired Date")]
        public DateTime? ExpiredDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Published is required")]
        [Display(Name = "User")]
        public string UserPublished { get; set; }

        //public bool IsValid(ModelStateDictionary modelState)
        //{
        //    var retval = true;
        //    if (string.IsNullOrWhiteSpace(Url))
        //    {
        //        modelState.AddModelError(nameof(Url), "Url is required");
        //        retval = false;
        //    }
        //    string pattern = @"^(https?)://[^\s/$.?#].[^\s]*$";
        //    if (Regex.IsMatch(Url, pattern))
        //    {
        //        modelState.AddModelError(nameof(Url), "Url is not a valid url");
        //        retval = false;
        //    }
        //    if (string.IsNullOrWhiteSpace(EventName))
        //    {
        //        modelState.AddModelError(nameof(EventName), "Event Name is required");
        //        retval = false;
        //    }
        //    if (string.IsNullOrWhiteSpace(EventLocation))
        //    {
        //        modelState.AddModelError(nameof(EventLocation), "Event Location is required");
        //        retval = false;
        //    }
        //    if (!EventDate.HasValue)
        //    {
        //        modelState.AddModelError(nameof(EventDate), "Event Date is required");
        //        retval = false;
        //    }
        //    if (!ExpiredDate.HasValue)
        //    {
        //        modelState.AddModelError(nameof(ExpiredDate), "Expired Date is required");
        //        retval = false;
        //    }
        //    if (string.IsNullOrWhiteSpace(UserPublished))
        //    {
        //        modelState.AddModelError(nameof(UserPublished), "User Published is required");
        //        retval = false;
        //    }
        //    return retval;
        //}

    }
}
