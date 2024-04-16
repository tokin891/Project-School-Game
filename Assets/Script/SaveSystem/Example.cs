using UnityEngine;

public class Example : MonoBehaviour {
    void Start() {	
        SaveSystem.SaveValue("Name", "aha");
        string playerName = SaveSystem.LoadValue("Name");
        Debug.Log("Player Name: " + playerName);
		Debug.Log("Saved to: " + Application.persistentDataPath);
    }
}