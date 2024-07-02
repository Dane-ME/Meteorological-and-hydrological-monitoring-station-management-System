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
    public partial class Document
    {
        public string Content { get => GetString(nameof(Content)); set => Push(nameof(Content), value); }
        public string Time { get => GetString(nameof(Time)); set => Push(nameof(Time), value); }
        public List<string> Add { get => GetArray<List<string>>(nameof(Add)); set => Push(nameof(Add), value); }
        public List<string> Remove { get => GetArray<List<string>>(nameof(Remove)); set => Push(nameof(Remove), value); }

    }
}
