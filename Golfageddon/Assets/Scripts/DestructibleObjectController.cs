using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectControler : MonoBehaviour {
    private Rigidbody rb;
    // private MeshCollider mc;

    private bool destroyObject;
    private bool gotHit;
    private Vector3 spawnPos;
    private float spawnRotX;
    private float spawnRotY;
    private float spawnRotZ;

    private GameManager gameManager;
    [SerializeField] private float maxVelocity = 5;
    [SerializeField] private int points = 1;
    private const float FadeDuration = 10f;
    private const float MoveLimit = 0.5f;
    
    // Start is called before the first frame update
    void Start() {
        gameManager = GameManager.Instance;

        spawnPos = transform.position;
        var rot = transform.rotation;
        spawnRotX = rot.eulerAngles.x;
        spawnRotY = rot.eulerAngles.y;
        spawnRotZ = rot.eulerAngles.z;
        
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = 2f;
        rb.drag = 2f;
        rb.angularDrag = 2f;
        
        // mc = gameObject.AddComponent<MeshCollider>();
        // mc.convex = true;
        
        //gather child  objects
        foreach (Transform child in transform) {
            // add destructible script
            if (child.gameObject.name.Contains("Floor") || child.gameObject.layer == 8) {
                
            } else if (child.gameObject.name.Contains("ceiling")) {

            } else { // all destructible objects
                child.gameObject.AddComponent<DestructibleObjectControler>();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        var tr = transform;
        var rot = tr.rotation;
        bool moved = Vector3.Distance(spawnPos, tr.position) > MoveLimit;
        bool rotated = Math.Abs(rot.eulerAngles.x - spawnRotX) > 10 ||
                       Math.Abs(rot.eulerAngles.y - spawnRotY) > 10 ||
                       Math.Abs(rot.eulerAngles.z - spawnRotZ) > 10;
        if (!destroyObject && (moved || rotated)) {
            gameManager.AddPoints(points);
            destroyObject = true;
            StartCoroutine(FadeOut());
        }
        
    }

    private void OnCollisionEnter(Collision collision) {
        var colObj = collision.gameObject;
        if (colObj.layer == 6) { // collide object is ball layer
            // Debug.Log("Hit something");
            EnableRb();
            return;
        }

        if (gotHit && !colObj.name.Contains("Floor") && !colObj.name.Contains("ceiling") && !(colObj.layer == 8)) { // get other objects in contact
            var objectController = colObj.GetComponent<DestructibleObjectControler>();
            if (null != objectController) {
                objectController.EnableRb();
            }    
        }
    }

    public void EnableRb() {
        rb.constraints = RigidbodyConstraints.None;
        gotHit = true;
        // StartCoroutine(FadeOut());
        // rb.velocity = Vector3.zero;
    }

    IEnumerator FadeOut() {
        
        for (float t = 0f; t < FadeDuration; t += Time.deltaTime) {
            // Color color = gameObject.GetComponent<MeshRenderer>().material.color;
            // color.a -= t;
            // gameObject.GetComponent<MeshRenderer>().material.color = color;
            yield return null;
        }
        Destroy(gameObject);
    }
}
