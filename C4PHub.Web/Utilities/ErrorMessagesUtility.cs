namespace C4PHub.Web.Utilities
{
    public static class ErrorMessagesUtility
    {
        private static IEnumerable<string> _errorMessages = new List<string>()
        {
            "Oops! It seems like our code got shuffled. Let’s try that again!",
            "Oops! Looks like our code went on a coffee break. Let’s give it another shot!",
            "Seems like our code took a detour. Let’s reroute them back!"
        };

        public static string GetErrorMessage()
        {
            var rndIndex= new Random().Next(0, _errorMessages.Count()-1);
            return _errorMessages.ElementAt(rndIndex);
        }
    }
}
