using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationHandler : MonoBehaviour
{
    private static List<Notificator> _notificators = new List<Notificator>();

    public static void AddNotificator(Notificator notificator)
    {
        _notificators.Add(notificator);
    }
    private void Start()
    {
        NotificationEventHandler.TriggerNotificationAddEvent(_notificators[0], 2);
        NotificationEventHandler.TriggerNotificationAddEvent(_notificators[1], 1);
    }
}
