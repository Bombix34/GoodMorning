using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KLabsAudioTools/MusicObject")]
public class MusicObject : MonoBehaviour
{

    public bool triggerEntered = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            triggerEntered = !triggerEntered;
        }
    }
}
