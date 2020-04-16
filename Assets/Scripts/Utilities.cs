using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities
{
    public static int playerDeath = 0;

    /// <summary>
    /// Restart level
    /// </summary>
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Restart level from a buildindex
    /// </summary>
    /// <param name="buildIndex"></param>
    public static void RestartLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        Time.timeScale = 1.0f;
    }
}
