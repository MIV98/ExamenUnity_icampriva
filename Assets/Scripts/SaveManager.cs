using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.dat";
    //private static int record = 0;
    private static int MAX_RECORDS = 5;
    private static int[] record = new int[MAX_RECORDS];

    public static void SaveRecord(int pooints)
    {
        for (int i = 0; i < record.Length; i++)
        {
            if (pooints > record[i])
            {
                int aux = record[i];
                record[i] = pooints;
                pooints = aux;
            }
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        RecordData data = new RecordData(record);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadRecord()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RecordData data = formatter.Deserialize(stream) as RecordData;  // equivalent to (RecordData)formatter.Deserialize(stream);
            stream.Close();

            record = data.GetRecord();
        }
        else
        {
            record = new int[MAX_RECORDS];
        }
        return record;
    }
}
