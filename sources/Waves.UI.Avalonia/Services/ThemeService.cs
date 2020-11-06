﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Avalonia.Base;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Base.Interfaces;
using Waves.UI.Services.Interfaces;
using Application = Avalonia.Application;

namespace Waves.UI.Avalonia.Services
{
    /// <summary>
    ///     Windows UI theme service.
    /// </summary>
    [Export(typeof(IService))]
    public class ThemeService : Service, IThemeService
    {
        private const string PrimaryLightColorsDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Primary.Light.xaml";
        private const string PrimaryDarkColorsDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Primary.Dark.xaml";
        private const string AccentWhiteColorsDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Accent.White.xaml";
        private const string AccentBlackColorsDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Accent.Haiti.xaml";
        private const string AccentBlueColorsDictionaryUri =
            "avares://Waves.UI.Avalonia/Colors/Accent.Picton.Blue.xaml";
        private const string AccentGreenColorsDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Accent.Jade.xaml";
        private const string AccentRedColorsDictionaryUri =
            "avares://Waves.UI.Avalonia/Colors/Accent.Sunset.Orange.xaml";
        private const string AccentYellowColorsDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Accent.Ronchi.xaml";
        private const string AccentTemplateDictionaryUri = 
            "avares://Waves.UI.Avalonia/Colors/Accent.Template.xaml";
        private const string MiscellaneousColorsDictionaryUri =
            "avares://Waves.UI.Avalonia/Colors/Miscellaneous.Classic.xaml";
        
        private readonly Uri _coreUri = new Uri("avares://Waves.UI.Avalonia/Core.axaml");

        private readonly object _themesCollectionLocker = new object();
        
        private StyleInclude _oldAccentStyleInclude;
        private StyleInclude _oldMiscellaneousStyleInclude;
        private StyleInclude _oldPrimaryStyleInclude;

        private ITheme _selectedTheme;

        private Guid _selectedThemeId = Guid.Empty;
        
        private Application _application;

        /// <inheritdoc />
        public event EventHandler ThemeChanged;

        /// <inheritdoc />
        public override Guid Id { get; } = Guid.Parse("85B35BE0-4543-402A-A880-E1C580F001D5");

        /// <inheritdoc />
        public override string Name { get; set; } = "Avalonia UI Theme Service";

        /// <inheritdoc />
        [Reactive]
        public bool UseDarkScheme { get; set; }

        /// <inheritdoc />
        [Reactive]
        public bool UseAutomaticScheme { get; set; } = true;

        /// <inheritdoc />
        [Reactive]
        public ITheme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTheme, value);
                OnThemeChanged();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public ObservableCollection<ITheme> Themes { get; protected set; } = new ObservableCollection<ITheme>();

        /// <inheritdoc />
        public override void Initialize(ICore core)
        {
            if (IsInitialized) return;

            try
            {
                Core = core;

                InitializeThemes();

                OnMessageReceived(
                    this,
                    new Message(
                        "Initialization", 
                        "Service has been initialized.", 
                        Name, 
                        MessageType.Information));


                IsInitialized = true;
            }
            catch (Exception e)
            {
                OnMessageReceived(this,
                    new Message(
                        "Service initialization", 
                        "Error service initialization.", 
                        Name, 
                        e, 
                        false));
            }
        }

        /// <inheritdoc />
        public override void LoadConfiguration()
        {
            try
            {
                _selectedThemeId =
                    LoadConfigurationValue(
                        Core.Configuration, 
                        "ThemesService-SelectedThemeId", 
                        Guid.Empty);
                
                UseAutomaticScheme =
                    LoadConfigurationValue(
                        Core.Configuration, 
                        "ThemesService-UseAutomaticScheme", 
                        false);
                
                UseDarkScheme = LoadConfigurationValue(
                    Core.Configuration,
                    "ThemesService-UseDarkScheme", 
                    false);
                
                OnMessageReceived(
                    this, 
                    new Message(
                        "Loading configuration",
                        "Configuration loads successfully.", 
                        Name,
                        MessageType.Success));
            }
            catch (Exception e)
            {
                OnMessageReceived(this,
                    new Message(
                        "Loading configuration", 
                        "Error loading configuration.", 
                        Name, 
                        e,
                        false));
            }
        }

