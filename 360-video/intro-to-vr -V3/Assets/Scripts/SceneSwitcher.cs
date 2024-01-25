using UnityEngine;
using UnityEngine.SceneManagement;
//My Directives above ^

public class SceneSwitcher : MonoBehaviour
{
    // Method to load the next scene based on the current scene's build index
    public void LoadNextScene()
    {
        // Get the build index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the next scene's index by adding 1 to the current index
        // The modulo operation ensures that the index loops back to 0 if it exceeds the total number of scenes !
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the scene at the calculated next scene index
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Method to load the previous scene based on the current scene's build index
    public void LoadPreviousScene()
    {
        // Get the build index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the previous scene's index by subtracting 1 from the current index
        int previousSceneIndex = currentSceneIndex - 1;

        // Check if the calculated previous scene index is valid (not less than 0)
        if (previousSceneIndex >= 0)
        {
            // Load the scene at the calculated previous scene index
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            // Log an error if there is no valid prev scene (if the current scene is the first scene)
            Debug.LogError("No previous scene found. Current scene is the first one.");
        }
    }

     public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Method to load a specific scene by name
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
