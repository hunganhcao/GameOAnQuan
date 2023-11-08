using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace XXX.SO.Event
{
	public class BaseSODataEvent<T> : BaseSODescription
	{
		public Action<T> OnEventRaised;

		public void RaiseEvent(T value)
		{
			if (OnEventRaised != null)
				OnEventRaised.Invoke(value);
		}
	}
}
