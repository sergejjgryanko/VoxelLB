using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hummer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>() != null)
            collision.gameObject.transform.GetComponent<Rigidbody>().AddForce(-this.transform.forward * 700);
    }
}
