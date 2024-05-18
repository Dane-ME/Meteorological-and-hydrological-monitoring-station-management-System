using System;
using System.Security.Cryptography;
using System.Text;

namespace MyNamespace
{
    class Text
    {
        public string? Value { get; set; }
        public void getValue()
        {
            Value = "123";
        }
    }
    class Number
    {
        private readonly Text _text;
        public Number(Text text) { _text = text; }
        public int Value { get; set; }
        public void getValue() 
        {
            _text.getValue();
            this.Value = _text.Value.Length;
        }
    }
    class Core
    {
        private readonly Number _number;
        public Core(Number number) { _number = number; }
        public int Test() { _number.getValue(); return _number.Value; }
    }
    class MainDB
    {
        private static Core _core;
        public MainDB(Core core) { _core = core;}
        static void Main(string[] args)
        {
            Create();
            Write();
            Create2();
            Write();
        }
        public static void Create()
        {
            Text text = new Text();
            Number number = new Number(text);
            Core core = new Core(number);
            MainDB mainDB = new MainDB(core);
        }
        public static void Create2()
        {
            Text text = new Text();
            Number number = new Number(text);
            Core core = new Core(number);
            MainDB mainDB = new MainDB(core);
        }
        public static void Write()
        {
            Console.WriteLine(_core.Test());
        }
        
    }
}
