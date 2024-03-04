using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float waitBeforeStopShaking;
    

    public static Wood Instance;
    private bool isShaking = false;
    private bool stopShaking = false;
    private bool youWin = false;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        Knife.onWoodHit += ShakeMe;
    }
    private void OnDestroy()
    {
        Knife.onWoodHit -= ShakeMe;
    }
    void Update()
    {
        if (!youWin)
        {
            transform.Rotate(0f, 0f, speed * Time.deltaTime);

            if (isShaking)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y + 0.1f, 0), shakeSpeed * Time.deltaTime);
            }
            else if (stopShaking)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y - 0.1f, 0), shakeSpeed * Time.deltaTime);
            }
        }
    }
    private void ShakeMe()
    {
        StartCoroutine("ShakeNow");
    }
    IEnumerator ShakeNow()
    {
        Vector3 originPos = transform.position;

        if(!isShaking)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(waitBeforeStopShaking);

        isShaking = false;
        stopShaking = true;

        yield return new WaitForSeconds(waitBeforeStopShaking);
        stopShaking = false;
    }
}
