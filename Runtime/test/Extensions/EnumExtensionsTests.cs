// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions;

[Flags]
public enum EnumFlag
{
    Thailand  = 0,
    Japan     = 1 << 0,
    Singapore = 1 << 1,
    Australia = 1 << 2
}

public class EnumExtensionsTests
{
    [Fact]
    public void GetFlag()
    {
        var one   = EnumFlag.Thailand;
        var two   = EnumFlag.Thailand | EnumFlag.Japan;
        var three = EnumFlag.Thailand | EnumFlag.Japan | EnumFlag.Singapore;
        var four  = EnumFlag.Thailand | EnumFlag.Japan | EnumFlag.Singapore | EnumFlag.Australia;
        Assert.Single(one.GetFlags());
        Assert.Equal(2, two.GetFlags().Count());
        Assert.Equal(3, three.GetFlags().Count());
        Assert.Equal(4, four.GetFlags().Count());
    }

    [Fact]
    public void ToStringInvariant()
    {
        var one = EnumFlag.Thailand;
        Assert.Equal("thailand", one.ToStringInvariant());
    }

    [Fact]
    public void ToStringInvariantFlag()
    {
        var flags = EnumFlag.Singapore;
        Assert.Equal("singapore", flags.ToStringInvariant());
    }
}