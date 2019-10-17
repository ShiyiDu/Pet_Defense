using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTank : MonoBehaviour
{
    //how long does it take to rotate one round?
    public float speedFactor = 3.14f;

    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //todo: rotation behaviour looks a little wierd when parent change y rotaion
        if (rigid) transform.Rotate(0, 0, -Mathf.Abs(rigid.velocity.x * speedFactor));
    }
}
