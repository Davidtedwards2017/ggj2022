using gamedev.utilities.events;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GGJ/Channels/Grid Channel")]
public class GridChannel : Channel<Grid> { }

[System.Serializable]
public class GridChannelEvent : UnityEvent<Grid> { }

