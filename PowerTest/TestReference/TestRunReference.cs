using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    public class TestRunReference
    {
        public readonly ITestReference TestReference;
        public readonly bool Direct;
        public readonly RuntimeMemory Input;
        public readonly bool Reuseable;

        public TestRunReference(ITestReference testReference, bool direct, bool reuseable)
        {
            this.TestReference = testReference;
            this.Direct = direct;
            this.Reuseable = reuseable;
        }
    }
}