        /// <inheritdoc />
        public override void SaveConfiguration()
        {
            try
            {
                Core.Configuration.SetPropertyValue("ThemesService-SelectedThemeId", _selectedThemeId);
                Core.Configuration.SetPropertyValue("ThemesService-UseAutomaticScheme", UseAutomaticScheme);
                Core.Configuration.SetPropertyValue("ThemesService-UseDarkScheme", UseDarkScheme);
                
                OnMessageReceived(
                    this, 
                    new Message(
                        "Saving configuration",
                        "Configuration saved successfully.", 
                        Name,
                        MessageType.Success));
            }
            catch (Exception e)
            {
                OnMessageReceived(this,
                    new Message(
                        "Saving configuration", 
                        "Error saving configuration.", 
                        Name, 
                        e,
                        false));
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Themes.Clear();
        }
        
        /// <summary>
        ///     Attaches application.
        /// </summary>
        /// <param name="application">Instance of avalonia application.</param>
        public void AttachApplication(Application application)
        {
            try
            {
                _application = application;

                InitializeSelectedTheme();
                //InitializeSystemThemeCheckerDaemon();

                OnMessageReceived(
                    this,
                    new Message(
                        "Initialization", 
                        $"Application attached - {application.Name}.", 
                        Name, 
                        MessageType.Information));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Notifies when theme changed.
        /// </summary>
        protected virtual void OnThemeChanged()
        {
            _selectedThemeId = _selectedTheme.Id;
            UseDarkScheme = _selectedTheme.UseDarkSet;
            
            UpdateTheme();
            
            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        ///     Actions when theme's primary color set changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnThemePrimaryColorSetChanged(object sender, EventArgs e)
        {
            UseDarkScheme = _selectedTheme.UseDarkSet;
            UpdateTheme();
        }

        /// <summary>
        ///     Initializes themes.
        /// </summary>
        private void InitializeThemes()
        {
            try
            {
                Themes.Clear();

                InitializeBaseThemes();

                if (Themes.Count == 0)
                    throw new Exception("Themes were not loaded.");
            }
            catch (Exception e)
            {
                OnMessageReceived(this,
                    new Message("Theme Service", "Error initializing base themes:\r\n" + e, Name, MessageType.Error));
            }
        }

        /// <summary>
        ///     Initializes base themes.
        /// </summary>
        private void InitializeBaseThemes()
        {
            var lightPrimaryColorsStylesInclude = CreateStyle(PrimaryLightColorsDictionaryUri);
            var darkPrimaryColorsStylesInclude = CreateStyle(PrimaryDarkColorsDictionaryUri);
            var accentGreenColorsStylesInclude = CreateStyle(AccentGreenColorsDictionaryUri);
            var accentBlueColorsStylesInclude = CreateStyle(AccentBlueColorsDictionaryUri);
            var accentRedColorsStylesInclude = CreateStyle(AccentRedColorsDictionaryUri);
            var accentYellowColorsStylesInclude = CreateStyle(AccentYellowColorsDictionaryUri);
            var miscellaneousColorsStylesInclude = CreateStyle(MiscellaneousColorsDictionaryUri);

            var colorSetNameKey = "ColorSetName";

            var primaryLightColorName = lightPrimaryColorsStylesInclude.GetString(colorSetNameKey);
            var primaryDarkColorName = darkPrimaryColorsStylesInclude.GetString(colorSetNameKey);
            var accentGreenColorName = accentGreenColorsStylesInclude.GetString(colorSetNameKey);
            var accentBlueColorName = accentBlueColorsStylesInclude.GetString(colorSetNameKey);
            var accentRedColorName = accentRedColorsStylesInclude.GetString(colorSetNameKey);
            var accentYellowColorName = accentYellowColorsStylesInclude.GetString(colorSetNameKey);
            var miscellaneousColorName = miscellaneousColorsStylesInclude.GetString(colorSetNameKey);

            var primaryLightColorSetId = lightPrimaryColorsStylesInclude.GetGuidFromString("ColorSetId");
            var primaryDarkColorSetId = darkPrimaryColorsStylesInclude.GetGuidFromString("ColorSetId");
            var accentGreenColorSetId = accentGreenColorsStylesInclude.GetGuidFromString("ColorSetId");
            var accentBlueColorSetId = accentBlueColorsStylesInclude.GetGuidFromString("ColorSetId");
            var accentRedColorSetId = accentRedColorsStylesInclude.GetGuidFromString("ColorSetId");
            var accentYellowColorSetId = accentYellowColorsStylesInclude.GetGuidFromString("ColorSetId");
            var miscellaneousColorSetId = miscellaneousColorsStylesInclude.GetGuidFromString("ColorSetId");

            var lightPrimaryColorSet = new PrimaryColorSet(
                primaryLightColorSetId,
                primaryLightColorName,
                lightPrimaryColorsStylesInclude);

            var darkPrimaryColorSet = new PrimaryColorSet(
                primaryDarkColorSetId,
                primaryDarkColorName,
                darkPrimaryColorsStylesInclude);

            var greenAccentColorSet = new AccentColorSet(
                accentGreenColorSetId,
                accentGreenColorName,
                accentGreenColorsStylesInclude);

            var blueAccentColorSet = new AccentColorSet(
                accentBlueColorSetId,
                accentBlueColorName,
                accentBlueColorsStylesInclude);

            var redAccentColorSet = new AccentColorSet(
                accentRedColorSetId,
                accentRedColorName,
                accentRedColorsStylesInclude);

            var yellowAccentColorSet = new AccentColorSet(
                accentYellowColorSetId,
                accentYellowColorName,
                accentYellowColorsStylesInclude);

            var miscellaneousColorSet = new MiscellaneousColorSet(
                miscellaneousColorSetId,
                miscellaneousColorName,
                miscellaneousColorsStylesInclude);

            Themes.Add(new Theme(
                Guid.Parse("12E87107-C2FC-4D68-908C-377B80A4056A"),
                accentGreenColorName,
                lightPrimaryColorSet,
                darkPrimaryColorSet,
                greenAccentColorSet,
                miscellaneousColorSet));

            Themes.Add(new Theme(
                Guid.Parse("2549DD92-C094-4EF5-B50D-0DD187DFE154"),
                accentBlueColorName,
                lightPrimaryColorSet,
                darkPrimaryColorSet,
                blueAccentColorSet,
                miscellaneousColorSet));

            Themes.Add(new Theme(
                Guid.Parse("07B33F24-141B-4D9A-9C3A-946B0FC6BC82"),
                accentRedColorName,
                lightPrimaryColorSet,
                darkPrimaryColorSet,
                redAccentColorSet,
                miscellaneousColorSet));

            Themes.Add(new Theme(
                Guid.Parse("4F7E14EA-B219-40C6-8870-D9D080756D15"),
                accentYellowColorName,
                lightPrimaryColorSet,
                darkPrimaryColorSet,
                yellowAccentColorSet,
                miscellaneousColorSet));

            foreach (var theme in Themes)
            {
                theme.PrimaryColorSetChanged += OnThemePrimaryColorSetChanged;
            }
        }

        /// <summary>
        ///     Updates theme.
        /// </summary>
        private void UpdateTheme()
        {
            if (_application == null) return;

            try
            {
                var styles = _application.Styles;
                var stylesToRemove = new List<IStyle>();

                // searching for theme style includes
                foreach (var style in styles)
                {
                    if (!(style is StyleInclude styleInclude))
                        continue;
                    
                    if (styleInclude.Source == null)
                        continue;
                    
                    if (styleInclude.Source.Equals(_coreUri))
                        continue;
                    
                    stylesToRemove.Add(style);
                }

                // removing theme style includes before adding new 
                foreach (var style in stylesToRemove) 
                    styles.Remove(style);

                if (!(SelectedTheme.PrimaryColorSet is PrimaryColorSet primaryColorSet)) return;
                if (!(SelectedTheme.AccentColorSet is AccentColorSet accentColorSet)) return;
                if (!(SelectedTheme.MiscellaneousColorSet is MiscellaneousColorSet miscellaneousColorSet)) return;

                styles.Insert(0, primaryColorSet.StyleInclude);
                styles.Insert(1, accentColorSet.StyleInclude);
                styles.Insert(2, miscellaneousColorSet.StyleInclude);

                _oldPrimaryStyleInclude = primaryColorSet.StyleInclude;
                _oldAccentStyleInclude = accentColorSet.StyleInclude;
                _oldMiscellaneousStyleInclude = miscellaneousColorSet.StyleInclude;

                OnMessageReceived(this,
                    SelectedTheme.UseDarkSet
                        ? new Message(
                            "Theme Service",
                            $"Theme changed to \"{SelectedTheme.Name}\" (Dark).", 
                            Name,
                            MessageType.Information)
                        : new Message(
                            "Theme Service", 
                            $"Theme changed to \"{SelectedTheme.Name}\" (Light).",
                            Name,
                            MessageType.Information));
            }
            catch (Exception e)
            {
                OnMessageReceived(
                    this,
                    new Message("Theme Service", 
                        "Error updating theme", 
                        Name, 
                        e,
                        false));
            }
        }

        /// <summary>
        ///     Initialized selected theme.
        /// </summary>
        private void InitializeSelectedTheme()
        {
            if (Themes.Count > 0)
                try
                {
                    if (_selectedThemeId.Equals(Guid.Empty))
                    {
                        SelectedTheme = Themes.First();
                    }
                    else
                    {
                        foreach (var theme in Themes)
                        {
                            if (theme.Id != _selectedThemeId) continue;

                            SelectedTheme = theme;

                            SelectedTheme.UseDarkSet = UseDarkScheme;

                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    OnMessageReceived(this,
                        new Message("Theme Service", "Error initializing selected theme:\r\n" + e, Name,
                            MessageType.Error));
                }
            else
                OnMessageReceived(this, new Message("Theme Service", "Themes not found.", Name, MessageType.Error));
        }

        /// <summary>
        ///     Creates style for current url.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns>Style.</returns>
        private static StyleInclude CreateStyle(string url)
        {
            var self = new Uri("resm:Colors?assembly=Waves.UI.Avalonia");

            return new StyleInclude(self)
            {
                Source = new Uri(url)
            };
        }
    }
}