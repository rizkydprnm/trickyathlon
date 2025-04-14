using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class NodeTypeAttribute : PropertyAttribute
{
    public Type[] AllowedTypes { get; private set; }

    public NodeTypeAttribute(params Type[] allowedTypes)
    {
        AllowedTypes = allowedTypes;
    }
}