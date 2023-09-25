﻿// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentEmptyException : ArgumentException
{
	private ArgumentEmptyException(SerializationInfo info, StreamingContext context) { }
	public ArgumentEmptyException() : base(SystemResources.ArgumentEmptyGeneric) { }
	public ArgumentEmptyException(string paramName) : base(SystemResources.ArgumentEmptyGeneric, paramName) { }
	public ArgumentEmptyException(string message,   Exception innerException) : base(message, innerException) { }
	public ArgumentEmptyException(string paramName, string    message) : base(message, paramName) { }
}