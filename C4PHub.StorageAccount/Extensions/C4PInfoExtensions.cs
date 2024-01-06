using C4PHub.StorageAccount.Utilities;

namespace C4PHub.Core.Entities
{
    internal static class C4PInfoExtensions
    {
        public static string GeneratePartitionKey(this C4PInfo c4p)
        {
            return c4p.EventDate.Year.ToString();
        }
    }
}
