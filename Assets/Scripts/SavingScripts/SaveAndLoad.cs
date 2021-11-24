using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

public static class SaveAndLoad
{

    public static void SaveStats(bool keepPreviousStats, int deaths)
    {

        string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnxietyPlayerData.anx");
        BinaryFormatter formatter = new BinaryFormatter();

        PlayerStats previousStats = null;
        if(keepPreviousStats)
            previousStats = LoadStats();

        FileStream stream = new FileStream(savePath, FileMode.Create);


        PlayerStats stats = new PlayerStats(deaths, previousStats);
        
        formatter.Serialize(stream, stats);
        stream.Close();

    }

    public static PlayerStats LoadStats()
    {

        string loadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnxietyPlayerData.anx");
        PlayerStats loadedStats = null;
        
        if(File.Exists(loadPath))
        {

            BinaryFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(loadPath, FileMode.OpenOrCreate);
            loadedStats = formatter.Deserialize(stream) as PlayerStats;

            stream.Close();

        }
        else
        {
            
            Debug.Log("No file found on path: " + loadPath + " Creating empty file...");
            SaveStats(false, 0);

            BinaryFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(loadPath, FileMode.Open);
            loadedStats = formatter.Deserialize(stream) as PlayerStats;

            stream.Close();

        }

        return loadedStats;

    }

    public static void WipeStats()
    {

        string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AnxietyPlayerData.anx");
        
        if(File.Exists(savePath))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.OpenOrCreate);

            formatter.Serialize(stream, new PlayerStats(0, null));
            stream.Close();

        }
        else
            Debug.Log("No file to wipe at: " + savePath);

    }
    
}