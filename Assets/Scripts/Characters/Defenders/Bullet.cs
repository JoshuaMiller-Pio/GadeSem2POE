using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rigComp;

    public float dmg = 3;
    // Start is called before the first frame update
    void Start()
    {
        _rigComp = GetComponent<Rigidbody>();
        _rigComp.AddForce(transform.forward * 1000, ForceMode.Force);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
