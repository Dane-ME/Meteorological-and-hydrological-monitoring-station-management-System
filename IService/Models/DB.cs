namespace System
{
    public partial class DB
    {
        public static BsonData.MainDatabase? Main { get; private set; }
        public static void Start(string path)
        {
            Main = new BsonData.MainDatabase("MainDB");
            Main.Connect(path);
            Main.StartStorageThread();
        }
    }
}
