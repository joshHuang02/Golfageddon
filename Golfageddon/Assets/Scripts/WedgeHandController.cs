using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeHandController : MonoBehaviour {

    private Material mat;

    private Collider col;
    // Start is called before the first frame update
    void Start() {
        mat = GetComponent<Renderer>().material;
        col = GetComponent<MeshCollider>();
        col.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            SetCollision(true);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            SetCollision(false);
        }
    }

    public void SetAlpha(float alpha) {
        Color color = mat.color;
        color.a = alpha;
        mat.color = color;
    }

    public void SetCollision(bool doCollide) {
        col.isTrigger = !doCollide;
        // Debug.Log("Change collision: " + doCollide);
    }
}
