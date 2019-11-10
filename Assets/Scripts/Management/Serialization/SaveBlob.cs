namespace Management.Serialization
{
    [System.Serializable]
    public class SaveBlob
    {
        public int[] SaveTimes = new int[Constants.CHECKPOINT_COUNT];

        public int[] SaveCollectibles = new int[Constants.CHECKPOINT_COUNT];
    }
}