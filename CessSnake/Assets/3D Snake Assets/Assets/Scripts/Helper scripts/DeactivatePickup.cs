using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactivate", Random.Range(2f, 4f));
    }

    // Update is called once per frame
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
