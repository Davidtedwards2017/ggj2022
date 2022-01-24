using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickSpawnController : MonoBehaviour
{
    public Grid Grid;
    public GlobalPropertiesSO GlobalProperties;
    public SpawnerGroup UpperSpawnGroup;
    public SpawnerGroup LowerSpawnGroup;
    public List<ColumnSpawnerGroup> ColumnSpawnerGroups;

    public Transform BrickContainer;

    //public List<Spawner> CurretBag;
    //public List<Spawner> NextBag;

    public List<ColumnSpawnerGroup> CurretBag;
    public List<ColumnSpawnerGroup> NextBag;

    public BrickGroup BrickGroupPrefab;

    public bool Spawning = true;

    private void OnEnable()
    {
        Rebuild();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnSequence());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void Rebuild()
    {
        UpperSpawnGroup.RebuildSpawners();
        LowerSpawnGroup.RebuildSpawners();

        CreateColumnSpawningGroups();
    }

    public void CreateColumnSpawningGroups()
    {
        ColumnSpawnerGroups = new List<ColumnSpawnerGroup>();

        for (int col = 0; col < Grid.ColumnCount; col++)
        {
            var upperSpawner = UpperSpawnGroup.Spawners.FirstOrDefault(s => s.Column == col);
            var lowerSpawner = LowerSpawnGroup.Spawners.FirstOrDefault(s => s.Column == col);

            var colSpawnGroup = new ColumnSpawnerGroup(col, GlobalProperties, upperSpawner, lowerSpawner);
            ColumnSpawnerGroups.Add(colSpawnGroup);
        }

    }

    //private List<Spawner> GetSpawnersInRandomOrder()
    //{
    //    var spawners = SpawnerGroups.SelectMany(sg => sg.Spawners).ToArray();
    //    return spawners.OrderBy(x => Random.value).ToList();
    //}

    private List<ColumnSpawnerGroup> GetColumnSpawnerGroupsInRandomOrder()
    {
        var spawners = new List<ColumnSpawnerGroup>(ColumnSpawnerGroups);
        spawners.Shuffle();
        return spawners;
    }

    private IEnumerator SpawnSequence()
    {
        while (true)
        {
            yield return Spawn();
        }
    }

    //public IEnumerator Spawn()
    //{
    //    var spawner = RequestNextSpawner();
    //    yield return new WaitForSeconds(GlobalProperties.TimeBetweenBrickSpawns);
    //
    //    if (Spawning)
    //    {
    //        spawner.Spawn(BrickGroupPrefab, BrickTypes.PickRandom(), BrickContainer);
    //    }
    //}


    public Side SpawnSide;
    public IEnumerator Spawn()
    {
        var spawnerGroup = RequestNextColumnSpawnerGroup();
        yield return new WaitForSeconds(GlobalProperties.TimeBetweenBrickSpawns);

        if (Spawning)
        {
            spawnerGroup.SpawnNext(SpawnSide, BrickGroupPrefab, BrickContainer);
            SwapSpawnSide();
        }
    }

    private void SwapSpawnSide()
    {
        switch (SpawnSide)
        {
            case Side.Lower:
                SpawnSide = Side.Upper;
                break;
            case Side.Upper:
                SpawnSide = Side.Lower;
                break;
        }

    }

    //public Spawner RequestNextSpawner()
    //{
    //    if (CurretBag == null) CurretBag = new List<Spawner>();
    //    if (NextBag == null) NextBag = new List<Spawner>();
    //
    //    if (!NextBag.Any())
    //    {
    //        NextBag.AddRange(GetSpawnersInRandomOrder());
    //    }
    //
    //    if (!CurretBag.Any())
    //    {
    //        CurretBag.AddRange(NextBag);
    //        NextBag.Clear();
    //    }
    //
    //    var spawner = CurretBag.First();
    //    CurretBag.Remove(spawner);
    //
    //    return spawner;
    //}

    public ColumnSpawnerGroup RequestNextColumnSpawnerGroup()
    {
        if (CurretBag == null) CurretBag = new List<ColumnSpawnerGroup>();
        if (NextBag == null) NextBag = new List<ColumnSpawnerGroup>();

        if (!NextBag.Any())
        {
            NextBag.AddRange(GetColumnSpawnerGroupsInRandomOrder());
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

    [System.Serializable]
    public class ColumnSpawnerGroup
    {
        private BrickTypeFilteredRandom randomTypePicker;
        private Spawner upperSpawner;
        private Spawner lowerSpawner;
        
        [ReadOnly]
        public int Column;

        public ColumnSpawnerGroup(
            int column, 
            GlobalPropertiesSO properties, 
            Spawner upperSpawner, 
            Spawner lowerSpawner)
        {
            Column = column;
            randomTypePicker = new BrickTypeFilteredRandom(properties.BrickTypes, 2);

            this.upperSpawner = upperSpawner;
            this.lowerSpawner = lowerSpawner;
        }

        public void SpawnNext(Side side, BrickGroup prefab, Transform container)
        {
            var spawner = side == Side.Upper ? upperSpawner : lowerSpawner;
            spawner.Spawn(prefab, randomTypePicker.GetNextRandom(), container);
        }
    }

}
