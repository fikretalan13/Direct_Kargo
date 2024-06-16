using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("---Koli Yönetimi---")]
    [SerializeField] List<GameObject> koliHavuzu;

    [SerializeField] Transform koliCikisNoktasi;

    int koliHavuzIndex;

    public float cikisZamani;

    [SerializeField]
    CikisBanti[] cikisBanti;

    [SerializeField]
    TasimaBandi[] tasimaBantlari;

    CikisBanti aktifBant;

    int dogruKoliSayisi;

    [Header("---Ses Yönetimi---")]
    [SerializeField] AudioSource[] sesler;
    [SerializeField]
    Image[] sesAyarButonlari;

    [SerializeField]
    Sprite[] spriteObjeleri;


    [Header("---UI Yönetimi---")]

    [SerializeField]
    GameObject[] paneller;
    [SerializeField] TextMeshProUGUI[] skorTextleri;

    int sahneIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(Instance);
    }


    private void Start()
    {
        sahneIndex = SceneManager.GetActiveScene().buildIndex;
        aktifBant = cikisBanti[0];
        SahneIlkIslemler();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(!UIObjesiMi())
                {
                    if (Physics.Raycast(ray, out RaycastHit hit, 100))
                    {
                        if (hit.transform.gameObject.CompareTag("Bant"))
                        {
                            if (aktifBant != null)
                            {
                                aktifBant.SecimIslemi(0);
                            }
                            aktifBant = hit.transform.gameObject.GetComponent<CikisBanti>();
                            aktifBant.SecimIslemi(1);
                            
                        }
                    }
                }
               
            }
        }
    }

    IEnumerator KoliGonder()
    {
        koliHavuzu[koliHavuzIndex].transform.position = koliCikisNoktasi.position;
        koliHavuzu[koliHavuzIndex].SetActive(true);
        koliHavuzIndex++;

        while (true)
        {
            yield return new WaitForSeconds(cikisZamani);
            koliHavuzu[koliHavuzIndex].transform.position = koliCikisNoktasi.position;
            koliHavuzu[koliHavuzIndex].SetActive(true);

            if (koliHavuzIndex == koliHavuzu.Count - 1)
            {
                koliHavuzIndex = 0;
            }
            else
            {
                koliHavuzIndex++;
            }
        }



    }

    public void TransferBasarili()
    {
        dogruKoliSayisi++;
        skorTextleri[0].text = dogruKoliSayisi.ToString();
        SesCal(2);
        print("Koli Sayisi: " + dogruKoliSayisi);

        switch (dogruKoliSayisi)
        {
            case 5:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = .6f;

                }
                cikisZamani = 1.5f;
                break;

            case 10:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = .7f;

                }
                cikisZamani = 1.4f;
                break;


            case 20:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = .8f;

                }
                cikisZamani = 1.3f;
                break;


            case 30:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = .9f;

                }
                cikisZamani = 1.2f;
                break;

            case 40:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 1f;

                }
                cikisZamani = 1.1f;
                break;

            case 50:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 1.1f;

                }
                cikisZamani = 1f;
                break;

            case 60:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 1.2f;

                }
                cikisZamani = 1f;
                break;

            case 70:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 1.3f;

                }
                cikisZamani = 1f;
                break;

            case 80:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 1.4f;

                }
                cikisZamani = 1f;
                break;

            case 90:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 1.5f;

                }
                cikisZamani = 0.9f;
                break;


            case 100:
                foreach (var item in tasimaBantlari)
                {
                    item.bantHizi = 2f;

                }
                cikisZamani = 0.9f;
                break;





        }
    }
    public void TransferBasarisiz()
    {
        if (dogruKoliSayisi > PlayerPrefs.GetInt("YuksekSkor"))
        {
            PlayerPrefs.SetInt("YuksekSkor", dogruKoliSayisi);
        }
        SesCal(3);
        skorTextleri[2].text = dogruKoliSayisi.ToString();
        skorTextleri[3].text = PlayerPrefs.GetInt("YuksekSkor").ToString();
        PanelIslemler(2, true);
        Time.timeScale = 0;
    }

    public Transform BantKabulNoktasi()
    {
        if (aktifBant != null)
            return aktifBant.varisNoktasi.transform;

        else
            return null;
    }

    public string BantSehirAdiGetir()
    {
        if (aktifBant != null)
            return aktifBant.sehirAdi;

        else
            return null;
    }

    void SahneIlkIslemler()
    {

        if (PlayerPrefs.GetInt("OyunSesi") == 1)
        {
            sesAyarButonlari[0].sprite = spriteObjeleri[0];
            sesler[0].mute = false;
        }
        else
        {
            sesAyarButonlari[0].sprite = spriteObjeleri[1];
            sesler[0].mute = true;
        }



        if (PlayerPrefs.GetInt("EfektSesi") == 1)
        {
            sesAyarButonlari[1].sprite = spriteObjeleri[2];

            for (int i = 1; i < sesler.Length; i++)
            {
                sesler[i].mute = false;
            }
        }
        else
        {
            sesAyarButonlari[1].sprite = spriteObjeleri[3];
            for (int i = 1; i < sesler.Length; i++)
            {
                sesler[i].mute = true;
            }

        }



        if (!PlayerPrefs.HasKey("YuksekSkor"))
        {
            PlayerPrefs.SetInt("YuksekSkor", 0);
            PlayerPrefs.SetInt("OyunSesi", 1);
            PlayerPrefs.SetInt("EfektSesi", 1);
        }
        skorTextleri[1].text = PlayerPrefs.GetInt("YuksekSkor").ToString();
    }

    void PanelIslemler(int Index, bool Durum)
    {
        paneller[Index].SetActive(Durum);
    }

    public void ButonIslemleri(string deger)
    {
        switch (deger)
        {
            case "Basla":
                SesCal(1);
                PanelIslemler(0, false);
                PanelIslemler(4, true);
                StartCoroutine(KoliGonder());
                break;

            case "Durdur":
                SesCal(1);
                PanelIslemler(1, true);
                Time.timeScale = 0;
                break;


            case "Cikis":
                SesCal(1);
                PanelIslemler(3, true);
                break;

            case "Evet":
                SesCal(1);
                Application.Quit();
                break;

            case "Hayir":
                SesCal(1);
                PanelIslemler(3, false);
                break;

            case "Tekrar":
                SesCal(1);
                SceneManager.LoadScene(sahneIndex);
                Time.timeScale = 1;
                break;

            case "DevamEt":
                SesCal(1);
                PanelIslemler(1, false);
                Time.timeScale = 1;
                break;

            case "OyunSesiAyar":
                SesCal(1);
                if (PlayerPrefs.GetInt("OyunSesi") == 0)
                {
                    PlayerPrefs.SetInt("OyunSesi", 1);
                    sesAyarButonlari[0].sprite = spriteObjeleri[0];
                    sesler[0].mute = false;
                }
                else
                {
                    PlayerPrefs.SetInt("OyunSesi", 0);
                    sesAyarButonlari[0].sprite = spriteObjeleri[1];
                    sesler[0].mute = true;
                }
                break;


            case "EfektSesiAyar":
                SesCal(1);

                if (PlayerPrefs.GetInt("EfektSesi") == 0)
                {
                    PlayerPrefs.SetInt("EfektSesi", 1);
                    sesAyarButonlari[1].sprite = spriteObjeleri[2];

                    for (int i = 1; i < sesler.Length; i++)
                    {
                        sesler[i].mute = false;
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("EfektSesi", 0);
                    sesAyarButonlari[1].sprite = spriteObjeleri[3];
                    for (int i = 1; i < sesler.Length; i++)
                    {
                        sesler[i].mute = true;
                    }

                }
                break;


        }
    }

    public void SesCal(int index)
    {
        sesler[index].Play();
    }

    //DURDURMA PANELÝNDE TIKLARKEN OYUNDAKÝ TIKLAMAYI ALGILAMASIN DÝYE GEREKLÝ OLAN KOD
    bool UIObjesiMi()
    {
        if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }
        return false;
    }


}
