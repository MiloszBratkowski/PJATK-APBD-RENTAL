namespace PJATK_APBD_RENTAL.Reports;

public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
}