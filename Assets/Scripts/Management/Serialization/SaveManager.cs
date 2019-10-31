using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;

namespace Management.Serialization
{
    public class SaveManager : MonoBehaviour
    {
        static string savepath;

        private void Start()
        {
            savepath = Application.persistentDataPath + "/save.gd";
        }

        public IEnumerator SaveCoroutine(CHECKPOINTS check, int time, int collectibles)
        {
            // * get old save
            yield return StartCoroutine(CheckFileAndCreate());
            FileStream fs = File.Open(savepath, FileMode.Open);
            yield return null;
            BinaryFormatter bf = new BinaryFormatter();
            SaveBlob blob = (SaveBlob)bf.Deserialize(fs);
            fs.Close();
            yield return null;

            int index = (int)check - 1;
            // * set new time to save
            if (time < blob.SaveTimes[index] || blob.SaveTimes[index] == -1) blob.SaveTimes[index] = time;
            if (collectibles > blob.SaveCollectibles[index]) blob.SaveCollectibles[(int)check] = collectibles;

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
                for (int _ = 0; _ < Constants.CHECKPOINT_COUNT; _++)
                {
                    blob.SaveCollectibles[_] = 0;
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

        public static SaveBlob FetchTimes()
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
                for (int _ = 0; _ < Constants.CHECKPOINT_COUNT; _++)
                {
                    _blob.SaveCollectibles[_] = 0;
                }
                bf.Serialize(fs, _blob);
                fs.Close();
            }
            fs = File.Open(savepath, FileMode.Open);
            SaveBlob blob = (SaveBlob)bf.Deserialize(fs);
            fs.Close();
            return blob;

        }
    }
}