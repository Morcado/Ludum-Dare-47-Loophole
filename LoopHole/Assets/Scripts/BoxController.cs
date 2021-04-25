using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour{
  // Start is called before the first frame update
    private GameObject target = null;
    [SerializeField] private Image image;
    [SerializeField] private Sprite disabled;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            image.sprite = disabled;
            Destroy(target);
        }
    }
}
