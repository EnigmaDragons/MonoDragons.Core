using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MonoDragons.Core.AudioSystem
{
    internal class AudioPlayer : IDisposable
    {
        public static readonly AudioPlayer Instance = new AudioPlayer();

        private readonly IWavePlayer _player;
        private readonly MixingSampleProvider _mixer;

        public AudioPlayer(int sampleRate = 44100)
        {
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 2)) {ReadFully = true};
            _player = new WaveOutEvent();
            _player.Init(_mixer);
            _player.Play();
        }

        public void Play(ISampleProvider samples)
        {
            _mixer.AddMixerInput(samples);
        }

        public void StopAll()
        {
            _mixer.RemoveAllMixerInputs();
        }

        public void Dispose()
        {
            _player.Dispose();
        }
    }
}
