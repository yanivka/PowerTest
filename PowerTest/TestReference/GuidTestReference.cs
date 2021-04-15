using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    class GuidTestRefenrece : ITestReference
    {
        public readonly Guid Id;
        public GuidTestRefenrece(Guid id)
        {
            this.Id = id;
        }
        public Guid GetId() => this.Id;
    }
}
