using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayedScene : MonoBehaviour
{
    public List<SceneScore> sceneList;

    public PlayedScene()
    {
        sceneList = new List<SceneScore>();
    }
}
