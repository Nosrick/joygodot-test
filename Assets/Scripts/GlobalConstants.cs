using Godot;
using JoyLib.Code.Helpers;

namespace JoyLib.Code
{
    public static class GlobalConstants
    {
        public const string GODOT_ASSETS_FOLDER = "res://Assets/";
        public const string ASSETS_FOLDER = "/Assets/";
        public const string SPRITES_FOLDER = "Sprites/";
        public const string DATA_FOLDER = "Data/";
        public const string SCRIPTS_FOLDER = DATA_FOLDER + "Scripts/";
        public const string SETTINGS_FOLDER = DATA_FOLDER + "Settings/";

        public static readonly bool IS_EDITOR = Engine.EditorHint;
        
        public const int MAX_LIGHT = 32;

        public const int SPRITE_SIZE = 16;
        public const int DEFAULT_SUCCESS_THRESHOLD = 7;
        public const int MINIMUM_SUCCESS_THRESHOLD = 4;
        public const int MAXIMUM_SUCCESS_THRESHOLD = 9;

        public const int MINIMUM_VISION_DISTANCE = 3;
        
        public const int FRAMES_PER_SECOND = 5;

        public static IGameManager GameManager { get; set; }

        public static ActionLog ActionLog { get; set; }
    }
}
