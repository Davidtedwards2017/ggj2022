using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.Linq;
using System.Collections;
using System.Text;

public static class GenericExtensions
{

    public static string Caesar(this string source, Int16 shift)
    {
        var maxChar = Convert.ToInt32(char.MaxValue);
        var minChar = Convert.ToInt32(char.MinValue);

        var buffer = source.ToCharArray();

        for (var i = 0; i < buffer.Length; i++)
        {
            var shifted = Convert.ToInt32(buffer[i]) + shift;

            if (shifted > maxChar)
            {
                shifted -= maxChar;
            }
            else if (shifted < minChar)
            {
                shifted += maxChar;
            }

            buffer[i] = Convert.ToChar(shifted);
        }

        return new string(buffer);
    }

    public static bool IsDestroyed(this GameObject gameObject)
    {
        // UnityEngine overloads the == opeator for the GameObject type
        // and returns null when the object has been destroyed, but 
        // actually the object is still there but has not been cleaned up yet
        // if we test both we can determine if the object has been destroyed.
        return gameObject == null && !ReferenceEquals(gameObject, null);
    }

    public static bool Exists(this string value)
    {
        return (!string.IsNullOrEmpty(value));
    }

    public static IEnumerable<T> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static string AddSpacesToSentence(this string text, bool preserveAcronyms = false)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }

    public static T SafeAddComponent<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null)
        {
            comp = go.AddComponent<T>();
        }

        return comp;
    }

    public static bool Is<T>(this object obj) where T : Component
    {
        if (obj == null) return false;

        if (obj is GameObject)
        {
            GameObject go = (GameObject)obj;
            var component = go.GetComponent<T>();
            return component != null;
        }
        else if (obj is T)
        {
            return true;
        }

        return false;
    }

    public static T Get<T>(this object obj) where T : Component
    {
        if (obj == null) return null;

        if (obj is GameObject)
        {
            GameObject go = (GameObject)obj;
            var component = go.GetComponent<T>();
            return component;
        }
        else if (obj is T)
        {
            return (T)obj;
        }

        return null;
    }

    public static T[] Shift<T>(this T[] collection)
    {
        T[] temp = new T[collection.Length];
        for (int i = 0; i < collection.Length - 1; i++)
        {
            temp[i + 1] = collection[i];
        }

        return temp;
    }

    public static void AddIfUnique<T>(this List<T> collection, T entry)
    {
        if (entry != null && !collection.Contains(entry))
        {
            collection.Add(entry);
        }
    }

    public static void AddIfUnique<T>(this List<T> collection, IEnumerable<T> entries)
    {
        foreach (var entry in entries)
        {
            collection.AddIfUnique(entry);
        }
    }

    public static void SafeAdd<T, K>(this List<KeyValuePair<T, K>> collection, T key, K value)
    {
        foreach (var pair in collection)
        {
            if (pair.Key.Equals(key) && pair.Value.Equals(value))
            {
                return;
            }
        }

        collection.Add(new KeyValuePair<T, K>(key, value));
    }

    public static T SafeGet<T>(this Dictionary<string, T> dictionary, string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
        }

        return default(T);
    }

    public static void SafeRemove<T>(this List<T> collection, T value)
    {
        if (collection == null) return;

        if (collection.Contains(value))
        {
            collection.Remove(value);
        }
    }

    public static void SafeRemove<T, K>(this List<KeyValuePair<T, K>> collection, T key, K value)
    {
        foreach (var pair in collection)
        {
            if (pair.Key.Equals(key) && pair.Value.Equals(value))
            {
                collection.Remove(pair);
                return;
            }
        }
    }

    public static List<T> RemoveMatchingEntries<T>(this List<T> collection, Predicate<T> matching)
    {
        var temp = new List<T>(collection);
        foreach (var entry in collection)
        {
            if (matching(entry))
            {
                temp.Remove(entry);
            }
        }

        return temp;
    }

    public static void SafeRemove<T, K>(this Dictionary<T, K> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary.Remove(key);
        }
    }

    public static K SafeGetOrInitialize<T, K>(this Dictionary<T, K> dictionary, T key) where K : new()
    {
        if (dictionary.ContainsKey(key))
        {
            return (K)dictionary[key];
        }
        else
        {
            var instance = new K();
            dictionary.Add(key, instance);
            return instance;
        }
    }

    public static void SafeSet<T, K>(this Dictionary<T, K> dictionary, T key, K value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void AddOrUpdate<T, K>(this Dictionary<T, K> dictionary, T key, K value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public static T[] FlipRows<T>(this T[] grid, int numRows, int numColumns)
    {
        var tempMatrix = new T[grid.Length];
        int index = 0;
        for (int r = numRows - 1; r >= 0; r--)
        {
            for (int c = 0; c < numColumns; c++)
            {
                tempMatrix[index++] = grid[(r * numColumns) + c];
            }
        }

        return tempMatrix;
    }

    public static T[] FlipColumns<T>(this T[] grid, int numRows, int numColumns)
    {
        var tempMatrix = new T[grid.Length];
        int index = 0;
        for (int r = 0; r < numRows; r++)
        {
            for (int c = numColumns - 1; c >= 0; c--)
            {
                tempMatrix[index++] = grid[(r * numColumns) + c];
            }
        }

        return tempMatrix;
    }

    public static T[] SwapRowsAndColumns<T>(this T[] grid, ref int numRows, ref int numColumns)
    {
        int newRows = numColumns;
        int newCols = numRows;

        numRows = newRows;
        numColumns = newCols;

        var tempMatrix = new T[grid.Length];
        int index = 0;
        for (int r = 0; r < numRows; r++)
        {
            for (int c = numColumns - 1; c >= 0; c--)
            {
                tempMatrix[index++] = grid[(c * numRows) + r];
            }
        }

        return tempMatrix;
    }

    public static T PickRandom<T>(this List<T> collection)
    {
        if (!collection.Any())
        {
            return default(T);
        }

        //UnityEngine.Random.InitState(DateTime.UtcNow.Millisecond);
        return collection[UnityEngine.Random.Range(0, collection.Count)];
    }

    public static List<T> PickRandom<T>(this List<T> collection, int count)
    {
        if (!collection.Any())
        {
            return null;
        }

        var pickedCollection = new List<T>();
        var pool = new List<T>(collection);

        while (count-- > 0 && pool.Any())
        {
            var picked = pool.PickRandom();
            pool.Remove(picked);

            pickedCollection.Add(picked);
        }

        return pickedCollection;
    }

    public static T RemoveRandom<T>(this List<T> collection)
    {
        var instance = collection.PickRandom();
        collection.Remove(instance);
        return instance;
    }

    public static int[] Shift(this int[] myArray)
    {
        int[] tArray = new int[myArray.Length];
        for (int i = 0; i < myArray.Length; i++)
        {
            if (i < myArray.Length - 1)
                tArray[i] = myArray[i + 1];
            else
                tArray[i] = myArray[0];
        }
        return tArray;
    }

    public static GameObject RootTo(this GameObject gameObject, GameObject parent, Vector3 offset)
    {
        return gameObject.AttachTo(parent).SetLocalPosition(offset);
    }

    public static GameObject Deatach(this GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
        return gameObject;
    }

    public static T AttachTo<T>(this T component, GameObject parent) where T : Component
    {
        component.gameObject.AttachTo(parent);
        return component;
    }

    public static GameObject AttachTo(this Component component, Component parent)
    {
        if (parent == null)
        {
            return component.gameObject.Deatach();
        }

        return component.gameObject.AttachTo(parent.gameObject);
    }

    public static GameObject AttachTo(this GameObject gameObject, GameObject parent)
    {
        if (parent == null)
        {
            gameObject.transform.SetParent(null);
            return gameObject;
        }

        return gameObject.AttachTo(parent.transform);
    }

    public static GameObject AttachTo(this GameObject gameObject, Transform parent)
    {
        if (gameObject == null || parent == null) return null;

        if (parent == null)
        {
            gameObject.transform.SetParent(null);
        }
        else
        {
            gameObject.transform.SetParent(parent.transform);
        }

        return gameObject;
    }

    public static GameObject SetLocalPosition(this GameObject gameObject, Vector3 position)
    {
        if (gameObject != null)
        {
            gameObject.transform.localPosition = position;
        }

        return gameObject;
    }

    public static GameObject ZeroLocalRotation(this GameObject gameObject)
    {
        if (gameObject == null) return null;

        gameObject.transform.localRotation = Quaternion.identity;

        //gameObject.transform. = Quaternion.Euler(0, 0, 0);

        return gameObject;
    }

    public static GameObject ZeroLocalPosition(this GameObject gameObject)
    {
        return gameObject.SetLocalPosition(Vector3.zero);
    }

}