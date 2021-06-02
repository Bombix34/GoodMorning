using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(InteractionTrigger))]
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
        linkedObject.transform.DOPunchScale(Vector3.one * amount, 0.3f);
    }

    public void DOScale(float newScale)
    {
        if (linkedObject == null)
            return;
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

}
