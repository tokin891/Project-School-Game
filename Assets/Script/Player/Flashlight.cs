using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteract{
    public Transform playerHand; 
    public Light flashlight;    
    private bool isPickedUp;      
    private bool isFlashlightOn;  
    private bool canToggle = true; 
    private float smoothTime = 0.5f; 
	
    private void Start(){
        isPickedUp = false;
        isFlashlightOn = false;

        if (flashlight != null){
            flashlight.enabled = false;
        }
    }

    public void CameraInteractWithObject(){
        if (playerHand != null){
            StartCoroutine(MoveToPlayerHandWithSlowRotation());
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            isPickedUp = true; 
        }
        else
        {
            Debug.LogWarning("Attach hand");
        }
    }

private IEnumerator MoveToPlayerHandWithSlowRotation(){
    float elapsedTime = 0f;
    float duration = 1.0f; 

    Vector3 initialPosition = transform.position;
    Quaternion initialRotation = transform.rotation;

    while (elapsedTime < duration){
        float t = elapsedTime / duration;
        transform.position = Vector3.Lerp(initialPosition, playerHand.position, t);
        transform.rotation = Quaternion.Slerp(initialRotation, playerHand.rotation, t);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    transform.position = playerHand.position;
    transform.rotation = playerHand.rotation;
   
    transform.SetParent(playerHand);
}


    void Update(){
        if (isPickedUp){
            if (Input.GetKeyDown(KeyCode.F) && canToggle){
                ToggleFlashlight();
                StartCoroutine(ToggleCooldown());
            }
        }
    }

    private void ToggleFlashlight(){
        isFlashlightOn = !isFlashlightOn;
		
        if (isFlashlightOn){
            flashlight.enabled = true;
        }

        StartCoroutine(LerpIntensity(isFlashlightOn ? 8.0f : 0.0f)); 

        if (!isFlashlightOn){
            StartCoroutine(DisableLightComponentAfterLerp());
        }
    }

    private IEnumerator LerpIntensity(float targetIntensity){
        float currentIntensity = flashlight.intensity;
        float elapsedTime = 0f;

        while (elapsedTime < smoothTime){
            flashlight.intensity = Mathf.Lerp(currentIntensity, targetIntensity, elapsedTime / smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashlight.intensity = targetIntensity;
    }

    private IEnumerator DisableLightComponentAfterLerp(){
        yield return new WaitForSeconds(smoothTime);
        flashlight.enabled = false;
    }

    private IEnumerator ToggleCooldown(){
        canToggle = false;
        yield return new WaitForSeconds(1f);
        canToggle = true;
    }
}