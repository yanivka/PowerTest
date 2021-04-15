using System;
using System.Collections.Generic;
using System.Text;

namespace PowerTest
{
    public static class TestReferenceExtentions
    {
        public static ITestReference AsTestReference(this Guid id) => new GuidTestRefenrece(id);
        public static ITestReference AsTestReference<TestClass>() => AsTestReference(typeof(TestClass));
        public static ITestReference AsTestReference(this Type testClassType, params object[] constractorArgs) =>
            testClassType.IsAssignableFrom(typeof(ITestReference)) ? (ITestReference)Activator.CreateInstance(testClassType, constractorArgs) : null;
            
        
    }
}
