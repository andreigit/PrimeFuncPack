#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PrimeFuncPack.Core.Tests
{
    partial class FailureObsoleteTest
    {
        [Fact]
        public void Map_ExpectIsObsolete()
        {
            const string expectedObsoleteMessage
                = "This method is obsolete. Call MapFailureCode instead.";

            IReadOnlyCollection<MethodInfo> methods = typeof(Failure<SomeFailureCode>)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.Name == nameof(Failure<SomeFailureCode>.Map))
                .ToArray();

            Assert.Equal(1, methods.Count);

            Assert.True(
                methods.All(
                    method => method.CustomAttributes.Any(
                        attr
                        =>
                        attr.AttributeType == typeof(ObsoleteAttribute) &&
                        attr.ConstructorArguments.Count == 2 &&
                        attr.ConstructorArguments[0].ArgumentType == typeof(string) &&
                        attr.ConstructorArguments[0].Value is expectedObsoleteMessage &&
                        attr.ConstructorArguments[1].ArgumentType == typeof(bool) &&
                        attr.ConstructorArguments[1].Value is true)));

            Assert.True(
                methods.All(
                    method => method.CustomAttributes.Any(
                        attr
                        =>
                        attr.AttributeType == typeof(DoesNotReturnAttribute))));
        }
    }
}