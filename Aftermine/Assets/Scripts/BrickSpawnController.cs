using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickSpawnController : MonoBehaviour
{
    public GlobalPropertiesSO GlobalProperties;
    public List<SpawnerGroup> SpawnerGroups;

    public List<Spawner> CurretBag;
    public List<Spawner> NextBag;

    public BrickGroup BrickGroupPrefab;
    public BrickType BrickType;

    public bool Spawning = true;

    private void OnEnable()
    {
        Rebuild();
    }

    private void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    public void Rebuild()
    {
        foreach (var spawnGroup in SpawnerGroups)
        {
            spawnGroup.RebuildSpawners();
        }
    }

    private List<Spawner> GetSpawnersInRandomOrder()
    {
        var spawners = SpawnerGroups.SelectMany(sg => sg.Spawners).ToArray();
        return spawners.OrderBy(x => Random.value).ToList();
    }

    private IEnumerator SpawnSequence()
    {
        while (true)
        {
            if (Spawning)
                yield return Spawn();
        }
    }

    public IEnumerator Spawn()
    {
        var spawner = RequestNextSpawner();
        yield return new WaitForSeconds(GlobalProperties.TimeBetweenBrickSpawns);
        spawner.Spawn(BrickGroupPrefab, BrickType);
    }

    public Spawner RequestNextSpawner()
    {
        if (CurretBag == null) CurretBag = new List<Spawner>();
        if (NextBag == null) NextBag = new List<Spawner>();

        if (!NextBag.Any())
        {
            NextBag.AddRange(GetSpawnersInRandomOrder());
        }

        if (!CurretBag.Any())
        {
            CurretBag.AddRange(NextBag);
            NextBag.Clear();
        }

        var spawner = CurretBag.First();
        CurretBag.Remove(spawner);

        return spawner;
    }
}
