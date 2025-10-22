using System.Collections;
using UnityEngine;

public class Planta : MonoBehaviour
{
    public int stage = 0;
    void Start()
    {
        StartCoroutine(timerStage());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void firstStage()
    {
        for(int i = 1; i <  transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);

        stage = 0;

        StartCoroutine(timerStage());
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
