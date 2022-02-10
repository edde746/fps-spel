using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactParticle;

    float cooldown = 0f;

    public int bulletsInMagazine = 20;
    public int magazineSize = 20;
    public float reloadTime = 17f;
    TextMeshProUGUI counter;
    bool reloading = false;

    void Start()
    {
        counter = GameObject.FindGameObjectWithTag("BulletCounter").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (reloading && cooldown <= 0f)
        {
            counter.text = $"{magazineSize}/{magazineSize}";
            reloading = false;
        }

        if (Input.GetButton("Fire1") && cooldown <= 0f && bulletsInMagazine > 0)
        {
            bulletsInMagazine -= 1;
            counter.text = $"{bulletsInMagazine}/{magazineSize}";
            if (bulletsInMagazine <= 0)
            {
                cooldown = reloadTime;
                bulletsInMagazine = magazineSize;
                counter.text = "Reloading...";
                reloading = true;
            }

            muzzleFlash.Play();
            RaycastHit ray;
            var hit = Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out ray);
            if (hit)
            {
                Instantiate(impactParticle, ray.point, Quaternion.identity);
                var target = ray.transform.GetComponent<Target>();
                if (target)
                {
                    target.ApplyDamage(damage);
                }
            }

            cooldown = 1f / fireRate;
        }

        if (counter && cooldown <= 0f)
            counter.text = $"{bulletsInMagazine}/{magazineSize}";

    }
}
