using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives;

    public Text livesText;

    public Vector3 respawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = PlayerController.instance.transform.position;

        #region Savegame
        if (SaveManager.instance.hasLoaded)
        {
            respawnPoint = SaveManager.instance.activeSave.respawnPosition;
            PlayerController.instance.transform.position = respawnPoint;

            lives = SaveManager.instance.activeSave.lives;
        }
        else
        {
            SaveManager.instance.activeSave.lives = lives;
        }
        #endregion

        livesText.text = "x " + lives;
    }
    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        lives--;
        livesText.text = "x " + lives;

        #region Savegame
        SaveManager.instance.activeSave.lives = lives;

        SaveManager.instance.Save();
        #endregion

        yield return new WaitForSeconds(.5f);

        #region Savegame
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        #endregion
    }
}
