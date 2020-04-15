public class Constants
{
    public const int PLAYER_MAX_HP = 4;
    public const int PLAYER_START_HP = 4;
    public const int CHECKPOINT_COUNT = 5;
    public const int MADNESS_LEVEL_COUNT = 4;
    public const int SAVE_VER = 1;
    public const float COYOTE_TIME = .17f;
    public const float INDICATOR_THRESHOLD_DISTANCE = 1800.0f;
}
public enum DIRECTION { LEFT, RIGHT };
public enum NORMAL_CHECKPOINT
{
    FIRST = 0,
    SECOND,
    THIRD,
    FOURTH,
    FIFTH,
}

public enum MADNESS_CHECKPOINT
{
    A = 0,
    B,
    C,
    D
}