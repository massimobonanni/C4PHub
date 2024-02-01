using C4PHub.Core.Entities;
using C4PHub.Core.Utilities;

namespace C4PHub.Web.Models.Home
{
    public class ActiveC4PModel
    {
        public void FillModel(string timeZone, IEnumerable<C4PInfo> c4pList)
        {
            var timespan = StringUtility.ConvertUtcStringToTimeSpan(timeZone);

            var list = new List<C4PData>();
            foreach (var c4p in c4pList)
            {
                var c4pData = new C4PData();
                c4pData.Id = c4p.Id;
                c4pData.Url = c4p.Url;
                c4pData.EventName = c4p.EventName;
                c4pData.EventLocation = c4p.EventLocation;
                c4pData.EventDate = c4p.EventDate.UtcDateTime.Add(timespan);
                c4pData.EventEndDate = c4p.EventEndDate.UtcDateTime.Add(timespan);
                c4pData.ExpiredDate = c4p.ExpiredDate.UtcDateTime.Add(timespan);
                c4pData.DayToExpiration = (int)(c4pData.ExpiredDate - DateTime.UtcNow.Add(timespan)).TotalDays;
                list.Add(c4pData);
            }
            C4PList = list;
        }

        public IEnumerable<C4PData> C4PList { get; internal set; }

        public class C4PData
        {
            public string Id { get; set; }
            public string Url { get; set; }
            public string EventName { get; set; }
            public string EventLocation { get; set; }

            public DateTime EventDate { get; set; }
            public DateTime EventEndDate { get; set; }
            public DateTime ExpiredDate { get; set; }

            public int DayToExpiration { get; set; }
        }

    }
}
