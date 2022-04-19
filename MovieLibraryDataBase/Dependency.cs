namespace MovieLibrary
{
    public class Dependency
    {
        public Manager GetManager()
        {
            return new FileManager();
        }

        public InputOutputService GetInputOutputService()
        {
            return new InputOutputService();
        }

        public MediaFormatter GetFormatter()
        {
            return new MediaFormatter();
        }

        public Search getSearch()
        {
            return new Search();
        }

        public DatabaseManager GetDatabaseManager()
        {
            return new DatabaseManager();
        }
    }
}