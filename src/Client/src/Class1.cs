// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

/// <summary>
/// Test Class
/// </summary>
public class Class1
{
    private bool wasCalled;

    /// <summary>
    /// Sample Method
    /// </summary>
    public void SampleMethod() => wasCalled = true;

    /// <summary>
    /// Internal Method call test "InternalsVisibleTo with key signing enabled
    /// </summary>
    /// <returns>If class was called</returns>
    internal bool SampleInternalMethod() => wasCalled = true;

    /// <summary>
    /// Class Was Called
    /// </summary>
    /// <returns>If class was called </returns>
    public bool ClassWasCalled() => wasCalled;
}
