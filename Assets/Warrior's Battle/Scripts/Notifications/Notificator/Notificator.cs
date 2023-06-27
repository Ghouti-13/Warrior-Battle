using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notificator : MonoBehaviour
{
    [SerializeField] protected int id;
    protected int notifications;

    public int ID => id;

    protected virtual void Awake()
    {
        id = Random.Range(0, 100);
        NotificationHandler.AddNotificator(this);
    }
}
