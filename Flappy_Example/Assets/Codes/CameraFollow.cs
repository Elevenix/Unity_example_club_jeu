using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform following;

    [SerializeField]
    public Transform mainCamera;

    public static CameraFollow cam;

    private void Start()
    {
        if(cam == null)
        {
            cam = this;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(following != null)
        {
            this.transform.position = new Vector3(following.position.x, following.position.y, transform.position.z);
        }
        
    }

    // screen shake (by Brackeys)
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCamera.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.localPosition = new Vector3(x, y, mainCamera.localPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.localPosition = originalPos;
    }

    // stop following the object
    public void stopFollow()
    {
        following = null;
    }
}
