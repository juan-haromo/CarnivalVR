using UnityEngine;

public interface IRestrictedObject
{
    public void OnEnterRestrictedArea(Transform exitPosition);
}