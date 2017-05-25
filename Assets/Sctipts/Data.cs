using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public static class Data
{
    public static void saveJson<T>(T[] vegetale, string FileName)
    {
        string json = JsonHelper.ToJson(vegetale, true);
        string path = Path.Combine(Application.persistentDataPath, FileName + ".json");
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
    }

    public static void loadJson<T>(ref T[] vegetale, string FileName)
    {
        //string path = Path.Combine(Application.persistentDataPath, FileName + ".json");
        //string json = File.ReadAllText(path);
        //vegetale = JsonHelper.FromJson<T>(json);

        TextAsset json = Resources.Load<TextAsset>("json/" + FileName.Replace(".json", ""));
        vegetale = JsonHelper.FromJson<T>(json.text);
    }

    static bool fileExists(string FileName)
    {
        string path = Path.Combine(Application.persistentDataPath, FileName + ".json");
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void saveToolsItem(List<Tools> json_tools)
    {
        string json = JsonHelper.ToJson<Tools>(json_tools.ToArray(), true);
        string path = Path.Combine(Application.persistentDataPath, "tools.json");
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
    }

    public static void savePlantItem(List<Plant> json_plant)
    {
        string json = JsonHelper.ToJson<Plant>(json_plant.ToArray(), true);
        string path = Path.Combine(Application.persistentDataPath, "vegetable.json");
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
    }

    public static void saveItem(List<Item> json_item)
    {
        string json = JsonHelper.ToJson<Item>(json_item.ToArray(), true);
        string path = Path.Combine(Application.persistentDataPath, "item.json");
        StreamWriter sw = File.CreateText(path);
        sw.Close();
        File.WriteAllText(path, json);
    }

    public static List<Tools> loadToolsItem()
    {
        //string path = Path.Combine(Application.persistentDataPath, "tools.json");
        //string json = File.ReadAllText(path);

        TextAsset json = Resources.Load<TextAsset>("json/tools");
        Tools[] json_tools = JsonHelper.FromJson<Tools>(json.text);
        return json_tools.ToList<Tools>();
    }

    public static List<Plant> loadPlantItem()
    {
        //string path = Path.Combine(Application.persistentDataPath, "vegetable.json");
        //string json = File.ReadAllText(path);
        TextAsset json = Resources.Load<TextAsset>("json/vegetable");
        Plant[] json_plant = JsonHelper.FromJson<Plant>(json.text);
        return json_plant.ToList<Plant>();
    }

    public static List<Item> loadItem()
    {
        //string path = Path.Combine(Application.persistentDataPath, "item.json");
        //string json = File.ReadAllText(path);
        TextAsset json = Resources.Load<TextAsset>("json/item");
        Item[] json_item = JsonHelper.FromJson<Item>(json.text);
        return json_item.ToList<Item>();
    }

    public static void saveMoney(float money)
    {
        PlayerPrefs.SetFloat("money", money);
    }

    public static float loadMoney()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            return PlayerPrefs.GetFloat("money");
        }
        else
        {
            return 0;
        }
    }
}

public class JsonHelper : MonoBehaviour
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}