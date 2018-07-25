using System;

namespace MonoDragons.Core.AudioSystem
{
    public class MasterVolume
    {
        private static float _musicVolume;
        private static float _soundEffectVolume;
        private static float _ambientVolume;

        public static float MusicVolume
        {
            get => _musicVolume;
            set => _musicVolume = Math.Min(1, Math.Max(0, value));
        }

        public static float SoundEffectVolume
        {
            get => _soundEffectVolume;
            set => _soundEffectVolume = Math.Min(1, Math.Max(0, value));
        }

        public static float AmbienceVolume
        {
            get => _ambientVolume;
            set => _ambientVolume = Math.Min(1, Math.Max(0, value));
        }

        public static float GetVolume(SoundType type)
        {
            if (type == SoundType.Music)
                return MusicVolume;
            if (type == SoundType.Ambient)
                return AmbienceVolume;
            return SoundEffectVolume;
        }
    }
}
