namespace C4PHub.Core.Entities
{
    public static class C4PInfoExtensions
    {
        public static int DayToExpiration(this C4PInfo c4p)
        {
            var now = DateTime.Now.Date;
            var diff = c4p.ExpiredDate - now;
            return diff.Days;
        }
    }
}
