using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailPrefab : MonoBehaviour
{   
    

    // Start is called before the first frame update
    void Awake()
    {
        transform.Rotate(180.0f, 0f, 90f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            GetComponent<Transform>().eulerAngles = new Vector3(180f, 0f, 180f);

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            GetComponent<Transform>().eulerAngles = new Vector3(180f, 0f, -180f);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            GetComponent<Transform>().eulerAngles = new Vector3(180f, 0f, 90f);

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            GetComponent<Transform>().eulerAngles = new Vector3(180f, 0f, -90f);

        }
    }
}
