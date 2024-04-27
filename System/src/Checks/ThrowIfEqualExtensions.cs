// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfEqualExtensions
{
	public static bool ThrowIfEqual([NotNull] this int value, int expected)
		=> value.ThrowIfEqual<ArgumentEqualException>(expected, nameof(value));

	public static bool ThrowIfEqual<T>([NotNull] this int value, int expected)
		where T : ArgumentException
		=> value.ThrowIfEqual<T>(expected, nameof(value));

	public static bool ThrowIfEqual<T>([NotNull] this int value, int expected, string paramName)
		where T : ArgumentException
		=> value == expected
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName)
			   : false;
}
