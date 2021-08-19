using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        LevelSelectPlatform platfm = collision.gameObject.GetComponent<LevelSelectPlatform>();
        if (platfm)
        {
            if (platfm.Unblocked()) { SceneManager.LoadScene(platfm.GetLevel());  }
        }
    }
}
