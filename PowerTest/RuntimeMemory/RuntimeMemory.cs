using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    public class RuntimeMemory
    {
        private IDictionary<string, object> memory;

        public RuntimeMemory()
        {
            this.memory = new Dictionary<string, object>();
        }

        public void Set(string key, object value) => this.memory[key] = value;
        public bool Get(string key, out object value) =>  this.memory.TryGetValue(key, out value);
    }
}
