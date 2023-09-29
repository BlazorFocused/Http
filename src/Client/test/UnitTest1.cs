// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client.Test;

public class UnitTest1
{
    [Fact]
    public void SampleMethod_ShouldCallMethod()
    {
        var underTest = new Class1();

        underTest.SampleMethod();

        Assert.NotNull(underTest);
        Assert.True(underTest.ClassWasCalled());
    }

    [Fact]
    public void SampleMethod_ShouldCallInternalMethod()
    {
        var underTest = new Class1();

        // Call internal method exposed only to test class
        underTest.SampleInternalMethod();

        Assert.NotNull(underTest);
        Assert.True(underTest.ClassWasCalled());
    }
}
