using UnityEngine;
using UnityEngine.UI;

public class CatShoot : MonoBehaviour
{
    // Primary projectile settings
    public GameObject primaryProjectilePrefab;
    public float primaryProjectileSpeed = 20f;
    public float primaryProjectileLifetime = 5f;
    public float primaryShootingInterval = 0.5f;

    // Secondary projectile settings
    public GameObject secondaryProjectilePrefab;
    public float secondaryProjectileSpeed = 15f;
    public float secondaryProjectileLifetime = 3f;
    public float secondaryShootingInterval = 1f;

    // General settings
    public Transform handPosition;
    public LayerMask aimingLayerMask;
    public Camera playerCamera;

    // UI Elements for cooldown indicators
    public Image primaryCooldownImage;
    public Image secondaryCooldownImage;

    private float timeSinceLastPrimaryShot;
    private float timeSinceLastSecondaryShot;

    void Start()
    {
        if (primaryProjectilePrefab == null) Debug.LogError("Primary projectile prefab is not assigned!");
        if (secondaryProjectilePrefab == null) Debug.LogError("Secondary projectile prefab is not assigned!");
        if (handPosition == null) Debug.LogError("Hand position is not assigned!");
        if (playerCamera == null) Debug.LogError("Player camera is not assigned!");
        if (primaryCooldownImage == null) Debug.LogError("Primary cooldown image is not assigned!");
        if (secondaryCooldownImage == null) Debug.LogError("Secondary cooldown image is not assigned!");

        // Initialize cooldown images to full (1)
        if (primaryCooldownImage) primaryCooldownImage.fillAmount = 1f;
        if (secondaryCooldownImage) secondaryCooldownImage.fillAmount = 1f;
    }

    void Update()
    {
        // Update time since last shots
        timeSinceLastPrimaryShot += Time.deltaTime;
        timeSinceLastSecondaryShot += Time.deltaTime;

        // Update UI fill amounts (1 to 0)
        if (primaryCooldownImage)
            primaryCooldownImage.fillAmount = Mathf.Clamp01(1f - (timeSinceLastPrimaryShot / primaryShootingInterval));

        if (secondaryCooldownImage)
            secondaryCooldownImage.fillAmount = Mathf.Clamp01(1f - (timeSinceLastSecondaryShot / secondaryShootingInterval));

        // Primary Shooting - Mouse (Left Click) or Gamepad (LB)
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.JoystickButton4)) && timeSinceLastPrimaryShot >= primaryShootingInterval)
        {
            ShootProjectile(primaryProjectilePrefab, primaryProjectileSpeed, primaryProjectileLifetime);
            timeSinceLastPrimaryShot = 0f;  // Reset cooldown
        }

        // Secondary Shooting - Mouse (Right Click) or Gamepad (RB)
        if ((Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.JoystickButton5)) && timeSinceLastSecondaryShot >= secondaryShootingInterval)
        {
            ShootProjectile(secondaryProjectilePrefab, secondaryProjectileSpeed, secondaryProjectileLifetime);
            timeSinceLastSecondaryShot = 0f;  // Reset cooldown
        }
    }

    void ShootProjectile(GameObject projectilePrefab, float projectileSpeed, float projectileLifetime)
    {
        if (projectilePrefab == null || handPosition == null || playerCamera == null)
        {
            Debug.LogError("Required components are missing.");
            return;
        }

        // Raycast to determine shooting direction
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 shootingDirection = playerCamera.transform.forward;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimingLayerMask))
        {
            shootingDirection = (hit.point - handPosition.position).normalized;
        }

        // Instantiate and set projectile velocity
        GameObject projectile = Instantiate(projectilePrefab, handPosition.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = shootingDirection * projectileSpeed;

        Destroy(projectile, projectileLifetime);
    }
}
