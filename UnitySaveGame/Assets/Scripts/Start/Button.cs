using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isOn;

    [Header("Stuff to control")]
    public Animator anim;
    public GameObject theBoxes;
    public GameObject[] debris;
    public int maxDebris = 10;

    // Start is called before the first frame update
    void Start()
    {
        #region Savegame
        if (SaveManager.instance.hasLoaded)
        {
            isOn = SaveManager.instance.activeSave.doorOpen;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        theBoxes.gameObject.SetActive(!isOn);

        anim.SetBool("buttonOn", isOn);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isOn = !isOn;

            if(isOn)
            {
                int debrisToSpawn = Random.Range(5, maxDebris);
                for(int i = 0; i < debrisToSpawn; i++)
                {
                    Instantiate(debris[Random.Range(0, debris.Length)], theBoxes.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation);
                }
            }

            #region Savegame
            SaveManager.instance.activeSave.doorOpen = isOn;
            #endregion
        }
    }
}
