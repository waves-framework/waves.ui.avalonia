using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Waves.Core.Extensions;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves paged list box.
/// </summary>
public class WavesPagedListBox : ListBox, IStyleable, IDisposable
{
    /// <summary>
    /// Defines <see cref="PageScrolled"/> property.
    /// </summary>
    public static readonly StyledProperty<ICommand> PageScrolledProperty = AvaloniaProperty.Register<WavesPagedListBox, ICommand>(nameof(PageScrolled));

    private bool _isPaginationInitialized;
    private double _verticalHeightMax = 0.0d;

    private CompositeDisposable _disposables = new CompositeDisposable();
    private CompositeDisposable _scrollViewerDisposables;

    /// <summary>
    /// Creates new instance of <see cref="WavesPagedListBox"/>.
    /// </summary>
    public WavesPagedListBox()
    {
        this.GetObservable(ListBox.ScrollProperty)
            .OfType<ScrollViewer>()
            .Take(1)
            .Subscribe(sv =>
            {
                _scrollViewerDisposables?.Dispose();
                _scrollViewerDisposables = new CompositeDisposable();

                sv.GetObservable(ScrollViewer.VerticalScrollBarMaximumProperty)
                    .Subscribe(newMax => _verticalHeightMax = newMax)
                    .DisposeWith(_scrollViewerDisposables);

                async void OnNext(Vector offset)
                {
                    //// if (offset.Y <= double.Epsilon)
                    //// {
                    ////     // at top
                    //// }

                    var delta = Math.Abs(_verticalHeightMax - offset.Y);
                    if (!(delta <= double.Epsilon) || _isPaginationInitialized)
                    {
                        return;
                    }

                    PageScrolled?.Execute(null);
                    _isPaginationInitialized = true;
                    UpdatePaginationDelay().FireAndForget();
                }

                sv.GetObservable(ScrollViewer.OffsetProperty)
                    .Subscribe(OnNext).DisposeWith(_disposables);
            }).DisposeWith(_disposables);
    }

    /// <summary>
    /// Gets or sets page scrolled command.
    /// </summary>
    public ICommand PageScrolled
    {
        get => GetValue(PageScrolledProperty);
        set => SetValue(PageScrolledProperty, value);
    }

    /// <inheritdoc />
    Type IStyleable.StyleKey => typeof(ListBox);

    /// <inheritdoc />
    public void Dispose()
    {
        _disposables.Dispose();
        _scrollViewerDisposables.Dispose();
    }

    /// <summary>
    /// Update pagination delay.
    /// </summary>
    private async Task UpdatePaginationDelay()
    {
        await Task.Delay(250);
        _isPaginationInitialized = false;
    }
}
