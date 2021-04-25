using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private GameObject[] platformGroup1 = null;
    private GameObject[] platformGroup2 = null;
    private float v_speed;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        v_speed = 0.2f;
        platformGroup1 = GameObject.FindGameObjectsWithTag("Barrier");
        platformGroup2 = GameObject.FindGameObjectsWithTag("Barrier2");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)(timer % 60);
        //Debug.Log(platform1.gameObject.name);
        // vertical movement
        if (seconds < 5) {
            for (int i = 0; i < platformGroup1.Length; i++)
            {
                platformGroup1[i].transform.Translate(Vector3.up * 10 * Time.deltaTime);
            }
            for (int i = 0; i < platformGroup2.Length; i++)
            {
                platformGroup2[i].transform.Translate(Vector3.down * 10 * Time.deltaTime);
            }
        } else if (seconds < 10) {
            for (int i = 0; i < platformGroup1.Length; i++)
            {
                platformGroup1[i].transform.Translate(Vector3.down * 10 * Time.deltaTime);
            }
            for (int i = 0; i < platformGroup2.Length; i++)
            {
                platformGroup2[i].transform.Translate(Vector3.up * 10 * Time.deltaTime);
            }
        } else{
            timer = 0f;
        }
    }
}
