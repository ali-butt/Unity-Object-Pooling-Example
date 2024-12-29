# Object Pooling in Unity (Bullet Pooling)

This repository contains two scripts that implement object pooling for bullets in Unity. Object pooling is a design pattern used to reuse objects that are expensive to create and destroy, improving performance and resource management in games.

## Files Included

- **ObjectPoolingManager.cs**: Manages the object pool, handles bullet retrieval, release, and destruction.
- **Bullet.cs**: The bullet behavior script that handles interactions with the pool when a bullet collides with certain objects.

## How to Use

1. **Import the Scripts**: Copy the `ObjectPoolingManager.cs` and `Bullet.cs` scripts into your Unity project.

2. **Setup in Unity**:

   - Create a Bullet prefab and attach the `Bullet.cs` script to it.
   - Create an empty GameObject in your scene to hold the `ObjectPoolingManager` script.
   - Assign the Bullet prefab to the `bulletPrefab` field in the `ObjectPoolingManager` script via the Unity Inspector.
   - Assign the transform of the Gun (or where the bullets should be spawned) to the `gunTransform` field in the `ObjectPoolingManager` script.

3. **Configure the Pool**: The object pool is configured in the `Awake()` method of the `ObjectPoolingManager`. You can customize the pool's initial capacity and maximum size as needed.

4. **Shooting**: The bullets are fired when the left mouse button is pressed (`Input.GetMouseButton(0)`). The bullets are retrieved from the pool, activated, and fired in the direction of the gun's transform.

5. **Bullet Collision**: When the bullet collides with an object tagged "GameController", it is released back to the pool.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
