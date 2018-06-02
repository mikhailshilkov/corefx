﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using Xunit;

namespace System.Tests
{
    public class FormattableStringTests : RemoteExecutorTestBase
    {
        [Fact]
        public static void CurrentCulture_ImplicityAndExplicitMethodsReturnSameString()
        {
            double d = 123.456;
            string text1 = $"This will be formatted using current culture {d}";
            string text2 = FormattableString.CurrentCulture($"This will be formatted using current culture {d}");
            Assert.Equal(text1, text2);
        }

        [Fact]
        public static void CurrentCulture_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FormattableString.CurrentCulture(null));
        }

        [Fact]
        public static void CurrentCulture_DutchCulture_FormatsDoubleBasedOnCurrentCulture()
        {
            RemoteInvoke(
                () =>
                {
                    var dutchCulture = new CultureInfo("nl");
                    CultureInfo.CurrentCulture = dutchCulture;
                    double d = 123.456;
                    string expected = string.Format(dutchCulture, "Dutch decimal separator is comma {0}", d);
                    string actual = FormattableString.CurrentCulture($"Dutch decimal separator is comma {d}");
                    Assert.Equal(expected, actual);

                    return SuccessExitCode;
                }).Dispose();
        }

        [Fact]
        public static void Invariant_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => FormattableString.Invariant(null));
        }

        [Fact]
        public static void Invariant_DutchCulture_FormatsDoubleBasedOnInvariantCulture()
        {
            RemoteInvoke(
                () =>
                {
                    CultureInfo.CurrentCulture = new CultureInfo("nl"); // would be 123,456 in Dutch
                    double d = 123.456;
                    string expected = string.Format(CultureInfo.InvariantCulture, "Invariant culture is used {0}", d);
                    string actual = FormattableString.Invariant($"Invariant culture is used {d}");
                    Assert.Equal(expected, actual);

                    return SuccessExitCode;
                }).Dispose();
        }
    }
}
