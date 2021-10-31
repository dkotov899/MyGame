using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MyGame.Audio
{
    public class Sound
    {
        private Song _music;
        private ContentManager _content;

        public Song Music
        {
            get { return _music; }
        }

        public Sound()
        {

        }

        public void LoadContent(ContentManager content)
        {
            _content = content;
            _music = _content.Load<Song>("Musics/RainbowLollipop");

            MediaPlayer.Play(_music);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        private void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            MediaPlayer.Volume -= 0.1f;
        }
    }
}
