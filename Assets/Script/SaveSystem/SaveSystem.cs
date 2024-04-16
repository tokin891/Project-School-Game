using UnityEngine;
using System.IO;

public static class SaveSystem {

    private static string filePath = Application.persistentDataPath + "/save.txt";

    public static void SaveValue(string key, string value) {
        try {
			if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
            }
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i].Trim(); 

                if (string.IsNullOrEmpty(line) || line.StartsWith("//")) {
                    continue;
                }

                string[] keyValue = line.Split(':');

                if (keyValue.Length == 2 && keyValue[0].Trim() == key) {
                    lines[i] = key + ":" + value; 
                    File.WriteAllLines(filePath, lines); 
                    return;
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath, true)) {
                writer.WriteLine(key + ":" + value);
            }
        } catch (IOException ex) {
            Debug.LogError("Error saving value: " + ex.Message);
        }
    }

    public static string LoadValue(string key){
        try {
            if (!File.Exists(filePath)){
                Debug.LogWarning("Save file not found");
                return null;
            }

            using (StreamReader reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null){
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line) || line.StartsWith("//")) {
                        continue;
                    }

                    string[] keyValue = line.Split(':');
                    
                    if (keyValue.Length == 2 && keyValue[0].Trim() == key) {
                        return keyValue[1];
                    }
                }
            }
            Debug.LogWarning("Key not found");
            return null;
        } catch (IOException ex) {
            Debug.LogError("Error loading value: " + ex.Message);
            return null;
        }
    }
}
