using UnityEngine;

/// <summary>
/// Base class for ScriptableObjects that need a public description field.
/// </summary>
/// 

namespace XXX.SO
{
	public class BaseSODescription : ScriptableObject
	{
#if UNITY_EDITOR
		[TextArea(10, 20)] [SerializeField] private string _description;
#endif
	}
}
