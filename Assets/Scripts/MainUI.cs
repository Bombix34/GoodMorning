using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] private TextMeshProUGUI debugText;

    public void DisplayDebugText(bool isActive,  string text="")
    {
        debugText.transform.parent.DOScale(isActive ? 1f : 0f, 0.3f);
        debugText.text = text;
    }
}
