namespace Management.Serialization
{
    [System.Serializable]
    public class SaveBlobV2
    {
        public int version = Constants.SAVE_VER;

        public int[] NormalTimes = new int[Constants.CHECKPOINT_COUNT];

        public int[] NormalCollectibles = new int[Constants.CHECKPOINT_COUNT];

        public int[] MadnessTimes = new int[Constants.MADNESS_LEVEL_COUNT];

        public int[] MadnessCollectibles = new int[Constants.MADNESS_LEVEL_COUNT];

        public void InitEmpty()
        {
            version = Constants.SAVE_VER;
            for (int _ = 0; _ < Constants.CHECKPOINT_COUNT; _++)
            {
                NormalTimes[_] = -1;
                NormalCollectibles[_] = 0;
            }
            for (int _ = 0; _ < Constants.MADNESS_LEVEL_COUNT; _++)
            {
                MadnessTimes[_] = -1;
                MadnessCollectibles[_] = 0;
            }
        }

    }

}