using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class DungeonObject
{
    public Vector2 position;
    public int objectType;
}
[System.Serializable]
public class DungeonEnemy
{
    public Vector2 position;
    public int enemyType;
}
[System.Serializable]
public class DungeonRoom
{
    public List<DungeonObject> objects = new List<DungeonObject>();
    public List<DungeonEnemy> enemies = new List<DungeonEnemy>();
    public int roomType;
    public bool roomCleared;
    public int roomX, roomY;
    public bool isEnd;


    public DungeonRoom(int x, int y, int roomType)
    {
        this.roomType = roomType;
        this.roomX = x;
        this.roomY = y;
        roomCleared = false;
        isEnd = false;
    }
}
[System.Serializable]
public class RoomOpenings
{
    public bool left;
    public bool right;
    public bool up;
    public bool down;
}
[System.Serializable]
public class ToolPrefab
{
    public Tools tool;
    public GameObject prefab;
}
public class LevelGenerator : MonoBehaviour
{
    const int MARKET = 11;
    public static LevelGenerator Instance;
    public int money { get; private set; } = 0;
    public int dynomite { get; private set; } = 0;
    [SerializeField] private GameObject mineral;
    [SerializeField] private RoomOpenings[] roomOpenings;

    [SerializeField] private List<DungeonRoom> rooms = new List<DungeonRoom>();
    [SerializeField] private List<Vector2> populateStack = new List<Vector2>();
    [SerializeField] private GameObject[] roomTiles;
    [SerializeField] private Vector2[] teleportPositions;

    public Vector2 currentRoom;
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private ToolPrefab[] toolPrefabs;

    public int enemyCount = 0;

    [Header("Map")]
    [SerializeField] private GameObject mapRoom;
    [SerializeField] private Transform mapContainer;
    [SerializeField] private float mapSpacing;

