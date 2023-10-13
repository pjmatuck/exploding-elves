using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class VFXSpawnerController : AbstractSpawnerController
{
    ParticleSystem _spawningObject;
    ObjectPool<ParticleSystem> _pool;

    int particleCounter = 0;

    protected override void InitPool()
    {
        _pool = new ObjectPool<ParticleSystem>();
    }

    public void SpawnExplosion(Vector3 position)
    {
        SpawnObject(position);
    }

    protected override void SpawnObject(Vector3 position)
    {
        var particleSystem = GetObject();
        particleSystem.transform.position = position;
        particleSystem.gameObject.SetActive(true);

        if (particleSystem != null)
        {
            particleSystem.Play();
            //Invoke(nameof(DisableEffect), _spawningObject.main.duration);
            StartCoroutine(DisableEffect(particleSystem, particleSystem.main.duration));
        }
    }

    protected ParticleSystem GetObject()
    {
        ParticleSystem particleSystem;
        if (!_pool.TryGetObject(out particleSystem))
        {
            particleSystem = Instantiate(_spawningObject, _transform);
            particleSystem.gameObject.name = $"Particle-{particleCounter++}";
        }

        return particleSystem;
    }

    IEnumerator DisableEffect(ParticleSystem particle, float timer)
    {
        yield return new WaitForSeconds(timer);
        particle.gameObject.SetActive(false);
        TakeObjectBack(particle);
    }
    protected void TakeObjectBack(ParticleSystem ps)
    {
        ps.gameObject.SetActive(false);
        _pool.Return(ps);
    }

    public override void SetSpawningObject(Object obj)
    {
        _spawningObject = (ParticleSystem)obj;
    }
}
