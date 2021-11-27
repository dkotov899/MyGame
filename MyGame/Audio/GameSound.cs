using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;

namespace MyGame.Audio
{
    public enum SoundEffectType
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
           _music = content.Load<Song>("Sound/Songs/Invitation");
        }

        public static void SetVolume(float value)
        {
            MediaPlayer.Volume = value;
        }

        public static void PlayMusic()
        {
            if (_music != null)
            {
                MediaPlayer.Play(_music);
                MediaPlayer.IsRepeating = true;
            }
        }

        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }

        public static void PlayEffect(SoundEffectType type)
        {
            switch (type)
            {
 
            }
        }
    }
}
