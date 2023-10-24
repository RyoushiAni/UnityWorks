using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerTrans;
    public float Speed;
    void Start()
    {
        PlayerTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Z = Input.GetAxisRaw("Vertical");
        PlayerTrans.transform.position = new Vector3(transform.position.x + (X * Speed), 0, transform.position.z + (Z * Speed));
    }
}
