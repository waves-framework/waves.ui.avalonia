using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Styling;
using Waves.Core.Extensions;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Paged data grid.
/// </summary>
public sealed class WavesPagedDataGrid : DataGrid, IStyleable
{
    /// <summary>
    /// Defines <see cref="PageScrolled"/> property.
    /// </summary>
    public static readonly StyledProperty<ICommand> PageScrolledProperty = AvaloniaProperty.Register<WavesPagedDataGrid, ICommand>(nameof(PageScrolled));

    private bool _isPaginationInitialized;
    private ScrollBar _scrollBar;

    /// <summary>
    /// Creates new instance of <see cref="WavesPagedDataGrid"/>.
    /// </summary>
    public WavesPagedDataGrid()
    {
        PointerWheelChanged += OnPointerWheelChanged;
        Sorting += OnSorting;
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
    Type IStyleable.StyleKey => typeof(DataGrid);

    private void OnVerticalScroll(object? sender, ScrollEventArgs e)
    {
        if (e.ScrollEventType == ScrollEventType.EndScroll)
        {
        }
    }

    private void OnPointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
        if (!_isPaginationInitialized)
        {
            var offsetProperty = typeof(DataGrid)
                .GetFields((BindingFlags)int.MaxValue)
                .FirstOrDefault(x => x.Name.Equals("_verticalOffset"));

            if (offsetProperty != null)
            {
                var offset = (double)(offsetProperty.GetValue(this) ?? 0);

                if (offset / 2 > Bounds.Bottom)
                {
                    PageScrolled?.Execute(null);
                }
            }

            _isPaginationInitialized = true;
            UpdatePaginationDelay().FireAndForget();
        }
    }

    private void OnSorting(object? sender, DataGridColumnEventArgs e)
    {
    }

    private async Task UpdatePaginationDelay()
    {
        await Task.Delay(250);
        _isPaginationInitialized = false;
    }
}
