﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.RenderTree;

public readonly struct RenderBatch
{
	/// <summary>
	///     Gets the changes to components that were added or updated.
	/// </summary>
	public ArrayRange<RenderTreeDiff> UpdatedComponents { get; }

	/// <summary>
	///     Gets render frames that may be referenced by entries in <see cref="UpdatedComponents" />.
	///     For example, edit entries of type <see cref="RenderTreeEditType.PrependFrame" />
	///     will point to an entry in this array to specify the subtree to be prepended.
	/// </summary>
	public ArrayRange<RenderTreeFrame> ReferenceFrames { get; }

	/// <summary>
	///     Gets the IDs of the components that were disposed.
	/// </summary>
	public ArrayRange<int> DisposedComponentIDs { get; }

	/// <summary>
	///     Gets the IDs of the event handlers that were disposed.
	/// </summary>
	public ArrayRange<ulong> DisposedEventHandlerIDs { get; }

	internal RenderBatch(
		ArrayRange<RenderTreeDiff>  updatedComponents,
		ArrayRange<RenderTreeFrame> referenceFrames,
		ArrayRange<int>             disposedComponentIDs,
		ArrayRange<ulong>           disposedEventHandlerIDs)
	{
		UpdatedComponents       = updatedComponents;
		ReferenceFrames         = referenceFrames;
		DisposedComponentIDs    = disposedComponentIDs;
		DisposedEventHandlerIDs = disposedEventHandlerIDs;
	}
}