﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

namespace Wangkanai.Collections;

internal sealed class StringArrayComparer : IEqualityComparer<string[]>
{
	public static readonly StringArrayComparer Ordinal           = new StringArrayComparer(StringComparer.Ordinal);
	public static readonly StringArrayComparer OrdinalIgnoreCase = new StringArrayComparer(StringComparer.OrdinalIgnoreCase);

	private readonly StringComparer _valueComparer;

	public StringArrayComparer(StringComparer valueComparer)
	{
		_valueComparer = valueComparer;
	}

	public bool Equals(string[]? x, string[]? y)
	{
		if (ReferenceEquals(x, y))
			return true;

		if (x == null && y == null)
			return true;

		if (x == null || y == null)
			return false;

		if (x.Length != y.Length)
			return false;

		for (var i = 0; i < x.Length; i++)
		{
			if (string.IsNullOrEmpty(x[i]) && string.IsNullOrEmpty(y[i]))
				continue;

			if (!_valueComparer.Equals(x[i], y[i]))
				return false;
		}

		return true;
	}

	public int GetHashCode(string[] obj)
	{
		if (obj == null)
			return 0;

		var hash = new HashCode();
		for (var i = 0; i < obj.Length; i++)
			hash.Add(obj[i] ?? string.Empty, _valueComparer);

		return hash.ToHashCode();
	}
}