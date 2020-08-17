using Microsoft.Win32;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.UI
{
    public class FileDialog : IFileDialog
    {
        public bool OpenFileDialog(out string path)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                return true;
            }
            path = null;
            return false;
        }

        public bool SaveFileDialog(out string path)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                return true;
            }
            path = null;
            return false;
        }
    }
}