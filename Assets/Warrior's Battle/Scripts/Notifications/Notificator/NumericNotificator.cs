using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericNotificator : Notificator
{
    [SerializeField] private NotificationNumericViewer _viewer;

    protected virtual void OnEnable()
    {
        NotificationEventHandler.OnNotificationReceived += HandleNotificationAdded;
    }

    private void HandleNotificationAdded(object notificator, int notificationsAmount)
    {
        if (this.GetType() != notificator.GetType()) return;

        var numericNotificator = notificator as NumericNotificator;

        numericNotificator.notifications += notificationsAmount;
        print(numericNotificator.name + " has " + numericNotificator.notifications + " notifications.");
    }

    protected virtual void OnDisable()
    {
        NotificationEventHandler.OnNotificationReceived -= HandleNotificationAdded;
    }
}
