using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    public class RuntimeMemoryOutputs
    {
        private IDictionary<Guid, RuntimeMemory> memory;

        public RuntimeMemoryOutputs()
        {
            this.memory = new Dictionary<Guid, RuntimeMemory>();
        }
        private RuntimeMemory GetRuntimeMemory(Guid id) => this.memory.TryGetValue(id, out RuntimeMemory memory) ? memory : null;
        public bool IsIdExist(Guid id) => this.GetRuntimeMemory(id) != null;
        public void Set(Guid id, string key, object value)
        {
            RuntimeMemory memory = this.GetRuntimeMemory(id);
            if (memory == null) throw new ArgumentException($"The given id {{{id}}} was not found", "id");
            memory.Set(key, value);
        }
        public bool Get(Guid id, string key, out object value)
        {
            RuntimeMemory memory = this.GetRuntimeMemory(id);
            if (memory == null) throw new ArgumentException($"The given id {{{id}}} was not found", "id");
            return memory.Get(key, out value);
        }
    }
}
