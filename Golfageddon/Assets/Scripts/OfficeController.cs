using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfficeController : MonoBehaviour {
    private List<GameObject> ceilingObjects = new List<GameObject>();

    private List<GameObject> groundedObjects = new List<GameObject>();

    private GameObject floor;
    private GameObject ceiling;
    // Start is called before the first frame update
    void Start()
    {
        //gather scene objects
        foreach (Transform child in transform) {
            // add destructible script
            if (child.gameObject.name.Contains("Floor") || child.gameObject.layer == 8) {
                
            } else if (child.gameObject.name.Contains("ceiling")) {

            } else { // all destructible objects
                child.gameObject.AddComponent<DestructibleObjectControler>();
            }
        }
        
        // TurnOnRigidBody();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void TurnOnRigidBody() {
    //     foreach (var ceilingLight in ceilingObjects) {
    //         var ceilingLightRb = ceilingLight.AddComponent<Rigidbody>();
    //         ceilingLightRb.constraints = RigidbodyConstraints.FreezeAll;
    //         ceilingLight.AddComponent<DestructibleObjectControler>();
    //         Debug.Log(ceilingLight.name);
    //     }
    //     foreach (GameObject groundedObject in groundedObjects) {
    //         var objRb = groundedObject.AddComponent<Rigidbody>();
    //         groundedObject.AddComponent<DestructibleObjectControler>();
    //         objRb.constraints = RigidbodyConstraints.FreezeAll;
    //     }
    // }
}
