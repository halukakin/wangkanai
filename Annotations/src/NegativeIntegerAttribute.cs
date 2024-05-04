// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[AttributeUsage( AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class NegativeIntegerAttribute : Attribute
{
	public NegativeIntegerAttribute() { }
}
