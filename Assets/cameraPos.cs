using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Transform cameraPosition;

    // Update is called once per frame
    void Update()
    {
        cameraPosition.position = transform.position;
    }
}
