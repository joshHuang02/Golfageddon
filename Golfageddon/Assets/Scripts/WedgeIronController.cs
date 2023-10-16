using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class WedgeIronController : MonoBehaviour {

    public GameObject attachPoint;
    public GameObject baseOfShaft;

    public float clubDownAdjust;

    private bool grabbed;
    private GameObject wedge;
    private GameObject shaftIron;
    private GameObject gripRubber;


    private float maxRange;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start() {
        wedge = transform.parent.gameObject;
        shaftIron = GameObject.Find("Shaft_Iron - Polished_0");
        gripRubber = GameObject.Find("Grip_Rubber - Weathered_0");
        maxRange = Vector3.Distance(transform.position, attachPoint.transform.position);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ResetPos();
        }
        // transform.position = club.transform.position;
        // Debug.Log(pos);
        if (grabbed) {
            int layerMask = 1 << 8;

            var directionOfClub = (baseOfShaft.transform.position - attachPoint.transform.position).normalized;
            if (Physics.Raycast(attachPoint.transform.position, directionOfClub, out hit, maxRange - clubDownAdjust, layerMask)) {
                // Debug.Log("Did Hit: " + hit.point);
                // var wedgePos = wedge.transform.position;
                // var basePos = baseOfShaft.transform.position;
                transform.position = hit.point + (wedge.transform.position - baseOfShaft.transform.position) + directionOfClub * clubDownAdjust;
            } else {
                transform.position = wedge.transform.position;
                // Debug.DrawRay(attachPoint.transform.position, (wedge.transform.position - attachPoint.transform.position) * 1000, Color.white);
                // Debug.Log("Did not Hit");
            }
        }
    }

    public void Grabbed() {
        gameObject.GetComponent<WedgeHandController>().SetAlpha(1f);
        shaftIron.GetComponent<WedgeHandController>().SetAlpha(1f);
        gripRubber.GetComponent<WedgeHandController>().SetAlpha(1f);
        gameObject.GetComponent<WedgeHandController>().SetCollision(true);
        shaftIron.GetComponent<WedgeHandController>().SetCollision(true);
        gripRubber.GetComponent<WedgeHandController>().SetCollision(true);
        grabbed = true;
        Debug.Log("Grab");
    }

    public void Dropped() {
        gameObject.GetComponent<WedgeHandController>().SetAlpha(.3f);
        shaftIron.GetComponent<WedgeHandController>().SetAlpha(.3f);
        gripRubber.GetComponent<WedgeHandController>().SetAlpha(.3f);
        gameObject.GetComponent<WedgeHandController>().SetCollision(false);
        shaftIron.GetComponent<WedgeHandController>().SetCollision(false);
        gripRubber.GetComponent<WedgeHandController>().SetCollision(false);
        grabbed = false;
        ResetPos();
        Debug.Log("No Grab");
    }

    private void ResetPos() {
        transform.position = wedge.transform.position;
    }
}
