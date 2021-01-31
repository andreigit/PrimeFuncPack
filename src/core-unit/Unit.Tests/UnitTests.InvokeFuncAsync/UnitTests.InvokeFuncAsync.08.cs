﻿#nullable enable

using Moq;
using NUnit.Framework;
using PrimeFuncPack.UnitTest;
using PrimeFuncPack.UnitTest.Moq;
using System;
using System.Threading.Tasks;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Core.Tests
{
    partial class UnitTests
    {
        [Test]
        public void InvokeFuncAsync_08_FuncIsNull_ExpectArgumentNullException()
        {
            Func<StructType, RefType, string, int, object, DateTime, StructType?, decimal, Task> funcAsync = null!;

            var arg1 = SomeTextStructType;
            var arg2 = PlusFifteenIdRefType;
            var arg3 = TabString;
            var arg4 = MinusFortyFive;
            var arg5 = new { Value = PlusTwoHundredPointFive };
            var arg6 = Year2015March11H01Min15;
            var arg7 = NullTextStructType;
            var arg8 = MinusSeventyFivePointSeven;

            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => _ = Unit.InvokeFuncAsync(funcAsync, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
            Assert.AreEqual("funcAsync", ex.ParamName);
        }

        [Test]
        public async Task InvokeFuncAsync_08_ExpectCallFuncOnce()
        {
            var mockFuncAsync = MockFuncFactory.CreateMockFunc<StructType, RefType?, string, int, object?, DateTime, StructType?, decimal?, Task>(Task.CompletedTask);

            var arg1 = SomeTextStructType;
            var arg2 = (RefType?)null;
            var arg3 = TabString;
            var arg4 = MinusFortyFive;
            var arg5 = new { Value = PlusTwoHundredPointFive };
            var arg6 = Year2015March11H01Min15;
            var arg7 = (StructType?)null;
            var arg8 = (decimal?)MinusSeventyFivePointSeven;

            var actual = await Unit.InvokeFuncAsync(mockFuncAsync.Object.Invoke, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

            Assert.AreEqual(Unit.Value, actual);
            mockFuncAsync.Verify(a => a.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), Times.Once);
        }
    }
}