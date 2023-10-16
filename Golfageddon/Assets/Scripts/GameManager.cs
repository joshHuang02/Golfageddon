using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int score;
    public GameObject ballPrefab;
    private GameObject wedge;
    private GameObject targeter;

    public TMP_Text scoreText;
    public GameObject tutorial;
    private bool tutorialTriggered;
    private bool tutorialOpen;
    private GameObject tee;
    private XRRayInteractor targeterRay;
    private GameObject leftHandObj;
    private InputDevice rightHand;
    private InputDevice leftHand;
    private bool triggerPressed;
    private bool ballMode;
    private int ballType;
    private Vector3 ballPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager is null.");
            }
            return instance;
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = "Score: " + score + " pts";
    }

    private void Start() {
        wedge = GameObject.FindGameObjectWithTag("Wedge");
        targeter = GameObject.FindGameObjectWithTag("Targeter");
        targeterRay = targeter.GetComponent(typeof(XRRayInteractor)) as XRRayInteractor;
        leftHandObj = GameObject.FindGameObjectWithTag("LeftHand");
        tee = GameObject.FindGameObjectWithTag("tee");
        ballPos = new Vector3(-2.33669996f, 0.213f, -0.777899981f);
        //ballPos = tee.transform.position + Vector3.up * 0.2f; 
        targeter.SetActive(false);
        ballMode = false;
        scoreText.text = "Score: " + score + " pts";
        Invoke(nameof(GetDevices), 1f);

        tutorial.SetActive(false);
    }

    private void Update() {
        bool triggerValue;
        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            // Debug.Log("Trigger button is pressed");
            if (!triggerPressed) {
                StartCoroutine(SpawnBall());
            }
        }

        if (leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            if (ballMode)
            {
                StartCoroutine(placeBall());
            }
        }

        if (leftHand.TryGetFeatureValue(CommonUsages.gripButton, out triggerValue) && triggerValue)
        {
            if (!ballMode)
            {
                StartCoroutine(SpawnBallMode());    
            }
        }

        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out triggerValue) && triggerValue) {
            if (!tutorialTriggered) {
                StartCoroutine(TriggerTutorial());
            }
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            GetDevices();
        }
    }

    private void GetDevices() {
        // referencing this site https://docs.unity3d.com/2019.1/Documentation/Manual/xr_input.html
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);
        
        foreach (var device in inputDevices) {
            if (device.name == "Oculus Touch Controller - Right") rightHand = device;
            if (device.name == "Oculus Touch Controller - Left") leftHand = device;
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }
        // Debug.Log(rightHand.name);
        // Debug.Log(leftHand.name);
    }

    private IEnumerator SpawnBall() {
        triggerPressed = true;
        
        Instantiate(ballPrefab, wedge.transform.position + Vector3.down * 0.1f, Quaternion.identity);
        yield return new WaitForSeconds(1);
        triggerPressed = false;
        yield return null;
    }
    
    private IEnumerator TriggerTutorial() {
        tutorialTriggered = true;
        
        tutorialOpen = !tutorialOpen;
        tutorial.SetActive(tutorialOpen);
        // Instantiate(ballPrefab, wedge.transform.position + Vector3.down * 0.1f, Quaternion.identity);
        yield return new WaitForSeconds(1);
        tutorialTriggered = false;
        yield return null;
    }

    private IEnumerator SpawnBallMode() {
        targeter.SetActive(true);
        ballMode = true;
        yield return null;
    }

    private IEnumerator placeBall() {
        targeter.SetActive(false);
        ballMode = false;
        RaycastHit raycastHit;
        targeterRay.TryGetCurrent3DRaycastHit(out raycastHit);
        GameObject hit = raycastHit.collider.gameObject;
        if (hit.tag == "spwnbtn")
        {
            SpawnerButton btn = hit.GetComponent(typeof(SpawnerButton)) as SpawnerButton;
            Instantiate(btn.storedBall, ballPos, Quaternion.identity);
        }
        yield return null;
    }
    
}
