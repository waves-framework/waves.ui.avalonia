using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Fluid.UI.Avalonia.Styles.Enums;

namespace Fluid.UI.Avalonia.Styles
{
    /// <summary>
    /// Fluid window.
    /// </summary>
    public class FluidWindow: Window, IStyleable
    {
        private Grid _bottomHorizontalGrip;
        private Grid _bottomLeftGrip;
        private Grid _bottomRightGrip;
        private Button _closeButton;
        private Image _icon;
        private Grid _leftVerticalGrip;
        private Button _minimiseButton;

        private bool _mouseDown;
        private Point _mouseDownPosition;
        private Button _restoreButton;
        private Path _restoreButtonPanelPath;
        private Grid _rightVerticalGrip;

        private Grid _titleBar;
        private Grid _topHorizontalGrip;
        private Grid _topLeftGrip;
        private Grid _topRightGrip;
        
        /// <summary>
        /// Replaces the specified 64-bit (long) value at the specified offset into the extra class memory or the WNDCLASSEX structure for the class to which the specified window belongs. 
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive, zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of the following values.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. If the value was not previously set, the return value is zero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        private static extern IntPtr SetClassLong64(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Replaces the specified 32-bit (long) value at the specified offset into the extra class memory or the WNDCLASSEX structure for the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive, zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of the following values.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. If the value was not previously set, the return value is zero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        private static extern IntPtr SetClassLong32(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong);
        
        /// <summary>
        /// Retrieves the specified 32-bit value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive, zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of the following values.</param>
        /// <returns>If the function succeeds, the return value is the requested value. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Retrieves the specified 64-bit value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive, zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of the following values.</param>
        /// <returns>If the function succeeds, the return value is the requested value. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);   
        
        /// <summary>
        /// Replaces the specified value at the specified offset into the extra class memory or the WNDCLASSEX structure for the class to which the specified window belongs. 
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive, zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of the following values.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. If the value was not previously set, the return value is zero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        public static IntPtr SetClassLong(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetClassLong32(hWnd, nIndex, dwNewLong);
            }

            return SetClassLong64(hWnd, nIndex, dwNewLong);
        }
        
        /// <summary>
        /// Retrieves the specified value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive, zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8 would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of the following values.</param>
        /// <returns>If the function succeeds, the return value is the requested value. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        public static IntPtr GetClassLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        /// <summary>
        /// Creates new instance of <see cref="FluidWindow"/>
        /// </summary>
        protected FluidWindow()
        {
            HasSystemDecorations = false;
            ClientDecorations = true;
            
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // do this in code or we get a delay in osx.
                ClientDecorations = true;

                if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var classes = (int)GetClassLong(this.PlatformImpl.Handle.Handle, (int)ClassLongIndex.GCL_STYLE);

                    classes |= (int)0x00020000;

                    SetClassLong(this.PlatformImpl.Handle.Handle, ClassLongIndex.GCL_STYLE, new IntPtr(classes));
                }
            }
        }

        public static readonly StyledProperty<bool> ClientDecorationsProperty =
            AvaloniaProperty.Register<FluidWindow, bool>(nameof(ClientDecorations));
        
        public bool ClientDecorations
        {
            get => GetValue(ClientDecorationsProperty);
            set => SetValue(ClientDecorationsProperty, value);
        }

        Type IStyleable.StyleKey => typeof(FluidWindow);
        
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            if (_topHorizontalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.North, e);
            }
            else if (_bottomHorizontalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.South, e);
            }
            else if (_leftVerticalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.West, e);
            }
            else if (_rightVerticalGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.East, e);
            }
            else if (_topLeftGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.NorthWest, e);
            }
            else if (_bottomLeftGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.SouthWest, e);
            }
            else if (_topRightGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.NorthEast, e);
            }
            else if (_bottomRightGrip.IsPointerOver)
            {
                BeginResizeDrag(WindowEdge.SouthEast, e);
            }
            else if (_titleBar.IsPointerOver)
            {
                _mouseDown = true;
                _mouseDownPosition = e.GetPosition(this);

                if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
                {
                    BeginMoveDrag(e);
                    _mouseDown = false;
                }
            }
            else
            {
                _mouseDown = false;
            }

            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            _mouseDown = false;
            base.OnPointerReleased(e);
        }
        
        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
			base.OnTemplateApplied(e);

            _titleBar = e.NameScope.Find<Grid>("TitleBarGrid");
            
            _minimiseButton = e.NameScope.Find<Button>("minimiseButton");
            _restoreButton = e.NameScope.Find<Button>("restoreButton");
            _restoreButtonPanelPath = e.NameScope.Find<Path>("restoreButtonPanelPath");
            _closeButton = e.NameScope.Find<Button>("closeButton");
            _icon = e.NameScope.Find<Image>("icon");

            _topHorizontalGrip = e.NameScope.Find<Grid>("topHorizontalGrip");
            _bottomHorizontalGrip = e.NameScope.Find<Grid>("bottomHorizontalGrip");
            _leftVerticalGrip = e.NameScope.Find<Grid>("leftVerticalGrip");
            _rightVerticalGrip = e.NameScope.Find<Grid>("rightVerticalGrip");

            _topLeftGrip = e.NameScope.Find<Grid>("topLeftGrip");
            _bottomLeftGrip = e.NameScope.Find<Grid>("bottomLeftGrip");
            _topRightGrip = e.NameScope.Find<Grid>("topRightGrip");
            _bottomRightGrip = e.NameScope.Find<Grid>("bottomRightGrip");

            if (_minimiseButton != null)
                _minimiseButton.Click += (sender, ee) => { WindowState = WindowState.Minimized; };

            if (_restoreButton != null)
                _restoreButton.Click += (sender, ee) => { ToggleWindowState(); };

            if (_titleBar != null)
                _titleBar.DoubleTapped += (sender, ee) => { ToggleWindowState(); };

            if (_closeButton != null)
                _closeButton.Click += (sender, ee) => { Close(); };

            if (_icon != null)
                _icon.DoubleTapped += (sender, ee) => { Close(); };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _topHorizontalGrip.IsVisible = false;
                _bottomHorizontalGrip.IsHitTestVisible = false;
                _leftVerticalGrip.IsHitTestVisible = false;
                _rightVerticalGrip.IsHitTestVisible = false;
                _topLeftGrip.IsHitTestVisible = false;
                _bottomLeftGrip.IsHitTestVisible = false;
                _topRightGrip.IsHitTestVisible = false;
                _bottomRightGrip.IsHitTestVisible = false;

                BorderThickness = new Thickness();
            }
        }

        private void ToggleWindowState()
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;

                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;
            }
        }
    }
}