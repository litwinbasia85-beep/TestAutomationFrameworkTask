using Microsoft.Extensions.Configuration;

namespace CoreLayer
{
    public class Configuration
    {
        public static string? BrowserType { get; private set; }
        public static string? AppUrl { get; private set; }
        public static string? ErrorLogin { get; private set; }
        public static string? ErrorPassword { get; private set; }

        public static string[][] AllUsersJson = [];
        public static IConfigurationRoot? ConfigBuilder { get; private set; }

        static Configuration() => Init();

        public static void Init()
        {

            ConfigBuilder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            BrowserType = ConfigBuilder["BrowserType"];
            AppUrl = ConfigBuilder["ApplicationUrl"];
            ErrorLogin = ConfigBuilder["ErrorLogin"];
            ErrorPassword = ConfigBuilder["ErrorPassword"];

            var ValuesSection = ConfigBuilder.GetSection("AllUsers");
            var size2 = ConfigBuilder.GetSection("AllUsers").GetChildren().AsEnumerable().Count();
            AllUsersJson = new string[size2][];
            for (int i = 0; i < size2; i++)
            {
                AllUsersJson[i] = new string[] { };
            }
            int m = 0;
            foreach (IConfigurationSection section in ValuesSection.GetChildren())
            {
                var JsonString = section.GetChildren().Select(x => x.Value).ToArray();
                AllUsersJson[m] = new string[JsonString.Length];
                if (JsonString != null)
                {
                    for (int j1 = 0; j1 < JsonString.Length; j1++)
                    {                        
                        AllUsersJson[m][j1] = JsonString[j1] ?? throw new ArgumentNullException("JSon string can't be null");
                    }
                }
                m++;
            }
        }
    }
}
