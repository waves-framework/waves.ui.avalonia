using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Avalonia.Styles.Enums;

namespace Waves.UI.Avalonia.Styles
{
    /// <summary>
    ///     Waves window.
    /// </summary>
    public class WavesWindow : Window, IStyleable
    {
        /// <summary>
        ///     Gets or sets client decorations property.
        /// </summary>
        public static readonly StyledProperty<bool> ClientDecorationsProperty =
            AvaloniaProperty.Register<WavesWindow, bool>(
                nameof(ClientDecorations));
        
        /// <summary>
        /// Defines <see cref="CurrentPlatform"/> property.
        /// </summary>
        public static readonly StyledProperty<OSPlatform> CurrentPlatformProperty =
            AvaloniaProperty.Register<WavesWindow, OSPlatform>(
                "CurrentPlatform", OSPlatform.Windows);
        
        /// <summary>
        /// Defines <see cref="IsWindowsPlatform"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsWindowsPlatformProperty =
            AvaloniaProperty.Register<WavesWindow, bool>(
                "IsWindowsPlatform", false);
        
        /// <summary>
        /// Defines <see cref="IsLinuxPlatform"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsLinuxPlatformProperty =
            AvaloniaProperty.Register<WavesWindow, bool>(
                "IsLinuxPlatform", false);
        
        /// <summary>
        /// Defines <see cref="IsOsxPlatform"/> property.
        /// </summary>
        public static readonly StyledProperty<bool> IsOsxPlatformProperty =
            AvaloniaProperty.Register<WavesWindow, bool>(
                "IsOsxPlatform", false);

        private bool _useCustomWindowForOsx = true;
        
        private Grid _contentGrid;
        private Grid _bottomHorizontalGrip;
        private Grid _bottomLeftGrip;
        private Grid _bottomRightGrip;
        private Button _closeButton;
        private Image _icon;
        private Grid _leftVerticalGrip;
        private Button _minimiseButton;

        private bool _mouseDown;
        private Point _mouseDownPosition;
        private Button _maximizeButton;
        private Path _restoreButtonPanelPath;
        private Grid _rightVerticalGrip;

        private Grid _titleBar;
        private Grid _topHorizontalGrip;
        private Grid _topLeftGrip;
        private Grid _topRightGrip;

        /// <summary>
        ///     Creates new instance of <see cref="WavesWindow" />
        /// </summary>
        protected WavesWindow()
        {
        }

        /// <summary>
        /// Initializes platorm.
        /// </summary>
        private void InitializePlatform()
        {
            SystemDecorations = SystemDecorations.BorderOnly;
            
            if (Net.Pkcs11Interop.Common.Platform.IsWindows)
            {
                CurrentPlatform = OSPlatform.Windows;
            }
            if (Net.Pkcs11Interop.Common.Platform.IsLinux)
            {
                CurrentPlatform = OSPlatform.Linux;
            }
            if (Net.Pkcs11Interop.Common.Platform.IsMacOsX)
            {
                CurrentPlatform = OSPlatform.OSX;
            }
            
            if (!Net.Pkcs11Interop.Common.Platform.IsMacOsX)
            {
                // do this in code or we get a delay in osx.
                ClientDecorations = true;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var classes = (int) GetClassLong(PlatformImpl.Handle.Handle, (int) ClassLongIndex.GCL_STYLE);

                    classes |= 0x00020000;

                    SetClassLong(PlatformImpl.Handle.Handle, ClassLongIndex.GCL_STYLE, new IntPtr(classes));
                }
            }
            else
            {
                ClientDecorations = false;
            }
        }

        /// <summary>
        /// Actions when "Maximize button clicked."
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnMaximize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Gets or sets current platform.
        /// </summary>
        [Reactive]
        public OSPlatform CurrentPlatform
        {
            get => GetValue(CurrentPlatformProperty);
            set
            {
                SetValue(CurrentPlatformProperty, value);

                if (value == OSPlatform.Windows)
                {
                    SetValue(IsWindowsPlatformProperty, true);
                    SetValue(IsLinuxPlatformProperty, false);
                    SetValue(IsOsxPlatformProperty, false);
                }
                
                if (value == OSPlatform.Linux)
                {
                    SetValue(IsWindowsPlatformProperty, false);
                    SetValue(IsLinuxPlatformProperty, true);
                    SetValue(IsOsxPlatformProperty, false);
                }
                
                if (value == OSPlatform.OSX)
                {
                    SetValue(IsWindowsPlatformProperty, false);
                    SetValue(IsLinuxPlatformProperty, false);
                    SetValue(IsOsxPlatformProperty, true);
                }
            }
        }

