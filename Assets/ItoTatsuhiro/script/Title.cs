using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Title->Stage");
            SceneManager.LoadScene("Stage");
        }
    }
}
