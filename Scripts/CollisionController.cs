using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    [SerializeField]
    private GameObject DeadImage;
    GameManager gameManager;

     void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.GeriSayimBittiMi == false)
        {
            if (collision.gameObject.CompareTag("Shuriken"))
            {
                DeadImage.SetActive(true);
                Invoke("YenidenOyna", 2f);


            }

            if (collision.gameObject.CompareTag("FallArea"))
            {
                YenidenOyna();
            }
        }
    }

   





    public void YenidenOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
