using Waves.UI.Avalonia.Controls.Drawing.Charting.View;
using Waves.UI.Drawing.Charting.Base.Interfaces;
using Waves.UI.Drawing.Charting.View.Interface;
using Waves.UI.Drawing.View.Interfaces;

namespace Waves.UI.Avalonia.Controls.Drawing.Factories
{
    /// <summary>
    /// Chart view factory.
    /// </summary>
    public class ChartViewFactory : IChartViewFactory
    {
        /// <inheritdoc />
        public IChartPresenterView GetChartView()
        {
            return new ChartView();
        }
    }
}