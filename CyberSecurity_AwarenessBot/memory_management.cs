using System.Collections.Generic;
using System.IO;
using System;

namespace CyberSecurity_AwarenessBot
{


    public class memory_management
    {
        private readonly List<string> memoryLog = new();

        public void AddToMemory(string entry)
        {
            memoryLog.Add($"{DateTime.Now:HH:mm} - {entry}");
        }

        public List<string> GetMemoryLog()
        {
            return new List<string>(memoryLog);
        }

        public void ClearMemory()
        {
            memoryLog.Clear();
        }
    }
}