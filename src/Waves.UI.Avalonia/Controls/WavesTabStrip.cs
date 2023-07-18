using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.VisualTree;

namespace Waves.UI.Avalonia.Controls;

/// <summary>
/// Waves tab strip.
/// </summary>
public class WavesTabStrip : SelectingItemsControl
{
    private static readonly FuncTemplate<IPanel> DefaultPanel =
        new FuncTemplate<IPanel>(() => new WrapPanel { Orientation = Orientation.Horizontal });

    /// <summary>
    /// Creates new instance of <see cref="TabStrip"/>.
    /// </summary>
    static WavesTabStrip()
    {
        SelectionModeProperty.OverrideDefaultValue<WavesTabStrip>(SelectionMode.AlwaysSelected);
        FocusableProperty.OverrideDefaultValue(typeof(WavesTabStrip), false);
        ItemsPanelProperty.OverrideDefaultValue<WavesTabStrip>(DefaultPanel);
    }

    /// <inheritdoc />
    protected override IItemContainerGenerator CreateItemContainerGenerator()
    {
        return new ItemContainerGenerator<WavesTabStripItem>(
            this,
            ContentControl.ContentProperty,
            ContentControl.ContentTemplateProperty);
    }

    /// <inheritdoc/>
    protected override void OnGotFocus(GotFocusEventArgs e)
    {
        base.OnGotFocus(e);

        if (e.NavigationMethod == NavigationMethod.Directional)
        {
            e.Handled = UpdateSelectionFromEventSource(e.Source);
        }
    }

    /// <inheritdoc/>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        if (e.Source is not IVisual source)
        {
            return;
        }

        var point = e.GetCurrentPoint(source);

        if (point.Properties.IsLeftButtonPressed)
        {
            e.Handled = UpdateSelectionFromEventSource(e.Source);
        }
    }
}
