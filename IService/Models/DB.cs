using BsonData;

namespace System
{
    public partial class Document
    {
        public List<string> Add { get => GetArray<List<string>>(nameof(Add)); set => Push(nameof(Add), value); }
        public List<string> Remove { get => GetArray<List<string>>(nameof(Remove)); set => Push(nameof(Remove), value); }
    }
    public partial class DB
    {
        public static BsonData.MainDatabase? MainMonitoring { get; private set; }
        public static BsonData.MainDatabase? MainManager { get; private set; }
        public static void InitMonitoringDatabase(string path)
        {
            MainMonitoring = new BsonData.MainDatabase("MonitoringDatabase");
            MainMonitoring.Connect(path);
            MainMonitoring.StartStorageThread();
        }
        public static void InitManagerDatabase(string path)
        {
            MainManager = new BsonData.MainDatabase("ManagerDatabase");
            MainManager.Connect(path);
            MainManager.StartStorageThread();
        }
    }
    public partial class DB
    {
        public static BsonData.Collection Verification => MainManager.GetCollection(nameof(Verification));
    }
    public partial class DB
    {
        public static BsonData.Collection? _6868 => MainMonitoring.GetCollection(nameof(_6868));
        public static BsonData.Collection? _60001 => MainMonitoring.GetCollection(nameof(_60001));
        public static BsonData.Collection? _2082219501 => MainMonitoring.GetCollection(nameof(_2082219501));
        public static BsonData.Collection? _2084244718 => MainMonitoring.GetCollection(nameof(_2084244718));
        public static BsonData.Collection? _48828 => MainMonitoring.GetCollection(nameof(_48828));
        public static BsonData.Collection? _48834 => MainMonitoring.GetCollection(nameof(_48834));
        public static BsonData.Collection? _48839 => MainMonitoring.GetCollection(nameof(_48839));
        public static BsonData.Collection? _48081 => MainMonitoring.GetCollection(nameof(_48081));
        public static BsonData.Collection? _48073 => MainMonitoring.GetCollection(nameof(_48073));
        public static BsonData.Collection? _48068 => MainMonitoring.GetCollection(nameof(_48068));
        public static BsonData.Collection? _48085 => MainMonitoring.GetCollection(nameof(_48085));
        public static BsonData.Collection? _48855 => MainMonitoring.GetCollection(nameof(_48855));
        public static BsonData.Collection? _48089 => MainMonitoring.GetCollection(nameof(_48089));
        public static BsonData.Collection? _561700 => MainMonitoring.GetCollection(nameof(_561700));
        public static BsonData.Collection? _48889 => MainMonitoring.GetCollection(nameof(_48889));
        public static BsonData.Collection? _48892 => MainMonitoring.GetCollection(nameof(_48892));
        public static BsonData.Collection? _087920 => MainMonitoring.GetCollection(nameof(_087920));
        public static BsonData.Collection? _087870 => MainMonitoring.GetCollection(nameof(_087870));

    }
}
