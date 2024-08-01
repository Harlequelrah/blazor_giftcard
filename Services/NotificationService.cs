public class NotificationService
{
    public event Action<string, string> OnNotify;

    public void Notify(string message, string notificationClass)
    {
        OnNotify?.Invoke(message, notificationClass);
    }
}
