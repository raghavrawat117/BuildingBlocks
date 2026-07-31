namespace RedisConsoleAppv1
{
    public static class MyEnv_Dev
    {
        // Don't use this class.
        // Create a class MyEnv (it's added to .gitignore, so credentials will not get checked into the repo)
        // Update the values there
        // Keep it static so we can use without object creation
        public static class Redis
        {
            public static string host = "redis-***************.cloud.redislabs.com";
            public static int port = 000000;
            public static string user = "d*****t";
            public static string password = "a**********Q";
        }


        public static class FileService
        {
            // Copy the fulll path of the folder and add slash at the end
            public static string pathToDataFolder = @"C:\*************\RedisConsoleAppv1\data\";
        }
    }
}