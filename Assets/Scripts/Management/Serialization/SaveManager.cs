using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;
namespace Management.Serialization
{
    // TODO : need complete overhaul of the save system.
    // TODO : keep only SaveCoroutine and FetchTimes
    // TODO : when opening a file, check for the version ... 
    // TODO : adapt old blob to new version ...
    // TODO : what do I do if deserialize failure ? 

    public class SaveManager : MonoBehaviour
    {

        bool isGameOver;

        static string savepath;

        private void Start()
        {
            savepath = Application.persistentDataPath + "/save.gd";
        }

        /// <summary>
        /// Checks if the save file exists.
        /// If not, creates it.
        /// </summary>
        public static void DoCheckFileAndCreate()
        {
            if (!File.Exists(savepath))
            {
                FileStream fileStream = File.Create(savepath);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                SaveBlobV2 blob = new SaveBlobV2();
                blob.InitEmpty();
                binaryFormatter.Serialize(fileStream, blob);
                fileStream.Close();
            }
        }

        /// <summary>
        /// checks contents of the save file,
        /// updates the save file format if old version detected,
        /// returns save data after operations
        /// </summary>
        /// <returns></returns>
        public static SaveBlobV2 DoCheckVersionAndRectify()
        {
            if (!File.Exists(savepath))
            {
                throw new FileNotFoundException("save file is supposed to exist");
            }
            SaveBlobV2 returnBlob;
            FileStream fileStream = File.Open(savepath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // object saveObject = binaryFormatter.Deserialize(fileStream);
            try
            {
                SaveBlobV2 blob = (SaveBlobV2)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                if (blob.version != Constants.SAVE_VER)
                {
                    // * the save file has wrong version
                    returnBlob = new SaveBlobV2();
                    returnBlob.InitEmpty();
                    for (int _ = 0; _ < Math.Min(blob.NormalTimes.Length, Constants.CHECKPOINT_COUNT); _++)
                    {
                        returnBlob.NormalTimes[_] = blob.NormalTimes[_];
                        returnBlob.NormalCollectibles[_] = blob.NormalCollectibles[_];
                    }
                    for (int _ = 0; _ < Math.Min(blob.MadnessTimes.Length, Constants.MADNESS_LEVEL_COUNT); _++)
                    {
                        returnBlob.MadnessTimes[_] = blob.MadnessTimes[_];
                        returnBlob.MadnessCollectibles[_] = blob.MadnessCollectibles[_];
                    }
                }
                else
                {
                    returnBlob = blob;
                }
            }
            catch (SerializationException)
            {
                // * the file does not contain a saveblobV2
                try
                {
                    SaveBlob blob = (SaveBlob)binaryFormatter.Deserialize(fileStream);
                    fileStream.Close();
                    returnBlob = new SaveBlobV2();
                    returnBlob.InitEmpty();
                    for (int _ = 0; _ < Math.Min(blob.SaveTimes.Length, Constants.CHECKPOINT_COUNT); _++)
                    {
                        returnBlob.NormalTimes[_] = blob.SaveTimes[_];
                        returnBlob.NormalCollectibles[_] = blob.SaveCollectibles[_];
                    }

                }
                catch (SerializationException)
                {
                    // * the file does not coontain a saveblob(V1) either
                    returnBlob = new SaveBlobV2();
                    returnBlob.InitEmpty();
                }
            }
            fileStream.Close();
            fileStream = File.Create(savepath);
            binaryFormatter.Serialize(fileStream, returnBlob);
            fileStream.Close();
            return returnBlob;
        }

        /// <summary>
        /// Overwrites save file with given save blob
        /// </summary>
        /// <param name="blob"> blob object to serialize on save file </param>
        public void DoWriteFile(SaveBlobV2 blob)
        {
            FileStream fileStream = File.Create(savepath);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, blob);
            fileStream.Close();
        }

        public IEnumerator SaveCoroutineNormal(NORMAL_CHECKPOINT check, int time, int collectibles)
        {
            DoCheckFileAndCreate();
            yield return null;
            SaveBlobV2 saveBlob = DoCheckVersionAndRectify();
            yield return null;
            int index = (int)check;
            if (time < saveBlob.NormalTimes[index] || saveBlob.NormalTimes[index] == -1) saveBlob.NormalTimes[index] = time;
            if (collectibles > saveBlob.NormalCollectibles[index]) saveBlob.NormalCollectibles[index] = collectibles;
            yield return null;
            FileStream fileStream = File.Create(savepath);
            new BinaryFormatter().Serialize(fileStream, saveBlob);
            yield return null;
            fileStream.Close();
            yield return null;

        }

        public IEnumerator SaveCoroutineMadness(MADNESS_CHECKPOINT check, int time, int collectibles)
        {
            DoCheckFileAndCreate();
            yield return null;
            SaveBlobV2 saveBlob = DoCheckVersionAndRectify();
            yield return null;
            int index = (int)check;
            if (time < saveBlob.MadnessTimes[index] || saveBlob.MadnessTimes[index] == -1) saveBlob.MadnessTimes[index] = time;
            if (collectibles > saveBlob.MadnessCollectibles[index]) saveBlob.MadnessCollectibles[index] = collectibles;
            yield return null;
            FileStream fileStream = File.Create(savepath);
            new BinaryFormatter().Serialize(fileStream, saveBlob);
            yield return null;
            fileStream.Close();
            yield return null;
        }


        public static bool FirstRun()
        {
            return !File.Exists(savepath);
        }

        public static SaveBlobV2 FetchBlob()
        {
            DoCheckFileAndCreate();
            return DoCheckVersionAndRectify();
        }

        public bool GetIsGameOver()
        {
            return isGameOver;
        }

        public IEnumerator CheckIsGameOver(MADNESS_CHECKPOINT check)
        {
            DoCheckFileAndCreate();
            yield return null;
            SaveBlobV2 saveBlob = DoCheckVersionAndRectify();
            isGameOver = true;
            for (int c = 0; c < saveBlob.MadnessTimes.Length; c++)
            {
                if (c != (int)check && saveBlob.MadnessTimes[c] == -1)
                {
                    isGameOver = false;
                }
            }
            if (saveBlob.MadnessTimes[(int)check] != -1)
            {
                isGameOver = false;
            }
            yield return null;
        }
    }
}