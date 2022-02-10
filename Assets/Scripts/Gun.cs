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
    public float reloadTime = 3f;
    TextMeshProUGUI counter;
    bool reloading = false;

    void Start()
    {
        counter = GameObject.FindGameObjectWithTag("BulletCounter").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Manage cooldown
        if (cooldown > 0f)
            cooldown -= Time.deltaTime;

        // Reload animation & finish reload
        if (reloading)
        {
            // Some issue with Lerping rotation here :(
            transform.Rotate(360f / (reloadTime / Time.deltaTime), 0f, 0f);

            if (cooldown <= 0f)
            {
                transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                counter.text = $"{magazineSize}/{magazineSize}";
                reloading = false;
            }
        }

        if (Input.GetButton("Fire1") && cooldown <= 0f && bulletsInMagazine > 0)
        {
            // Update bullets & manage firerate
            bulletsInMagazine -= 1;
            counter.text = $"{bulletsInMagazine}/{magazineSize}";
            cooldown = 1f / fireRate;

            // Play muzzle flash & raycast to hit object
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

            // Auto reload when out of bullets
            if (bulletsInMagazine <= 0)
            {
                cooldown = reloadTime;
                bulletsInMagazine = magazineSize;
                counter.text = "Reloading...";
                reloading = true;
            }
        }
    }
}
