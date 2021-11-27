using System.Collections.Generic;

namespace MyGame.GameComponents.World
{
    public class LevelData
    {
        public int Key { get; set; }
        public string LevelName { get; set; }
        public bool Status { get; set; }
    }

    public class PayLoad
    {
        public List<LevelData> LevelData { get; set; }
    }

    public class Root
    {
        public PayLoad PayLoad { get; set; }
    }
}
