using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WatchController : MonoBehaviour {

    private Transform cameraTransform;
    private TextMeshPro scoreText;
    
    // Start is called before the first frame update
    void Start() {
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
        // scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraTransform);
    }
}
