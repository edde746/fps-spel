using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 3f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactParticle;

    float cooldown = 0f;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldown <= 0f)
        {
            muzzleFlash.Play();
            RaycastHit ray;
            var hit = Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out ray);
            if (hit) {
                Instantiate(impactParticle, ray.point, Quaternion.identity);
                var target = ray.transform.GetComponent<Target>();
                if (target) {
                    target.ApplyDamage(damage);
                }
            }

            cooldown = 1f/fireRate;
        }
    }
}
