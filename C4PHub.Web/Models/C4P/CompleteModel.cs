using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using C4PHub.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace C4PHub.Web.Models.C4P
{
    public class CompleteModel
    {
        public bool IsUserAuthenticated { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Url is required")]
        [DataType(DataType.Url)]
        [Display(Name = "C4p Url")]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Name is required")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Event Location is required")]
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Start Event Date is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Start Event Date")]
        public DateTime? EventDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "End Event Date is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "End Event Date")]
        public DateTime? EventEndDate { get; set; }

        [Display(Name = "Event Timezone")]
        public List<SelectListItem>? TimeZones { get; private set; }

        public string SelectedTimeZone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "C4P Expired Date is required")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "C4P Expired Date")]
        public DateTime? ExpiredDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Published is required")]
        [Display(Name = "User")]
        public string UserPublished { get; set; }

        public void Fill(C4PInfo c4p)
        {
            EventName = c4p.EventName;
            EventLocation = c4p.EventLocation;
            EventDate = c4p.EventDate != default ? c4p.EventDate.Date : DateTime.Now;
            EventEndDate = c4p.EventEndDate != default ? c4p.EventEndDate.Date : DateTime.Now;
            ExpiredDate = c4p.ExpiredDate != default ? c4p.ExpiredDate.Date : DateTime.Now;
            TimeZones = TimeZone.GetTimeZones().Select(tz=> new SelectListItem(tz.DisplayName,tz.Offset)).ToList();
            if (c4p.EventDate != default)
            {
                var startDateOffsetStr= string.Format("UTC{0}{1:D2}:00", c4p.EventDate.Offset.Hours < 0 ? "-" : "+", Math.Abs(c4p.EventDate.Offset.Hours));
                var timeZone = TimeZones.FirstOrDefault(tz => tz.Value == startDateOffsetStr);
                if (timeZone != null)
                    SelectedTimeZone=timeZone.Value;
            }
            else
            {
                var utcZone = TimeZones.FirstOrDefault(tz => tz.Text == "UTC+00:00");
                if (utcZone != null)
                    SelectedTimeZone=utcZone.Value;
            }
        }
    }

    public class TimeZone
    {
        public static IEnumerable<TimeZone> GetTimeZones()
        {
            var timeZones = new List<TimeZone>();
            for (int i = -12; i <= 13; i++)
            {
                string utcString = string.Format("UTC{0}{1:D2}:00", i < 0 ? "-" : "+", Math.Abs(i));
                timeZones.Add(new TimeZone() { DisplayName = utcString, Offset = utcString });
            }
            return timeZones;
        }
        public string DisplayName { get; set; }

        public string Offset { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
