using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotificationEventHandler
{
    public static event Action<object, int> OnNotificationReceived;
    public static event Action<object> OnNotificationSeen;

    public static void TriggerNotificationAddEvent(Notificator notificator, int notifications)
    {
        OnNotificationReceived?.Invoke(notificator, notifications);
    }
}
