using System.Collections;
using UnityEngine;

public class Planta : MonoBehaviour
{
    private int stage = 0;
    void Start()
    {
        StartCoroutine(timerStage());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private IEnumerator timerStage()
    {
        while (stage < transform.childCount - 1)
        {
            yield return new WaitForSeconds(5f);
            updateStage();
        }
    }


    private void updateStage()
    {
        transform.GetChild(stage).gameObject.SetActive(false);
        stage++;
        transform.GetChild(stage).gameObject.SetActive(true);
    }
}
