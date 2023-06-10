using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class Gun : MonoBehaviour
{
    public GameObject crosshair;

    public GameObject image0RedCross3Empty;
    public GameObject image1RedCross2Empty;
    public GameObject image2RedCross1Empty;
    public GameObject image3RedCross0Empty;

    public GameObject muzzleFlash;

    public AudioSource gunSource;
    public AudioClip gunShotSound;

    public AudioSource spawnsSource;
    public AudioClip fruitSplashSound;
    public AudioClip bombExplodeSound;

    public float shootingRange = 100f;

    private int score = 0;
    private int bombShootCount = 0;

    public TMP_Text scoreText;

    public ParticleSystem appleJuice;
    public ParticleSystem strawberryJuice;
    public ParticleSystem peachJuice;
    public ParticleSystem pearJuice;
    public ParticleSystem lemonJuice;

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
            gunSource.PlayOneShot(gunShotSound);
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            if (hit.collider.CompareTag("Apple") || hit.collider.CompareTag("Strawberry") || hit.collider.CompareTag("Peach") || hit.collider.CompareTag("Pear") || hit.collider.CompareTag("Lemon"))
            {
                spawnsSource.PlayOneShot(fruitSplashSound);
                Destroy(hit.collider.gameObject);
                score++;
                UpdateScoreText();

                if (hit.collider.gameObject.tag == "Apple")
                {
                    ParticleSystem appleParticles = Instantiate(appleJuice, hit.transform.position, Quaternion.identity);
                    Destroy(appleParticles.gameObject, 5f);
                }
                else if (hit.collider.gameObject.tag == "Strawberry")
                {
                    ParticleSystem strawberryParticles = Instantiate(strawberryJuice, hit.transform.position, Quaternion.identity);
                    Destroy(strawberryParticles.gameObject, 5f);
                }
                else if (hit.collider.gameObject.tag == "Peach")
                {
                    ParticleSystem peachParticles = Instantiate(peachJuice, hit.transform.position, Quaternion.identity);
                    Destroy(peachParticles.gameObject, 5f);
                }
                else if (hit.collider.gameObject.tag == "Pear")
                {
                    ParticleSystem pearParticles = Instantiate(pearJuice, hit.transform.position, Quaternion.identity);
                    Destroy(pearParticles.gameObject, 5f);
                }
                else if (hit.collider.gameObject.tag == "Lemon")
                {
                    ParticleSystem lemonParticles = Instantiate(lemonJuice, hit.transform.position, Quaternion.identity);
                    Destroy(lemonParticles.gameObject, 5f);
                }


            }

            else if (hit.collider.CompareTag("Bomb"))
            {
                spawnsSource.PlayOneShot(bombExplodeSound);
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
