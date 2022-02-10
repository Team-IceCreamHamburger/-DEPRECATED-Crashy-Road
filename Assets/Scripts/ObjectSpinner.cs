using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    [SerializeField] private float speed;


    void Awake() {
        ObjSpin(this.speed);
    }


    void Update() {
        ObjSpin(this.speed);    
    }

    void ObjSpin(float speed) {
        gameObject.transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
