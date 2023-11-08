using UnityEngine;

/// <summary>
/// Base class for ScriptableObjects that need a public description field.
/// </summary>
public class BaseSODescription : ScriptableObject
{
    [Multiline(10)]
    [SerializeField] private string description;
}

public class SOPath
{
    public const string Event = "XXX/SO/Events/";
    public const string DataReference = "XXX/SO/DataReference/";
}
