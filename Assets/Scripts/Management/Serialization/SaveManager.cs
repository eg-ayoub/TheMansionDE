using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;

namespace Management.Serialization
{
    public class SaveManager : MonoBehaviour
    {
        static string savepath = Application.persistentDataPath + "/save.gd";

        public IEnumerator SaveCoroutine(CHECKPOINTS check, int time)
        {
            // * get old save
            yield return StartCoroutine(CheckFileAndCreate());
            FileStream fs = File.Open(savepath, FileMode.Open);
            yield return null;
            BinaryFormatter bf = new BinaryFormatter();
            SaveBlob blob = (SaveBlob)bf.Deserialize(fs);
            fs.Close();
            yield return null;

            // * set new time to save
            blob.SaveTimes[(int)check - 1] = time;

            // * write to save file
            fs = File.Create(savepath);
            yield return null;
            bf.Serialize(fs, blob);
            yield return null;
            fs.Close();
            yield return null;

        }

        public IEnumerator CheckFileAndCreate()
        {
            if (!File.Exists(savepath))
            {
                FileStream fs = File.Create(savepath);
                BinaryFormatter bf = new BinaryFormatter();
                SaveBlob blob = new SaveBlob();
                for (int _ = 0; _ < Constants.CHECKPOINT_COUNT; _++)
                {
                    blob.SaveTimes[_] = -1;
                }
                bf.Serialize(fs, blob);
                fs.Close();
            }
            yield return null;
        }

        public static bool FirstRun()
        {
            return !File.Exists(savepath);
        }

        public static int[] FetchTimes()
        {
            FileStream fs;
            BinaryFormatter bf = new BinaryFormatter();
            if (!File.Exists(savepath))
            {
                fs = File.Create(savepath);
                SaveBlob _blob = new SaveBlob();
                for (int _ = 0; _ < Constants.CHECKPOINT_COUNT; _++)
                {
                    _blob.SaveTimes[_] = -1;
                }
                bf.Serialize(fs, _blob);
                fs.Close();
            }
            fs = File.Open(savepath, FileMode.Open);
            SaveBlob blob = (SaveBlob)bf.Deserialize(fs);
            fs.Close();
            return blob.SaveTimes;

        }
    }
}