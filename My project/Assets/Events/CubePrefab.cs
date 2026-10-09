using UnityEngine;
using UnityEngine.Events;
public class CubePrefab : MonoBehaviour
{

    private CubeDestroyTrigger trigger;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Find the trigger game object and then subscribe to the unity event.
        trigger = GameObject.Find("CubeDestroyTrigger").GetComponent<CubeDestroyTrigger>();
        trigger.OnTouch.AddListener(OnDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //Unsubscribe to the function in the CubeDestroyTrigger script.
        trigger.OnTouch.RemoveListener(OnDestroy);

        //Destroy the cube.
        Destroy(this.gameObject);
    }
}
