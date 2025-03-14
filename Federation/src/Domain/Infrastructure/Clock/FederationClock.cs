// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

public class FederationClock : IClock
{
	public DateTimeOffset UtcNow => _timeProvider.GetUtcNow();

	private readonly TimeProvider _timeProvider;

	public FederationClock()
		=> _timeProvider = TimeProvider.System;

	public FederationClock(TimeProvider timeProvider)
		=> _timeProvider = timeProvider;
}