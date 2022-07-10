using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Waves.Core.Extensions;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves paged list box.
/// </summary>
public class WavesPagedListBox : ListBox, IStyleable
{
    /// <summary>
    /// Defines <see cref="PageScrolled"/> property.
    /// </summary>
    public static readonly StyledProperty<ICommand> PageScrolledProperty = AvaloniaProperty.Register<WavesPagedListBox, ICommand>(nameof(PageScrolled));

    private bool _isPaginationInitialized;

    /// <summary>
    /// Creates new instance of <see cref="WavesPagedListBox"/>.
    /// </summary>
    public WavesPagedListBox()
    {
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
    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
    {
        if (change.Property.Name.Equals(nameof(Scroll)) || change.Property.Name.Equals(nameof(IsPointerOver)))
        {
            OnScrollChanged(Scroll);
        }

        base.OnPropertyChanged(change);
    }

    private void OnScrollChanged(IScrollable scrollable)
    {
        if (!_isPaginationInitialized)
        {
            var maximumProperty = typeof(IScrollable)
                .GetProperties((BindingFlags)int.MaxValue)
                .FirstOrDefault(x => x.Name.Equals("VerticalScrollBarMaximum"));

            var valueProperty = typeof(IScrollable)
                .GetProperties((BindingFlags)int.MaxValue)
                .FirstOrDefault(x => x.Name.Equals("VerticalScrollBarValue"));

            if (maximumProperty != null && valueProperty != null)
            {
                var maximum = (double)(maximumProperty.GetValue(this) ?? 0);
                var value = (double)(valueProperty.GetValue(this) ?? 0);

                // if (offset / 2 > Bounds.Bottom)
                // {
                //     PageScrolled?.Execute(null);
                // }
            }

            _isPaginationInitialized = true;
            UpdatePaginationDelay().FireAndForget();
        }
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
