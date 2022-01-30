using gamedev.utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickSpawnController : MonoBehaviour
{
    public Grid Grid;
    public BrickCollection brickCollection;
    public DifficultyController DifficultyController;
    public GlobalPropertiesSO GlobalProperties;
    public SpawnerGroup UpperSpawnGroup;
    public SpawnerGroup LowerSpawnGroup;
    public List<ColumnSpawnerGroup> ColumnSpawnerGroups;
    public AudioSource audioEmit;

    public Transform BrickContainer;
    public Side SpawnSide;

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

            var colSpawnGroup = new ColumnSpawnerGroup(col, GlobalProperties, upperSpawner, lowerSpawner, 
                DifficultyController, brickCollection);
            ColumnSpawnerGroups.Add(colSpawnGroup);
        }
    }

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

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(DifficultyController.current.TimeBetweenSpawns);

        if (Spawning)
        {
            for (int i = 0; i < DifficultyController.current.BricksPerSpawn; i++)
            {
                var spawnerGroup = RequestNextColumnSpawnerGroup();
                spawnerGroup.SpawnNext(SpawnSide, BrickGroupPrefab, BrickContainer, DifficultyController.current);
                SwapSpawnSide();
            }
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
            Spawner lowerSpawner,
            DifficultyController difficultyController,
            BrickCollection brickCollection)
        {
            Column = column;
            randomTypePicker = new BrickTypeFilteredRandom(properties.BrickTypes, 2, 
                difficultyController, brickCollection, column);

            this.upperSpawner = upperSpawner;
            this.lowerSpawner = lowerSpawner;
        }

        public void SpawnNext(Side side, BrickGroup prefab, Transform container, DifficultySetting difficulty)
        {
            var spawner = side == Side.Upper ? upperSpawner : lowerSpawner;
            spawner.Spawn(prefab, randomTypePicker.GetNextRandom(), container, difficulty);

        }
    }

}
