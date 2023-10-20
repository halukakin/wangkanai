﻿// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.Internal;

public class TypeNameHelperTests
{
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName();
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true, true, '+');
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter_WithNestedTypeDelimiter()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true, true, '+');
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}
}

public class DisplayNumeric;