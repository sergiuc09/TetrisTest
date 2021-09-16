using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void Load(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

public enum Scenes
{
    Menu, Game
}
