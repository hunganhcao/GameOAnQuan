using UnityEngine.Events;
using UnityEngine;
using System;

namespace XXX.SO.Event
{
    [CreateAssetMenu(menuName = SOPathConstant.Event + "Void", fileName = SOPathConstant.Name_Event + "void_")]
    public class SOVoidEvent : BaseSODescription 
    {
		public Action OnEventRaised;
		public void RaiseEvent()
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke();
		}
	}
}
