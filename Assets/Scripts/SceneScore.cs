using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScore :  MonoBehaviour
{

    public int sceneIndex;
    public float sceneTime;

    public SceneScore(int index, float time)
    {
        sceneIndex = index;
        sceneTime = time;
    }

}
