using System;
using System.Linq;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.UI.Avalonia.Showcase.View.Control.Tabs;
using Waves.UI.Showcase.Common.Presentation.Tabs;

namespace Waves.UI.Avalonia.Showcase.Presentation.Controllers
{
    /// <summary>
    /// Main tab presentation controller.
    /// </summary>
    public class MainTabPresentationController : Waves.UI.Showcase.Common.Presentation.Controllers.MainTabPresentationController
    {
        /// <inheritdoc />
        public MainTabPresentationController(IWavesCore core) : base(core)
        {
        }
        
        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Main Tab Presenter Controller";

        /// <inheritdoc />
        public override void Initialize()
        {
            try
            {
                //var textTabPresentation = new TextTabPresentation(Core);
                //textTabPresentation.SetView(new TextTabView());
                //RegisterPresentation(textTabPresentation);

                var buttonsTabPresentation = new ButtonsTabPresentation(Core);
                buttonsTabPresentation.SetView(new ButtonsTabView());
                RegisterPresenter(buttonsTabPresentation);

                var comboBoxesTabPresentation = new ComboBoxesTabPresentation(Core);
                comboBoxesTabPresentation.SetView(new ComboBoxesTabView());
                RegisterPresenter(comboBoxesTabPresentation);

                var checkBoxesTabPresentation = new CheckBoxesTabPresentation(Core);
                checkBoxesTabPresentation.SetView(new CheckBoxesTabView());
                RegisterPresenter(checkBoxesTabPresentation);

                //var radioButtonsTabPresentation = new RadioButtonsTabPresentation(Core);
                //radioButtonsTabPresentation.SetView(new RadioButtonsTabView());
                //RegisterPresentation(radioButtonsTabPresentation);

                //var textBoxesTabPresentation = new TextBoxesTabPresentation(Core);
                //textBoxesTabPresentation.SetView(new TextBoxesTabView());
                //RegisterPresentation(textBoxesTabPresentation);

                //var listBoxesTabPresentation = new ListBoxesTabPresentation(Core);
                //listBoxesTabPresentation.SetView(new ListBoxesTabView());
                //RegisterPresentation(listBoxesTabPresentation);

                //var progressBarsTabPresentation = new ProgressBarsTabPresentation(Core);
                //progressBarsTabPresentation.SetView(new ProgressBarsTabView());
                //RegisterPresentation(progressBarsTabPresentation);

                //var menusTabPresentation = new MenusTabPresentation(Core);
                //menusTabPresentation.SetView(new MenusTabView());
                //RegisterPresentation(menusTabPresentation);

                var chartingTabPresentation = new ChartingTabPresentation(Core);
                chartingTabPresentation.SetView(new ChartingTabView());
                RegisterPresenter(chartingTabPresentation);

                //var configurationTabPresentation = new ConfigurationTabPresentation(Core);
                //configurationTabPresentation.SetView(new ConfigurationTabView());
                //RegisterPresentation(configurationTabPresentation);

                var coreTabPresentation = new CoreTabPresentation(Core);
                coreTabPresentation.SetView(new CoreTabView());
                RegisterPresenter(coreTabPresentation);

                var themeTabPresentation = new ThemeTabPresentation(Core);
                themeTabPresentation.SetView(new ThemeTabView());
                RegisterPresenter(themeTabPresentation);

                //var aboutTabPresentation = new AboutTabPresentation(Core);
                //aboutTabPresentation.SetView(new AboutTabView());
                //RegisterPresentation(aboutTabPresentation);

                OnMessageReceived(this,new WavesMessage("Initialization", "Main tab controller initialized.", "Main tab controller", WavesMessageType.Success));

                if (Presenters.Count > 0)
                    SelectedPresenter = Presenters.First();
            }
            catch (Exception e)
            {
                OnMessageReceived(this,new WavesMessage("Initialization", "Error initialization main tab controller:\r\n" + e, "Main tab controller", WavesMessageType.Error));
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Presenters.Clear();
        }
    }
}