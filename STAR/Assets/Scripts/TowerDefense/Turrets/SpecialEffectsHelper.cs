using UnityEngine;

/// <summary>
/// Effets de particules sans efforts
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static SpecialEffectsHelper Instance;

    public ParticleSystem[] particleSystems;
    

    void Awake()
    {
        // On garde une référence du singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }

    private void Update()
    {

    }

    public void Explosion(Vector3 position)
    {            
        instantiate(particleSystems[2], position);
    }
    public void EnnemiHit(Vector3 position)
    {
        instantiate(particleSystems[1], position);

    }
    public void baseHit(Vector3 position)
    {
        instantiate(particleSystems[0], position);

    }

   

    public ParticleSystem projectileMoving(Vector3 position)
    {
       return instantiate(particleSystems[3], position);
    }
    public void destroyEnnemi(Vector3 position)
    {
        instantiate(particleSystems[4], position);
    }

    public void buildOrUpgradeTower (Vector3 position)
    {
        instantiate(particleSystems[5], position);
    }

    public void victoryEffect(Vector3 position)
    {
        instantiate(particleSystems[6], position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;
        newParticleSystem.gameObject.layer = 4;
        // Destruction programmée
        Destroy(
          newParticleSystem.gameObject,
          newParticleSystem.startLifetime
        );

        return newParticleSystem;
    }
}
