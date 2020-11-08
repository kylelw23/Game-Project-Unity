using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBodyParts : MonoBehaviour
{

    void Start()
    {
        //wait 5 seconds before the body parts disappear
        Destroy(this.gameObject, 5.0f);
    }

}
