using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IListener
{
	//Notification function to be invoked on Listeners when events happen
	void OnEvent(int itemCode, Component sender, object param = null);
}

public interface IItem
{
	void UseItem();
}
