namespace Management.Serialization
{
    // [System.Serializable, System.Obsolete("use V2 instead!")]
    public class SaveBlob
    {
        public int[] SaveTimes = new int[Constants.CHECKPOINT_COUNT];

        public int[] SaveCollectibles = new int[Constants.CHECKPOINT_COUNT];
    }
}