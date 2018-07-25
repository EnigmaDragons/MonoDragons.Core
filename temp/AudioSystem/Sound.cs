using System;

namespace MonoDragons.Core.AudioSystem
{
    public class Sound : IDisposable
    {
        private readonly SmartSample _smartSample;

        private bool _initialized = false;
        private float _volume;

        public bool IsPlaying { get; set; } = false;
        public bool Looping { get; set; }
        public bool Mute { get; set; }
        public SoundType Type { get; set; }

        public Sound Music(string fileName, float volume = 1) => new Sound(fileName, true, false, volume, SoundType.Music);
        public Sound Ambient(string fileName, float volume = 1) => new Sound(fileName, true, false, volume, SoundType.Ambient);
        public Sound SoundEffect(string fileName, float volume = 1) => new Sound(fileName, false, false, volume, SoundType.Effect);

        public Sound(string fileName, bool looping, bool mute, float volume, SoundType type)
        {
            _smartSample = new SmartSample(this, fileName);
            Looping = looping;
            Mute = mute;
            Volume = volume;
            Type = type;
        }

        public float Volume
        {
            get => _volume;
            set => _volume = Math.Min(1, Math.Max(0, value));
        }

        public void Play()
        {
            if (!_initialized)
            {
                AudioPlayer.Instance.Play(_smartSample);
                _initialized = true;
            }
            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Reset()
        {
            _smartSample.Reset();
        }

        public void Dispose()
        {
            _smartSample.Dispose();
        }
    }
}
