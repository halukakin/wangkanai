﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown;

public interface IMarkdownPageFactoryProvider
{
	MarkdownPageFactoryResult CreateFactory(string relativePath);
}