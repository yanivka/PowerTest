using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    public class TestExecutionReson
    {
        public readonly Test Test;
        public readonly bool Dependency;
        public readonly Test[] TestExecutionPlan;
        public readonly RuntimeMemory Input;


    }
}
