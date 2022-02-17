using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Tools.Audio;
using Tools.Managers;

public class InteractActions : MonoBehaviour
{
    private GameObject linkedObject;
    private InteractionTrigger interactTrigger;

    private void Awake()
    {
        interactTrigger = GetComponent<InteractionTrigger>();
        linkedObject = this.transform.GetChild(0)?.gameObject;
    }

    public void DOPunchScale(float amount)
    {
        if (linkedObject == null)
            return;
        linkedObject.transform.DORewind();
        linkedObject.transform.DOPunchScale(Vector3.one * amount, 0.3f);
    }

    public void DOScale(float newScale)
    {
        if (linkedObject == null)
            return;
        linkedObject.transform.DORewind();
        linkedObject.transform.DOScale(Vector3.one * newScale, 0.3f);
    }

    public void TeleportOther(Transform newPosition)
    {
        interactTrigger.CurrentObjectInteract.transform.position = newPosition.position;
    }

    public void LaunchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayDebugText(string text)
    {
        MainUI.Instance.DisplayDebugText(true, text);
    }

    public void HiddeDebugText()
    {
        MainUI.Instance.DisplayDebugText(false);
    }

    public void SetAnimatorBoolTrue(string boolKey)
    {
        linkedObject.GetComponentInChildren<Animator>()?.SetBool(boolKey, true);
    }

    public void SetAnimatorBoolFalse(string boolKey)
    {
        linkedObject.GetComponentInChildren<Animator>()?.SetBool(boolKey, false);
    }

    public void SetAnimatorBoolInvert(string boolKey)
    {
        Animator animator = linkedObject.GetComponentInChildren<Animator>();
        if (animator == null)
            return;
        bool currentBoolValue = animator.GetBool(boolKey);
        animator.SetBool(boolKey, !currentBoolValue);
    }

    public void SetAnimatorTrigger(string triggerKey)
    {
        linkedObject.GetComponentInChildren<Animator>()?.SetTrigger(triggerKey);
    }

    public void PlaySFXSound(int audioType)
    {
        SoundManager.Instance.PlaySound((AudioFieldEnum) audioType);
    }

}
