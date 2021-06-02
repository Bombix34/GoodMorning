using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractActions : MonoBehaviour
{
    private GameObject linkedObject;

    private void Awake()
    {
        linkedObject = this.transform.GetChild(0)?.gameObject;
    }

    public void DOPunchScale(float amount)
    {
        if (linkedObject == null)
            return;
        linkedObject.transform.DOPunchScale(Vector3.one * amount, 0.3f);
    }
}
