﻿using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Views;

namespace CommunityToolkit.Maui.Core;

/// <summary>
/// The DrawingView allows you to draw one or multiple lines on a canvas
/// </summary>
public interface IDrawingView : IView
{
	/// <summary>
	/// Event occurred when drawing line completed
	/// </summary>
	/// <param name="lastDrawingLine">Last drawing line</param>
	void DrawingLineCompleted(DrawingLine lastDrawingLine);

	/// <summary>
	/// The <see cref="Color"/> that is used by default to draw a line on the <see cref="IDrawingView"/>.
	/// </summary>
	Color LineColor { get; }

	/// <summary>
	/// The width that is used by default to draw a line on the <see cref="IDrawingView"/>.
	/// </summary>
	float LineWidth { get; }

	/// <summary>
	/// The collection of lines that are currently on the <see cref="IDrawingView"/>.
	/// </summary>
	ObservableCollection<DrawingLine> Lines { get; }

	/// <summary>
	/// Toggles multi-line mode. When true, multiple lines can be drawn on the <see cref="IDrawingView"/> while the tap/click is released in-between lines.
	/// Note: when <see cref="ClearOnFinish"/> is also enabled, the lines are cleared after the tap/click is released.
	/// Additionally, <see cref="DrawingLineCompleted"/> will be fired after each line that is drawn.
	/// </summary>
	bool MultiLineMode { get; }

	/// <summary>
	/// Indicates whether the <see cref="IDrawingView"/> is cleared after releasing the tap/click and a line is drawn.
	/// Note: when <see cref="MultiLineMode"/> is also enabled, this might cause unexpected behavior.
	/// </summary>
	bool ClearOnFinish { get; }

	/// <summary>
	/// Retrieves a <see cref="Stream"/> containing an image of the <see cref="Lines"/> that are currently drawn on the <see cref="IDrawingView"/>.
	/// </summary>
	/// <param name="imageSizeWidth">Desired width of the image that is returned.</param>
	/// <param name="imageSizeHeight">Desired height of the image that is returned.</param>
	/// <returns><see cref="Task{Stream}"/> containing the data of the requested image with data that's currently on the <see cref="IDrawingView"/>.</returns>
	Task<Stream> GetImageStream(double imageSizeWidth, double imageSizeHeight);
}