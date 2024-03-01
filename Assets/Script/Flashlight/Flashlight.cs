using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour{
    [SerializeField] private Light flashlight;       
    private bool isFlashlightOn;  
    private bool canToggle = true; 
    private float smoothTime = 0.5f; 
	
    private void Start(){
        isFlashlightOn = false;

        if (flashlight != null){
            flashlight.enabled = false;
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

    public void ToggleFlashlight(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        if (canToggle)
        {
            ToggleFlashlight();
            StartCoroutine(ToggleCooldown());
        }
    }
}