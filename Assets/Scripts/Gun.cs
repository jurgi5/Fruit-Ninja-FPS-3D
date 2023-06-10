using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public GameObject crosshair;

    public GameObject image0RedCross3Empty;
    public GameObject image1RedCross2Empty;
    public GameObject image2RedCross1Empty;
    public GameObject image3RedCross0Empty;

    public GameObject muzzleFlash;

    public AudioSource source;
    public AudioClip gunShotSound;

    public float shootingRange = 100f;

    private int score = 0;
    private int bombShootCount = 0;

    public TMP_Text scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            muzzleFlash.SetActive(true);
            Invoke(nameof(TurnOffMuzzleFlash), 0.05f);           
            source.PlayOneShot(gunShotSound);
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            if (hit.collider.CompareTag("Fruit"))
            {
                Destroy(hit.collider.gameObject);
                score++;
                UpdateScoreText();
            }

            else if (hit.collider.CompareTag("Bomb"))
            {
                Destroy(hit.collider.gameObject);
                bombShootCount++;
                DisableAllBombImages();

                if (bombShootCount == 1)
                {
                    image1RedCross2Empty.SetActive(true);
                }
                else if (bombShootCount == 2)
                {
                    image2RedCross1Empty.SetActive(true);
                }
                else if (bombShootCount == 3)
                {
                    image3RedCross0Empty.SetActive(true);
                    SceneManager.LoadScene(2);
                }
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void DisableAllBombImages()
    {
        image0RedCross3Empty.SetActive(false);
        image1RedCross2Empty.SetActive(false);
        image2RedCross1Empty.SetActive(false);
        image3RedCross0Empty.SetActive(false);
    }

    public void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }


}
