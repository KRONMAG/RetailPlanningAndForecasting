namespace RetailPlanningAndForecasting.Services
{
    public interface IFileDialog
    {
        bool OpenFileDialog(out string path);
        bool SaveFileDialog(out string path);
    }
}