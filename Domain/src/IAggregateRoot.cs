// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain;

public interface IAggregateRoot : IEntity
{
}

public interface IAggregateRoot<T> : IEntity<T>
{
}