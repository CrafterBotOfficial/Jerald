using BepInEx.Configuration;

namespace Example
{
    public static class Configuration
    {
        public static ConfigEntry<string> PersistantNote;

        static Configuration()
        {
            var config = Main.Config;
            PersistantNote = config.Bind("misc", "note", "");
        }
    }
}
