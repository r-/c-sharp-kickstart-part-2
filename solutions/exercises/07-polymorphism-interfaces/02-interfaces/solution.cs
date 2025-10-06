using System;
using System.Collections.Generic;

namespace Part2.Ch07.Ex02.Solution
{
    interface IDataStore
    {
        string StorageName { get; }
        int ItemCount { get; }
        
        void Save(string key, string value);
        string Load(string key);
        bool Delete(string key);
        void Clear();
    }
    
    class MemoryStore : IDataStore
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        
        public string StorageName => "Memory Storage";
        public int ItemCount => data.Count;
        
        public void Save(string key, string value)
        {
            data[key] = value;
            Console.WriteLine($"Saved: {key} = {value}");
        }
        
        public string Load(string key)
        {
            if (data.ContainsKey(key))
                return data[key];
            return null;
        }
        
        public bool Delete(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
                Console.WriteLine($"Deleted {key}");
                return true;
            }
            return false;
        }
        
        public void Clear()
        {
            data.Clear();
            Console.WriteLine("Memory cleared");
        }
    }
    
    class FileStore : IDataStore
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        
        public string StorageName => "File Storage";
        public int ItemCount => data.Count;
        
        public void Save(string key, string value)
        {
            data[key] = value;
            Console.WriteLine($"Writing to file: {key} = {value}");
        }
        
        public string Load(string key)
        {
            if (data.ContainsKey(key))
            {
                Console.WriteLine($"Reading from file: {key}");
                return data[key];
            }
            return null;
        }
        
        public bool Delete(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
                Console.WriteLine($"Deleting file: {key}");
                return true;
            }
            return false;
        }
        
        public void Clear()
        {
            data.Clear();
            Console.WriteLine("All files deleted");
        }
    }
    
    class DatabaseStore : IDataStore
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        
        public string StorageName => "Database Storage";
        public int ItemCount => data.Count;
        
        public void Save(string key, string value)
        {
            data[key] = value;
            Console.WriteLine($"Executing INSERT: {key} = {value}");
        }
        
        public string Load(string key)
        {
            if (data.ContainsKey(key))
            {
                Console.WriteLine($"Executing SELECT: {key}");
                return data[key];
            }
            return null;
        }
        
        public bool Delete(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
                Console.WriteLine($"Executing DELETE: {key}");
                return true;
            }
            return false;
        }
        
        public void Clear()
        {
            data.Clear();
            Console.WriteLine("Database truncated");
        }
    }
    
    class Program
    {
        static void TestStorage(IDataStore storage)
        {
            Console.WriteLine($"\nTesting: {storage.StorageName}");
            
            storage.Save("user1", "Alice");
            storage.Save("user2", "Bob");
            storage.Save("user3", "Carol");
            
            string value = storage.Load("user1");
            Console.WriteLine($"Loaded user1: {value}");
            
            Console.WriteLine($"Item count: {storage.ItemCount}");
            
            storage.Delete("user2");
            Console.WriteLine($"Item count after delete: {storage.ItemCount}");
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Storage Systems");
            Console.WriteLine("=======================");
            
            IDataStore memory = new MemoryStore();
            IDataStore file = new FileStore();
            IDataStore database = new DatabaseStore();
            
            TestStorage(memory);
            TestStorage(file);
            TestStorage(database);
        }
    }
}