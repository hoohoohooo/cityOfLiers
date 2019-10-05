using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class questEvent : ScriptableObject
{
    public bool clickCheck = false;
    public IEnumerator qEvent()
    {
        yield return null;
    }
}