        /// <summary>
        /// Gets or sets whether app running on Windows.
        /// </summary>
        [Reactive]
        public bool IsWindowsPlatform
        {
            get => GetValue(IsWindowsPlatformProperty);
            set => SetValue(IsWindowsPlatformProperty, value);
        }
        
        /// <summary>
        /// Gets or sets whether app running on Linux.
        /// </summary>
        [Reactive]
        public bool IsLinuxPlatform
        {
            get => GetValue(IsLinuxPlatformProperty);
            set => SetValue(IsLinuxPlatformProperty, value);
        }
        
        /// <summary>
        /// Gets or sets whether app running on OSX.
        /// </summary>
        [Reactive]
        public bool IsOsxPlatform
        {
            get => GetValue(IsOsxPlatformProperty);
            set => SetValue(IsOsxPlatformProperty, value);
        }

        /// <summary>
        ///     Gets or sets Client decorations.
        /// </summary>
        public bool ClientDecorations
        {
            get => GetValue(ClientDecorationsProperty);
            set => SetValue(ClientDecorationsProperty, value);
        }

        /// <summary>
        ///     Style key.
        /// </summary>
        Type IStyleable.StyleKey => typeof(WavesWindow);

        /// <summary>
        ///     Actions when pointer pressed.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            if (_topHorizontalGrip.IsPointerOver)
            {
                if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
                {
                    BeginResizeDrag(WindowEdge.North, e);
                }
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

        /// <summary>
        ///     Actions when pointer released.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            _mouseDown = false;
            base.OnPointerReleased(e);
        }

        /// <summary>
        ///     Actions when template applied.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            InitializePlatform();
            
            _contentGrid = e.NameScope.Find<Grid>("ContentGrid");
            _restoreButtonPanelPath = e.NameScope.Find<Path>("restoreButtonPanelPath");
            _icon = e.NameScope.Find<Image>("icon");

            _topHorizontalGrip = e.NameScope.Find<Grid>("TopHorizontalGrip");
            _bottomHorizontalGrip = e.NameScope.Find<Grid>("BottomHorizontalGrip");
            _leftVerticalGrip = e.NameScope.Find<Grid>("LeftVerticalGrip");
            _rightVerticalGrip = e.NameScope.Find<Grid>("RightVerticalGrip");

            _topLeftGrip = e.NameScope.Find<Grid>("TopLeftGrip");
            _bottomLeftGrip = e.NameScope.Find<Grid>("BottomLeftGrip");
            _topRightGrip = e.NameScope.Find<Grid>("TopRightGrip");
            _bottomRightGrip = e.NameScope.Find<Grid>("BottomRightGrip");

            InitializeTitleBar(e.NameScope);
            InitializeOsxWindowButtonsEvents(e.NameScope);

            if (_titleBar != null)
                _titleBar.DoubleTapped += (sender, ee) => { ToggleWindowState(); };

