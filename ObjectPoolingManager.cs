using System;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingManager : MonoBehaviour
{
    // Prefab of the bullet to be instantiated
    public Bullet bulletPrefab;

    // Transform of the gun for setting bullet position and rotation
    public Transform gunTransform;

    // Object pool for managing bullet instances
    private ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        // Initialize the object pool with proper configurations
        bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,               // Function to create new bullet instances
            actionOnGet: OnBulletRetrieved,         // Action performed when a bullet is taken from the pool
            actionOnRelease: OnBulletReleased,      // Action performed when a bullet is returned to the pool
            actionOnDestroy: OnBulletDestroyed,     // Action performed when a bullet is permanently destroyed
            collectionCheck: true,                 // Ensures objects are not returned to the pool multiple times
            defaultCapacity: 20,                   // Initial number of bullets the pool can hold
            maxSize: 50                            // Maximum number of bullets the pool can hold
        );
    }

    /// <summary>
    /// Creates a new bullet instance when the pool needs more objects.
    /// </summary>
    /// <returns>A new bullet instance.</returns>
    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation, gunTransform);
        bullet.SetPool(bulletPool); // Pass the pool reference to the bullet
        return bullet;
    }

    /// <summary>
    /// Called when a bullet is retrieved from the pool.
    /// Sets the bullet's position, rotation, and activates it.
    /// </summary>
    /// <param name="bullet">The bullet instance retrieved from the pool.</param>
    private void OnBulletRetrieved(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = gunTransform.position;
        bullet.transform.rotation = gunTransform.rotation;

        // Removed previous velocity before Applying a force to simulate firing the bullet
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.GetComponent<Rigidbody>().AddForce(gunTransform.forward * 100, ForceMode.Impulse);
    }

    /// <summary>
    /// Called when a bullet is returned to the pool.
    /// Deactivates the bullet to hide it from the scene.
    /// </summary>
    /// <param name="bullet">The bullet instance being released to the pool.</param>
    private void OnBulletReleased(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when a bullet instance is destroyed permanently.
    /// Cleans up the bullet's resources.
    /// </summary>
    /// <param name="bullet">The bullet instance to destroy.</param>
    private void OnBulletDestroyed(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        // Retrieve a bullet from the pool when the left mouse button is clicked
        if (Input.GetMouseButton(0))
        {
            bulletPool.Get();
        }
    }
}
