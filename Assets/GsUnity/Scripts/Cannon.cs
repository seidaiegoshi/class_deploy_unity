using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float angleRange = 60f;
    [SerializeField] AudioClip shotSe;

    [SerializeField] Transform cannonFx; // CannonFxのTransformを格納する変数
    [SerializeField] GameObject shotFx; // パーティクルエフェクトを格納する変数


    float angleValue;
    Transform body;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        body = transform.GetChild(0);
        audioSource = GetComponent<AudioSource>();


    }

    private void Update()
    {
        float cycle = Mathf.Sin(Time.time * rotationSpeed);

        angleValue = cycle * angleRange;
        body.localRotation = Quaternion.AngleAxis(angleValue, Vector3.up);

    }


    public void Shot(float power)
    {
        var cannonBall = Instantiate(cannonBallPrefab, muzzle.position, Quaternion.identity);
        var cannonBallRb = cannonBall.GetComponent<Rigidbody>();
        cannonBallRb.AddForce(muzzle.forward * power, ForceMode.VelocityChange);
        audioSource.PlayOneShot(shotSe);
        Instantiate(shotFx, cannonFx);

    }


}
