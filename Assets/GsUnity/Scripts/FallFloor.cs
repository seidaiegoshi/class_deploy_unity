using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    [SerializeField] float waitingTime = 3f;
    [SerializeField] float initializeTime = 5f;
    [SerializeField] Color warningColor = Color.red;

    Rigidbody rb;
    Vector3 initalPosition; 
    Quaternion initialRotation;

    Color initialColor;
    MeshRenderer floorMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initalPosition = transform.position; 
        initialRotation = transform.rotation;

        floorMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        initialColor = floorMeshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("衝突した！");

        if (collision.gameObject.CompareTag("Player")) { 
            floorMeshRenderer.material.color = warningColor;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(waitingTime);
        rb.isKinematic = false;
        yield return new WaitForSeconds(initializeTime);
        Init();
    }



    void Init()
    {
        rb.isKinematic = true;
        transform.position = initalPosition;
        transform.rotation = initialRotation;
        floorMeshRenderer.material.color = initialColor;
    }
}


