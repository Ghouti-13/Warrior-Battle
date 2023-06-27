using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteNotificator : Notificator
{
    [SerializeField] private NotificationSpriteViewer _viewer;

    protected virtual void OnEnable()
    {
        NotificationEventHandler.OnNotificationReceived += HandleNotificationAdded;
    }

    private void HandleNotificationAdded(object notificator, int notificationsAmount)
    {
        if (this.GetType() != notificator.GetType()) return;

        var spriteNotificator = notificator as SpriteNotificator;

        spriteNotificator.notifications += notificationsAmount;
        print(spriteNotificator.name + " has " + spriteNotificator.notifications + " notifications.");
    }

    protected virtual void OnDisable()
    {
        NotificationEventHandler.OnNotificationReceived -= HandleNotificationAdded;
    }
}
