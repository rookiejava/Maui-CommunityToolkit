using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace CommunityToolkit.Maui.Core.Views;

public partial class DrawingViewHandler : ViewHandler<IDrawingView, MauiDrawingView>
{
	/// <summary>
	/// Action that's triggered when the DrawingView <see cref="IDrawingView.Lines"/> property changes.
	/// </summary>
	/// <param name="handler">An instance of <see cref="DrawingViewHandler"/>.</param>
	/// <param name="view">An instance of <see cref="IDrawingView"/>.</param>
	public static void MapLines(DrawingViewHandler handler, IDrawingView view)
	{
		UpdateLines(view, handler.PlatformView);
	}

	/// <summary>
	/// Action that's triggered when the DrawingView <see cref="IDrawingView.ClearOnFinish"/> property changes.
	/// </summary>
	/// <param name="handler">An instance of <see cref="DrawingViewHandler"/>.</param>
	/// <param name="view">An instance of <see cref="IDrawingView"/>.</param>
	public static void MapClearOnFinish(DrawingViewHandler handler, IDrawingView view)
	{
		handler.PlatformView.SetClearOnFinish(view.ClearOnFinish);
	}

	/// <summary>
	/// Action that's triggered when the DrawingView <see cref="IDrawingView.LineColor"/> property changes.
	/// </summary>
	/// <param name="handler">An instance of <see cref="DrawingViewHandler"/>.</param>
	/// <param name="view">An instance of <see cref="IDrawingView"/>.</param>
	public static void MapLineColor(DrawingViewHandler handler, IDrawingView view)
	{
		handler.PlatformView.SetLineColor(view.LineColor);
	}

	/// <summary>
	/// Action that's triggered when the DrawingView <see cref="IDrawingView.LineWidth"/> property changes.
	/// </summary>
	/// <param name="handler">An instance of <see cref="DrawingViewHandler"/>.</param>
	/// <param name="view">An instance of <see cref="IDrawingView"/>.</param>
	public static void MapLineWidth(DrawingViewHandler handler, IDrawingView view)
	{
		handler.PlatformView.SetLineWidth(view.LineWidth);
	}

	/// <summary>
	/// Action that's triggered when the DrawingView <see cref="IDrawingView.MultiLineMode"/> property changes.
	/// </summary>
	/// <param name="handler">An instance of <see cref="DrawingViewHandler"/>.</param>
	/// <param name="view">An instance of <see cref="IDrawingView"/>.</param>
	public static void MapMultiLineMode(DrawingViewHandler handler, IDrawingView view)
	{
		handler.PlatformView.SetMultiLineMode(view.MultiLineMode);
	}

	/// <inheritdoc />
	protected override void ConnectHandler(MauiDrawingView nativeView)
	{
		base.ConnectHandler(nativeView);
		nativeView.Initialize();
		VirtualView.Lines.CollectionChanged += OnLinesCollectionChanged;
		PlatformView.DrawingLineCompleted += OnPlatformViewDrawingLineCompleted;
	}

	/// <inheritdoc />
	protected override void DisconnectHandler(MauiDrawingView nativeView)
	{
		PlatformView.DrawingLineCompleted -= OnPlatformViewDrawingLineCompleted;
		VirtualView.Lines.CollectionChanged -= OnLinesCollectionChanged;
		nativeView.CleanUp();
		base.DisconnectHandler(nativeView);
	}

	/// <inheritdoc />
	protected override MauiDrawingView CreatePlatformView() => new();

	void OnPlatformViewDrawingLineCompleted(object? sender, MauiDrawingLineCompletedEventArgs e)
	{
		VirtualView.DrawingLineCompleted(new DrawingLineCompletedEventArgs()
		{
			LineColor = e.Line.LineColor,
			EnableSmoothedPath = e.Line.EnableSmoothedPath,
			Granularity = e.Line.Granularity,
			LineWidth = e.Line.LineWidth,
			Points = e.Line.Points
		});
	}

	void OnLinesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
	{
		UpdateLines(VirtualView, PlatformView);
	}

	static void UpdateLines(IDrawingView virtualView, MauiDrawingView platformView)
	{
		platformView.Lines.Clear();
		if (!virtualView.MultiLineMode && virtualView.Lines.Count > 1)
		{
			throw new InvalidOperationException("Only 1 line is allowed with multiline mode");
		}

		foreach (var line in virtualView.Lines)
		{
			platformView.Lines.Add(new MauiDrawingLine()
			{
				LineColor = line.LineColor,
				EnableSmoothedPath = line.EnableSmoothedPath,
				Granularity = line.Granularity,
				LineWidth = line.LineWidth,
				Points = line.Points
			});
		}
	}
}