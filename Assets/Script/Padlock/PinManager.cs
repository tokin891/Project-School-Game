using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PinManager : MonoBehaviour{
	
    public List<GameObject> pinObjects = new List<GameObject>();
    public GameObject doorObject;
    public int correctPin = 12;
    public int maxPinLength = 2;
    private bool isPlayingRotationSound = false;
    private List<string> enteredPins = new List<string>();
    private int currentPinIndex = 0;
    public float rotationSpeed = 50.0f;
    public float glowIntensity = -4f;
    private List<Material> originalMaterials = new List<Material>();
    private bool glowEffectApplied = false;
    public AudioSource audioSource;
    public AudioClip RotationSoundClip;
    public AudioClip unlockSoundClip;
    public delegate void CorrectPinEnteredAction();
    public static event CorrectPinEnteredAction OnCorrectPinEntered;

    void Start(){
        Door doorScript = doorObject.GetComponent<Door>();
        if (doorScript != null)
        {
            doorScript.enabled = false;
        }

        foreach (GameObject pinObject in pinObjects){
            Renderer renderer = pinObject.GetComponent<Renderer>();
            if (renderer != null){
                originalMaterials.Add(renderer.material);
            }
        }

        ApplyGlowEffect(pinObjects[currentPinIndex], glowIntensity);
    }

    void ApplyGlowEffect(GameObject pinObject, float intensity){
        Renderer renderer = pinObject.GetComponent<Renderer>();

        if (renderer != null){
            Material material = renderer.material;

            if (intensity >= 0f && intensity <= 1f){
                Color emissionColor = new Color(1.0f, 0.92f, 0.016f) * intensity;
                material.SetColor("_EmissionColor", emissionColor);
                material.EnableKeyword("_EMISSION");
            }
            else{
                float defaultIntensity = 0.1f;
                Color defaultEmissionColor = new Color(1.0f, 0.92f, 0.016f) * defaultIntensity;
                material.SetColor("_EmissionColor", defaultEmissionColor);
                material.EnableKeyword("_EMISSION");
            }
        }
    }

    void ResetGlowEffect(GameObject pinObject){
        Renderer renderer = pinObject.GetComponent<Renderer>();
        if (renderer != null){
            Material originalMaterial = originalMaterials[pinObjects.IndexOf(pinObject)];
            originalMaterial.SetColor("_EmissionColor", Color.black);
            originalMaterial.DisableKeyword("_EMISSION");
            renderer.material = originalMaterial;
        }
    }

    void PlayRotationSound(){
        if (audioSource != null && RotationSoundClip != null && !isPlayingRotationSound){
            audioSource.PlayOneShot(RotationSoundClip);
            isPlayingRotationSound = true;
            Invoke("StopRotationSound", 1.5f);
        }
    }

    void StopRotationSound(){
        isPlayingRotationSound = false;
        audioSource.Stop();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.W)){
            ResetGlowEffect(pinObjects[currentPinIndex]);
            currentPinIndex = (currentPinIndex - 1 + pinObjects.Count) % pinObjects.Count;
            glowEffectApplied = false;
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            ResetGlowEffect(pinObjects[currentPinIndex]);
            currentPinIndex = (currentPinIndex + 1) % pinObjects.Count;
            glowEffectApplied = false;
        }

        GameObject currentPinObject = pinObjects[currentPinIndex];
        Transform pinTransform = currentPinObject.transform;

        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.A)){
            rotationInput = -1f;
            PlayRotationSound();
        }
        else if (Input.GetKey(KeyCode.D)){
            rotationInput = 1f;
            PlayRotationSound();
        }

        float newZRotation = pinTransform.eulerAngles.z + rotationInput * rotationSpeed * Time.deltaTime;
        pinTransform.eulerAngles = new Vector3(pinTransform.eulerAngles.x, pinTransform.eulerAngles.y, newZRotation);
        int outputValue = Mathf.RoundToInt(newZRotation / 36f) % 10;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)){
            if (currentPinIndex < enteredPins.Count){
                enteredPins[currentPinIndex] = outputValue.ToString();
            }
            else{
                enteredPins.Add(outputValue.ToString());
            }

            if (enteredPins.Count > maxPinLength){
                enteredPins.RemoveAt(0);
            }

            if (!glowEffectApplied){
                ApplyGlowEffect(currentPinObject, glowIntensity);
                glowEffectApplied = true;
            }
        }

        string enteredPin = string.Join("", enteredPins);
        if (enteredPin.Length == maxPinLength && int.Parse(enteredPin) == correctPin){
            Door doorScript = doorObject.GetComponent<Door>();
            if (doorScript != null){
                doorScript.enabled = true;
                enabled = false;
            }

            if (OnCorrectPinEntered != null){
                OnCorrectPinEntered();
            }

            if (audioSource != null && unlockSoundClip != null){
                audioSource.PlayOneShot(unlockSoundClip);
            }

            Rigidbody selfRigidbody = gameObject.AddComponent<Rigidbody>();
            StartCoroutine(DisableRigidbodyAfterDelay(selfRigidbody, 2.0f));
        }
    }

    private IEnumerator DisableRigidbodyAfterDelay(Rigidbody rb, float delay){
        yield return new WaitForSeconds(delay);
        if (rb != null){
            Destroy(rb);
			GetComponent<BoxCollider>().enabled = false;
        }
    }
}