            if (_icon != null)
                _icon.DoubleTapped += (sender, ee) => { Close(); };
        }

        /// <summary>
        /// Initializes title bar.
        /// </summary>
        /// <param name="nameScope">Name scope.</param>
        private void InitializeTitleBar(INameScope nameScope)
        {
            _titleBar = nameScope.Find<Grid>("TitleBarGrid");

            if (CurrentPlatform == OSPlatform.Windows)
            {
                _titleBar.Height = 34;
            }
            if (CurrentPlatform == OSPlatform.Linux)
            {
                
            }
            if (CurrentPlatform == OSPlatform.OSX)
            {
                _titleBar.Height = 22;
                
                if (_useCustomWindowForOsx)
                    return;
                
                // use system decorations, because custom not fully works
                _titleBar.IsVisible = false;
                SystemDecorations = SystemDecorations.Full;
                
                _topHorizontalGrip.IsHitTestVisible = false;
                _bottomHorizontalGrip.IsHitTestVisible = false;
                _leftVerticalGrip.IsHitTestVisible = false;
                _rightVerticalGrip.IsHitTestVisible = false;
                _topLeftGrip.IsHitTestVisible = false;
                _bottomLeftGrip.IsHitTestVisible = false;
                _topRightGrip.IsHitTestVisible = false;
                _bottomRightGrip.IsHitTestVisible = false;
                
                BorderThickness = new Thickness();
                _contentGrid.Margin = new Thickness(0, -4, 0, 0);
            }
        }

        /// <summary>
        /// Initializes window buttons for OSX.
        /// </summary>
        /// <param name="nameScope">Name scope.</param>
        private void InitializeOsxWindowButtonsEvents(INameScope nameScope)
        {
            if (CurrentPlatform != OSPlatform.OSX)
                return;
            
            _minimiseButton = nameScope.Find<Button>("OsxMinimizeButton");
            _maximizeButton = nameScope.Find<Button>("OsxMaximizeButton");
            _closeButton = nameScope.Find<Button>("OsxCloseButton");
            
            if (_minimiseButton != null)
                _minimiseButton.Click += (sender, ee) => { WindowState = WindowState.Minimized; };

            if (_maximizeButton != null)
                _maximizeButton.Click += (sender, ee) => { ToggleWindowState(); };

            if (_closeButton != null)
                _closeButton.Click += (sender, ee) => { Close(); };
        }

        /// <summary>
        ///     Toggles window state.
        /// </summary>
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

        /// <summary>
        ///     Retrieves the specified 32-bit value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        ///     The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive,
        ///     zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of
        ///     extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8
        ///     would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of
        ///     the following values.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the requested value. If the function fails, the return value is
        ///     zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        /// <summary>
        ///     Retrieves the specified 64-bit value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        ///     The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive,
        ///     zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of
        ///     extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8
        ///     would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of
        ///     the following values.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the requested value. If the function fails, the return value is
        ///     zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        ///     Replaces the specified value at the specified offset into the extra class memory or the WNDCLASSEX structure for
        ///     the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        ///     The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive,
        ///     zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of
        ///     extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8
        ///     would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of
        ///     the following values.
        /// </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the previous value of the specified 32-bit integer. If the value was
        ///     not previously set, the return value is zero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        public static IntPtr SetClassLong(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4) return SetClassLong32(hWnd, nIndex, dwNewLong);

            return SetClassLong64(hWnd, nIndex, dwNewLong);
        }

        /// <summary>
        ///     Retrieves the specified value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        ///     The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive,
        ///     zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of
        ///     extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8
        ///     would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of
        ///     the following values.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is the requested value. If the function fails, the return value is
        ///     zero. To get extended error information, call GetLastError.
        /// </returns>
        public static IntPtr GetClassLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        /// <summary>
        ///     Replaces the specified 64-bit (long) value at the specified offset into the extra class memory or the WNDCLASSEX
        ///     structure for the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        ///     The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive,
        ///     zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of
        ///     extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8
        ///     would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of
        ///     the following values.
        /// </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the previous value of the specified 32-bit integer. If the value was
        ///     not previously set, the return value is zero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        private static extern IntPtr SetClassLong64(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong);

        /// <summary>
        ///     Replaces the specified 32-bit (long) value at the specified offset into the extra class memory or the WNDCLASSEX
        ///     structure for the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">
        ///     The value to be replaced. To set a 32-bit value in the extra class memory, specify the positive,
        ///     zero-based byte offset of the value to be set. Valid values are in the range zero through the number of bytes of
        ///     extra class memory, minus four; for example, if you specified 12 or more bytes of extra class memory, a value of 8
        ///     would be an index to the third 32-bit integer. To set any other value from the WNDCLASSEX structure, specify one of
        ///     the following values.
        /// </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>
        ///     If the function succeeds, the return value is the previous value of the specified 32-bit integer. If the value was
        ///     not previously set, the return value is zero.
        ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        private static extern IntPtr SetClassLong32(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong);
    }
}