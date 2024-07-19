using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.VFX;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System;


public class GameManager : MonoBehaviour
{
    //
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;
    [SerializeField]
    private GameObject leftstaron, midstaron, rightstaron;
    [SerializeField] private GameObject Sonucyildiz1, Sonucyildiz2, Sonucyildiz3;
    private int puskurtmekontrol = 1;
    public Slider slider;
    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    Line currentLine;
    Camera cam;
    Vector3 previousMousePosition;

    public bool GeriSayimBittiMi = false;
    //


    [SerializeField] private GameObject shuriken;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private float sliderAzalmadegeri = 0.7f;
    [SerializeField] private Image oyunsonugerisayimImg;
    [SerializeField] private Text oyunsonuGerisayimtxt;
    [SerializeField] private GameObject sonucEkrani;
    [SerializeField] private GameObject deadimage;

    int toplananYildiz ;

    


    public float fireAngle = 30f;
    private float fireForce = 0.5f;
    public float duration = 5f; // Fonksiyonun çalýþma süresi




    private void Start()
    {
        //
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        previousMousePosition = Input.mousePosition; // Baþlangýçta fare pozisyonunu kaydet
                                                     //

    }

    void puskurmeyeBasla()
    {
        if (puskurtmekontrol == 0)
        {

            InvokeRepeating("ThrowShurikens", 0f, 0.05f); // Ýlk ateþleme anýndan itibaren belirli aralýklarla FireBullets metodu çaðýrýlacak
            Invoke("StopFiringBullets", duration); // Belirli bir süre sonra StopFiringBullets metodunu çaðýr
            if (!deadimage.activeSelf)
            {
               Invoke("OyunSonuGeriSayim", duration + 1f); // StopFiringBullets çaðrýldýktan 1 saniye sonra BaskaFonksiyon metodunu çaðýr
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();
        if (currentLine != null)
            Draw();
        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();

        }
        Vector3 currentMousePosition = Input.mousePosition;
        if (currentLine != null && currentMousePosition != previousMousePosition)
            slider.value -= sliderAzalmadegeri;

        // Fare pozisyonunu güncelle
        previousMousePosition = currentMousePosition;

        starControl();


    }


    public void ThrowShurikens()
    {

        float baseAngle = -90f; // Silahýn yukarýdan aþaðýya baktýðý açý
        float randomAngle = UnityEngine.Random.Range(-fireAngle, fireAngle); // Negatif ve pozitif açý aralýðýnda rastgele bir açý seçin
        float finalAngle = baseAngle + randomAngle; // Rastgele sapmayý silahýn bakýþ açýsýna ekleyin

        Quaternion rotation = Quaternion.Euler(0, 0, finalAngle);
        GameObject bullet = Instantiate(shuriken, spawnpoint.transform.position, rotation);
       // GameObject bullet = Instantiate(shuriken, spawnpoint.transform.position, rotation, transform.parent);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.right;
            rb.AddForce(direction * fireForce, ForceMode2D.Impulse);
        }
    }

    void StopFiringBullets()
    {
        CancelInvoke("ThrowShurikens"); // FireBullets metodunun tekrar çaðrýlmasýný durdur
    }

    void BeginDraw()
    {

        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);

    }

    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit)
            EndDraw();
        else
        {
            currentLine.AddPoint(mousePosition);
            ToggleAllCollidersTriggers(currentLine.gameObject, true);
        }
    }

    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);

            }
            else
            {
                ToggleAllCollidersTriggers(currentLine.gameObject, false);
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
                puskurtmekontrol--;
                puskurmeyeBasla();
            }
        }

    }

    void starControl()
    {
        if (slider.value > 200)
        {
             toplananYildiz = 3;
        }

        else if (slider.value < 200 && slider.value >100)
        {
            rightstaron.SetActive(false);
            toplananYildiz = 2;
        }

        else if (slider.value < 100)
        {
            midstaron.SetActive(false);
            toplananYildiz = 1;
        }

        
    }


    void ToggleAllCollidersTriggers(GameObject obj, bool isTrigger)
    {
        
        Collider2D[] colliders = obj.GetComponentsInChildren<Collider2D>();

        
        foreach (Collider2D collider in colliders)
        {
            
            collider.isTrigger = isTrigger;
        }
    }

    void OyunSonuGeriSayim()
    {
        oyunsonugerisayimImg.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        StartCoroutine(StartCountdown());

    }

    IEnumerator StartCountdown()
    {
        int count = 3;
        while (count >= 0)
        {
            oyunsonuGerisayimtxt.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }


        GeriSayimBittiMi = true;
        //buaraya
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Shuriken");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        GameObject[] objectsToDestroy1 = GameObject.FindGameObjectsWithTag("Line");
        foreach (GameObject obj in objectsToDestroy1)
        {
            Destroy(obj);
        }

        sonucEkrani.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        sonucEkrani.GetComponent<CanvasGroup>().blocksRaycasts = true;
        int allLayersMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "CantDrawOver");
        cantDrawOverLayer = allLayersMask;
        Invoke("YildizYerlestir", 1f);





    }

    void YildizYerlestir()
    {
        StartCoroutine(OyunSonuStarControl());
    }


    /// <summary>
    /// /////////////////////
    /// </summary>
    /// <returns></returns>

    IEnumerator OyunSonuStarControl()
    {
        string aktifSahneismi = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(aktifSahneismi, toplananYildiz);
        int oncekiYildizlar = PlayerPrefs.GetInt(aktifSahneismi, 0);
        int yeniYildizlar = Mathf.Max(toplananYildiz, oncekiYildizlar);
        PlayerPrefs.SetInt(aktifSahneismi, yeniYildizlar);



        Sonucyildiz1.GetComponent<CanvasGroup>().DOFade(1f, 1f);
        yield return wait1second();

        if (toplananYildiz == 2)
        {
            Sonucyildiz2.GetComponent<CanvasGroup>().DOFade(1f, 1f);
            yield return wait1second();
        }

        else if (toplananYildiz == 3)
        {
            Sonucyildiz2.GetComponent<CanvasGroup>().DOFade(1f, 1f);
            yield return wait1second();
            Sonucyildiz3.GetComponent<CanvasGroup>().DOFade(1f, 1f);
            yield return wait1second();
        }

        
        
    }


    IEnumerator wait1second()
    {
        yield return new WaitForSeconds(1);
    }
}
