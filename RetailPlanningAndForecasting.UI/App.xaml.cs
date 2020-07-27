using System.Windows;
using DevExpress.Xpf.Grid;

namespace RetailPlanningAndForecasting
{
    public partial class App : Application
    {
        public App()
        {
            GridControl.AllowInfiniteGridSize = true;
        }
    }
}
