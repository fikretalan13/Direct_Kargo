using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Kutu : MonoBehaviour
{
    bool hareketEt;

    Transform varisNoktasi;

    public string kutuSehirAdi;

    string aktifBantSehirAdi;

    void Update()
    {
        if (hareketEt)
        {
            transform.position = Vector3.Lerp(transform.position,varisNoktasi.transform.position,.2f);

            if(Vector3.Distance(transform.position,varisNoktasi.position) < .50f)
            {
                hareketEt = false;

                if (kutuSehirAdi == aktifBantSehirAdi)
                    GameManager.Instance.TransferBasarili();

                else
                    GameManager.Instance.TransferBasarisiz();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TasimaNoktasi"))
        {
            if (GameManager.Instance.BantKabulNoktasi() != null)
            {
                varisNoktasi = GameManager.Instance.BantKabulNoktasi();

                aktifBantSehirAdi = GameManager.Instance.BantSehirAdiGetir();
                hareketEt = true;
            }
        }

        else if (other.gameObject.CompareTag("DeaktifEdiciObje"))
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(-90,0,-90);
        }
    }
}
