using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace MyGame.Audio
{
    public enum AudioEffectType
    {
    }

    public static class GameSound
    {
        private static Song _music;

        static GameSound()
        {
        }

        public static void Load(ContentManager content)
        {
           _music = content.Load<Song>("Sound/Musics/MusicMain");
        }

        public static void SetVolume(float value)
        {
            MediaPlayer.Volume = value;
        }

        public static void PlayMusic()
        {
            if (_music != null)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(_music);
            }
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public static void PlayEffect(AudioEffectType type)
        {
        }
    }
}
