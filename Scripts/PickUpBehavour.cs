using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavour : MonoBehaviour
{
    private Vector3 rotationVector; 
    // Start is called before the first frame update
    void Start()
    {
        rotationVector =  new Vector3(30, 45 , 60);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
