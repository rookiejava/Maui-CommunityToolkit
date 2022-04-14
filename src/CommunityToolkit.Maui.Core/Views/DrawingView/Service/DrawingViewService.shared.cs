﻿#if !(IOS || ANDROID || MACCATALYST || WINDOWS)
namespace CommunityToolkit.Maui.Core.Views;

/// <summary>
/// Drawing view service
/// </summary>
public static partial class DrawingViewService
{
	/// <summary>
	/// Get image stream from points
	/// </summary>
	/// <param name="points">Drawing points</param>
	/// <param name="imageSize">Image size</param>
	/// <param name="lineWidth">Line Width</param>
	/// <param name="strokeColor">Line color</param>
	/// <param name="backgroundColor">Image background color</param>
	/// <returns>Image stream</returns>
	public static Task<Stream> GetImageStream(IList<Point> points,
		Size imageSize,
		float lineWidth,
		Color strokeColor,
		Color backgroundColor) =>
		Task.FromResult(Stream.Null);

	/// <summary>
	/// Get image stream from lines
	/// </summary>
	/// <param name="lines">Drawing lines</param>
	/// <param name="imageSize">Image size</param>
	/// <param name="backgroundColor">Image background color</param>
	/// <returns>Image stream</returns>
	public static Task<Stream> GetImageStream(IList<DrawingLine> lines,
		Size imageSize,
		Color backgroundColor) =>
		Task.FromResult(Stream.Null);
}
#endif