    [Header("References")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text dynomiteText;
    [SerializeField] private GameObject elevator;

    [Header("settings")]
    [SerializeField] private int enemyRandomRange;
    [SerializeField] private bool isFirstLevel;

    /// <summary>
    /// Events
    /// </summary>


    public delegate void BasicEvent();
    public static event BasicEvent OnChangeRoom;

    public static event BasicEvent OnShowElevator;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private int intersectionRooms = 0;
    public Vector2 endRoom { get; private set; }
    private int GetRoom(int x, int y)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].roomX == x && rooms[i].roomY == y)
                return i;
        }


        return -1;
    }
    public void ShowElevator()
    {
        Debug.Log("end room: " + endRoom);
        OnShowElevator?.Invoke();
    }
    private void PopulateRoom(DungeonRoom room)
    {
        for (int i = 0; i < Random.Range(5, 15); i++)
        {
            room.objects.Add(new DungeonObject());
            room.objects[room.objects.Count - 1].objectType = Random.Range(0, 6);
            room.objects[room.objects.Count - 1].position = new Vector2((int)Random.Range(-spawnRange.x, spawnRange.x), (int)Random.Range(-spawnRange.y, spawnRange.y));
        }
        if (room.roomX == 0 && room.roomY == 0)
            return;
        for (int i = 0; i < Random.Range(0, 10); i++)
        {
            room.enemies.Add(new DungeonEnemy());
            room.enemies[room.enemies.Count - 1].enemyType = Random.Range(0, enemyRandomRange);
            room.enemies[room.enemies.Count - 1].position = new Vector2((int)Random.Range(-spawnRange.x, spawnRange.x), (int)Random.Range(-spawnRange.y, spawnRange.y));
        }
    }
    private void GenerateRoom(int x, int y)
    {
        if (GetRoom(x, y) != -1)
            return;

        rooms.Add(new DungeonRoom(x, y, 0));
        DungeonRoom current = rooms[rooms.Count - 1];
        if (x == 0 && y == 0)
        {
            if (isFirstLevel)
            {
                current.roomType = 3;
            }
            else
            {
                current.roomType = MARKET;
            }
        }
        else if (x == 1 && y == 0)
        {
            current.roomType = 0;
        }
        else
        {

            bool works = false;
            // bool triedEnd = false;

            while (!works)
            {
                current.roomType = Random.Range(0, 12);
                if (current.roomType == MARKET)
                {
                    continue;
                }
                /*if (rooms.Count > 2 && !triedEnd)
                {
                    current.roomType = 12;
                    triedEnd = true;
                }*/
                if (intersectionRooms >= 3 && current.roomType == 0 && Random.Range(0, 30) != 0)
                    continue;
                if ((current.roomType == 1 || current.roomType == 10) && Random.Range(0, 4) == 0)
                    continue;
                works = true;

                int leftRoom = GetRoom(x - 1, y);
                int rightRoom = GetRoom(x + 1, y);
                int upRoom = GetRoom(x, y + 1);
                int downRoom = GetRoom(x, y - 1);

                Debug.Log(leftRoom + ", " + rightRoom + ", " + upRoom + ", " + downRoom);

                if (roomOpenings[current.roomType].left && leftRoom != -1 && !roomOpenings[rooms[leftRoom].roomType].right)
                    works = false;
                if (roomOpenings[current.roomType].right && rightRoom != -1 && !roomOpenings[rooms[rightRoom].roomType].left)
                    works = false;
                if (roomOpenings[current.roomType].up && upRoom != -1 && !roomOpenings[rooms[upRoom].roomType].down)
                    works = false;
                if (roomOpenings[current.roomType].down && downRoom != -1 && !roomOpenings[rooms[downRoom].roomType].up)
                    works = false;

                if (!roomOpenings[current.roomType].left && leftRoom != -1 && roomOpenings[rooms[leftRoom].roomType].right)
                    works = false;
                if (!roomOpenings[current.roomType].right && rightRoom != -1 && roomOpenings[rooms[rightRoom].roomType].left)
                    works = false;
                if (!roomOpenings[current.roomType].up && upRoom != -1 && roomOpenings[rooms[upRoom].roomType].down)
                    works = false;
                if (!roomOpenings[current.roomType].down && downRoom != -1 && roomOpenings[rooms[downRoom].roomType].up)
                    works = false;
            }
        }
        if (roomOpenings[current.roomType].up)
        {
            populateStack.Add(new Vector2(x, y + 1));
        }
        if (roomOpenings[current.roomType].left)
        {
            populateStack.Add(new Vector2(x - 1, y));
        }
        if (roomOpenings[current.roomType].down)
        {
            populateStack.Add(new Vector2(x, y - 1));
        }
        if (roomOpenings[current.roomType].right)
        {
            populateStack.Add(new Vector2(x + 1, y));
        }
        if (current.roomType == 0)
            intersectionRooms++;


        if (x != 0 || y != 0)
            PopulateRoom(current);

    }
    private void GenerateLevel()
    {
        intersectionRooms = 0;
        populateStack = new List<Vector2>();
        populateStack.Add(new Vector2(0, 0));
        while (populateStack.Count > 0)
        {
            GenerateRoom((int)populateStack[0].x, (int)populateStack[0].y);
            populateStack.RemoveAt(0);
        }
        int endRoomIndex = Random.Range(2, rooms.Count);
        rooms[endRoomIndex].isEnd = true;
        endRoom = new Vector2(rooms[endRoomIndex].roomX, rooms[endRoomIndex].roomY);
    }
    private void DrawMap()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject x = Instantiate(mapRoom, mapContainer);
            x.GetComponent<MapTile>().position = new Vector2(rooms[i].roomX, rooms[i].roomY);
            x.GetComponent<RectTransform>().anchoredPosition = new Vector2(rooms[i].roomX * mapSpacing, rooms[i].roomY * mapSpacing);
        }
    }
    private void Start()
    {
        Instance = this;
        currentRoom = new Vector2(0, 0);
        GenerateLevel();
        DrawMap();
        Move(5);
    }
    private void Update()
    {
        moneyText.text = money.ToString();
        dynomiteText.text = dynomite.ToString() + " (E)";
        if (enemyCount <= 0)
        {
            rooms[GetRoom((int)currentRoom.x, (int)currentRoom.y)].roomCleared = true;
        }
    }
    //0 - up, 1 - right, 2 - down, 3 - left, 5 - just refresh
    public void Move(int direction)
    {
        foreach (GameObject x in instantiatedObjects)
            Destroy(x);
        instantiatedObjects = new List<GameObject>();
        if (direction != 5)
        {
            switch (direction)
            {
                case 0:
                    currentRoom.y += 1;
                    break;
                case 1:
                    currentRoom.x += 1;
                    break;
                case 2:
                    currentRoom.y -= 1;
                    break;
                case 3:
                    currentRoom.x -= 1;
                    break;
            }
            PlayerCharacter.Instance.SetPosition(teleportPositions[(direction + 2) % 4]);
        }

        int roomNumber = GetRoom((int)currentRoom.x, (int)currentRoom.y);
        for (int i = 0; i < roomTiles.Length; i++)
        {
            roomTiles[i].SetActive(rooms[roomNumber].roomType == i);
        }

        if (GroundRandomizer.Instance != null)
            GroundRandomizer.Instance.Randomize();
        mapContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-currentRoom.x * mapSpacing, -currentRoom.y * mapSpacing);

        foreach (DungeonObject obj in rooms[roomNumber].objects)
        {
            GameObject x = Instantiate(mineral);
            x.GetComponent<Mineral>().SetMineral(obj.objectType);
            x.transform.position = obj.position;
            instantiatedObjects.Add(x);
        }

        if (!rooms[roomNumber].roomCleared)
        {
            foreach (DungeonEnemy enemy in rooms[roomNumber].enemies)
            {
                GameObject x = Instantiate(enemyPrefabs[enemy.enemyType]);
                x.transform.position = enemy.position;
                instantiatedObjects.Add(x);
            }
            enemyCount = rooms[roomNumber].enemies.Count;
        }

        Debug.Log(roomNumber);

        OnChangeRoom?.Invoke();



        if (rooms[roomNumber].roomType == MARKET)
        {
            ElevatorOpen.Instance.OpenCage();
        }
        if (rooms[roomNumber].isEnd)
        {
            elevator.SetActive(true);
        }
        else
        {
            elevator.SetActive(false);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
    public void BuyDynomite()
    {
        dynomite++;
    }
    public void UseDynomite()
    {
        dynomite--;
    }
    public void GivePlayerWeapon(Tools weapon)
    {
        foreach (ToolPrefab t in toolPrefabs)
        {
            if (t.tool == weapon)
            {
                GameObject x = Instantiate(t.prefab);
                PlayerCharacter.Instance.Drop();
                x.GetComponent<ItemPickup>().Pickup();
                PlayerCharacter.Instance.SetItem(x.GetComponent<ItemPickup>());
                return;
            }
        }
    }
    public void SetDynomite(int amount)
    {
        dynomite = amount;
    }

}
