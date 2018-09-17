﻿using System;
using System.Collections;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    public class Placeholder { }
    public class TestClassAttribute : NUnit.Framework.TestFixtureAttribute
    {
    }
    public class TestInitializeAttribute : NUnit.Framework.SetUpAttribute
    {
    }
    public class TestFixtureSetUpAttribute : NUnit.Framework.TestFixtureSetUpAttribute
    {
    }
    public class TestMethodAttribute : NUnit.Framework.TestAttribute
    {
    }
    public class TestCleanupAttribute : NUnit.Framework.TearDownAttribute
    {
    }
    public class IgnoreAttribute : NUnit.Framework.IgnoreAttribute
    {
    }
    public class ExpectedExceptionAttribute : NUnit.Framework.ExpectedExceptionAttribute
    {
        public ExpectedExceptionAttribute(Type exceptionType) : this(exceptionType, null)
        {
        }
        public ExpectedExceptionAttribute(Type exceptionType, string message) : base(exceptionType)
        {
            UserMessage = message;
        }
    }
    public class TestContext : NUnit.Framework.TestContext
    {
        public TestContext(IDictionary dictionary) : base(dictionary)
        {
        }
    }
    public class Assert : NUnit.Framework.Assert
    {
    }
    public class CollectionAssert : NUnit.Framework.CollectionAssert
    {
    }
}
