using Microsoft.Xna.Framework.Audio;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.Sounds
{
    public class LoadedSoundEffect
    {
        private readonly string soundEffectName;
        private const string SoundEffectFolder = "SoundEffect/";

        public LoadedSoundEffect(string soundEffectName)
        {
            this.soundEffectName = soundEffectName;
        }

        public SoundEffect Get()
        {
            return new GameInstance().Load<SoundEffect>(SoundEffectFolder + soundEffectName);
        }
    }
}
