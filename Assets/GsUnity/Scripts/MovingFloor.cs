using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] float moveRange;
    [SerializeField] float offset;
    // Start is called before the first frame update

    Vector3 initalPosition;
    Transform floor;

    void Start()
    {
        floor = transform.GetChild(0);
        initalPosition = floor.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float floorPositionX = Mathf.Sin(Time.time * moveSpeed+ offset);
        floor.localPosition = new Vector3(floorPositionX * moveRange, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // GameObjectを子にするには transform.SetParent(Transform p); をつかう
            // collisionは自分自身、body.transorm は床オブジェクト
            collision.transform.SetParent(floor.transform);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 親子関係を解消するには SetParent(null)　とする
            collision.transform.SetParent(null);
        }
    }

}
