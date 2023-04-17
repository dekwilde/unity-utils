/**
    SceneLoader.cs
    Call it from a MonoBehaviour 
        `SceneLoader.instance.LoadScene(name or id)` to load another scene
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader {

     private static SceneLoader instance; 
     public static SceneLoader getInstance() {
          if (instance == null) {
               instance = new SceneLoader();
          }
          return instance;
     }

     public void LoadScene(string sceneName) {
          SceneManager.LoadScene(sceneName);
     }

}