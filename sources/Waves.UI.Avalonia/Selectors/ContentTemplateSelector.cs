using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Interfaces;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Avalonia.Selectors
{
    /// <summary>
    /// Tab content template selector.
    /// </summary>
    [WavesPlugin(typeof(ContentTemplateSelector))]
    public class ContentTemplateSelector : IDataTemplate, IWavesPlugin
    {
        private readonly IWavesCore _core;

        private IWavesView _oldWavesView;
        private IWavesViewModel _oldWavesViewModel;

        private Dictionary<object, DataTemplate> _resolvedTemplates;

        /// <summary>
        /// Creates new instance of <see cref="ContentTemplateSelector"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        public ContentTemplateSelector(IWavesCore core)
        {
            _core = core;
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        public event PropertyChangingEventHandler PropertyChanging;

        /// <inheritdoc />
        public bool IsInitialized { get; private set; }
        
        /// <inheritdoc />
        public IControl Build(object param)
        {
            var view = _core.GetInstanceAsync<IWavesView>(param);
            return (IControl)view;
        }

        /// <inheritdoc />
        public bool Match(object data)
        {
            return data is IWavesViewModel;
        }

        // /// <inheritdoc />
        // public override DataTemplate SelectTemplate(object item, DependencyObject container)
        // {
        //     if (item == null)
        //     {
        //         return null;
        //     }
        //
        //     _oldWavesView?.Dispose();
        //     _oldWavesViewModel?.ViewDisappeared();
        //
        //     if (_resolvedTemplates.ContainsKey(item))
        //     {
        //         return _resolvedTemplates[item];
        //     }
        //
        //     var template = TemplateGenerator.CreateDataTemplate(() =>
        //     {
        //         try
        //         {
        //             if (item is IWavesViewModel viewModel)
        //             {
        //                 var result = _core.GetInstanceAsync<IWavesView>(item.GetType()).Result;
        //                 result.DataContext = item;
        //                 result.InitializeAsync().FireAndForget();
        //
        //                 _oldWavesView = result;
        //                 _oldWavesViewModel = viewModel;
        //
        //                 viewModel.ViewAppeared();
        //
        //                 return result;
        //             }
        //         }
        //         catch (Exception e)
        //         {
        //             _core.WriteLogAsync(e, this).FireAndForget();
        //             return null;
        //         }
        //
        //         return null;
        //     });
        //
        //     _resolvedTemplates.Add(item, template);
        //
        //     return template;
        // }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return Task.CompletedTask;
            }

            _resolvedTemplates = new Dictionary<object, DataTemplate>();

            IsInitialized = true;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            OnPropertyChanging(args);
        }

        /// <inheritdoc />
        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args);
        }

        /// <summary>
        ///     Disposes object.
        /// </summary>
        /// <param name="disposing">Set
        ///     <value>true</value>
        ///     if you need to release managed and unmanaged resources. Set
        ///     <value>false</value>
        ///     if need to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: your code for release managed resources.
            }

            // TODO: your code for release unmanaged resources.
        }

        /// <summary>
        /// Callback when property changed.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Callback when property changing.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            PropertyChanging?.Invoke(this, e);
        }
    }
}
