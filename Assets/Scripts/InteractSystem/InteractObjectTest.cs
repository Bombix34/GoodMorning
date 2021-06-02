using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractObjectTest : MonoBehaviour
{
    public void Shake()
    {
        this.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f);
    }
}
