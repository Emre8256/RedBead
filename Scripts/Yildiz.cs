using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Yildiz : MonoBehaviour
{



    public void Yildizyerlestir(string Levelbutonu, string Tag, int yildizSayisi)
    {
        // Belirtilen tag'e sahip olan parent objeyi bul
        GameObject parentObject = GameObject.FindGameObjectWithTag(Levelbutonu);

        // Parent obje bulunamadýysa uyarý ver ve iþlemi sonlandýr
        if (parentObject == null)
        {
            Debug.LogWarning("Parent object with tag " + Levelbutonu + " not found!");
            return;
        }

        int activatedChildrenCount = 0;

        // Parent objenin altýndaki tüm child objeleri al
        foreach (Transform child in parentObject.transform)
        {
            // Eðer alt obje belirtilen tag'e sahipse ve maksimum sayýya ulaþýlmadýysa
            if (child.CompareTag(Tag) && activatedChildrenCount < yildizSayisi)
            {
                // Alt objeyi etkinleþtir
                child.gameObject.SetActive(true);
                activatedChildrenCount++;
            }
        }
    }

    void Start()
    {
        int Level1yildiz = PlayerPrefs.GetInt("Level1", 0);
        Yildizyerlestir("Level1", "Yildizlar", Level1yildiz);
        int Level2yildiz = PlayerPrefs.GetInt("Level2", 0);
        Yildizyerlestir("Level2", "Yildizlar", Level2yildiz);
        int Level3yildiz = PlayerPrefs.GetInt("Level3", 0);
        Yildizyerlestir("Level3", "Yildizlar", Level3yildiz);
        int Level4yildiz = PlayerPrefs.GetInt("Level4", 0);
        Yildizyerlestir("Level4", "Yildizlar", Level4yildiz);
        int Level5yildiz = PlayerPrefs.GetInt("Level5", 0);
        Yildizyerlestir("Level5", "Yildizlar", Level5yildiz);
        int Level6yildiz = PlayerPrefs.GetInt("Level6", 0);
        Yildizyerlestir("Level6", "Yildizlar", Level6yildiz);
        int Level7yildiz = PlayerPrefs.GetInt("Level7", 0);
        Yildizyerlestir("Level7", "Yildizlar", Level7yildiz);
        int Level8yildiz = PlayerPrefs.GetInt("Level8", 0);
        Yildizyerlestir("Level8", "Yildizlar", Level8yildiz);
        int Level9yildiz = PlayerPrefs.GetInt("Level9", 0);
        Yildizyerlestir("Level9", "Yildizlar", Level9yildiz);
        int Level10yildiz = PlayerPrefs.GetInt("Level10", 0);
        Yildizyerlestir("Level10", "Yildizlar", Level10yildiz);
        int Level11yildiz = PlayerPrefs.GetInt("Level11", 0);
        Yildizyerlestir("Level11", "Yildizlar", Level11yildiz);
        int Level12yildiz = PlayerPrefs.GetInt("Level12", 0);
        Yildizyerlestir("Level12", "Yildizlar", Level12yildiz);

    }

    // Update is called once per frame
     public void prefSifirla()
    
       { PlayerPrefs.DeleteAll();}

    
}
