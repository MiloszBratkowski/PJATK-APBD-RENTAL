namespace PJATK_APBD_RENTAL.Reports;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message) => Console.WriteLine($"[INFO] {message}");
    public void LogError(string message) => Console.WriteLine($"[ERR] {message}");
}