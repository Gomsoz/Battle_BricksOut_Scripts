using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager eventMgr;
    public static EventManager EventMgr
    {
        get
        {
            return eventMgr;
        }
        set { }
    }

    public delegate void itemDelegate();
    public event itemDelegate itemEvent;

    //리스너 오브젝트 배열

    private Dictionary<int, List<IListener>> listeners 
        = new Dictionary<int, List<IListener>>();

    private void Awake()
    {
        if (eventMgr == null)
        {
            eventMgr = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EventHandler()
    {
        itemEvent();
    }

    public void AddItemListener(int itemCode, IListener listener)
    {
        List<IListener> listenerList = null;

        if(listeners.TryGetValue(itemCode, out listenerList))
        {
            listenerList.Add(listener);
            return;
        }

        listenerList = new List<IListener>();
        listenerList.Add(listener);
        listeners.Add(itemCode, listenerList);
    }

    public void PostNotification(
        int itemCode, Component Sender, object Param = null)
    {
        List<IListener> listener = new List<IListener>();

        if (!listeners.TryGetValue(itemCode, out listener))
        {
            return;
        }

        for(int i = 0; i < listener.Count; i++)
        {
            if(!listener[i].Equals(null))
                listener[i].OnEvent(itemCode, Sender, Param);
        }   
    }

}
