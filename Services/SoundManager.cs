using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Vorbis;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace YouthMeadowGeneralStore.Services
{
    public sealed class SoundManager : IDisposable
    {
        private const int MixerSampleRate = 44100;
        private readonly string _soundDirectory;
        private readonly Dictionary<string, string> _effects;
        private readonly object _syncRoot = new object();
        private readonly WaveOutEvent _outputDevice;
        private readonly MixingSampleProvider _mixer;
        private ISampleProvider _backgroundInput;
        private IDisposable _backgroundResource;
        private VolumeSampleProvider _backgroundVolumeProvider;

        public SoundManager()
        {
            _soundDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds");
            Volume = 50;
            _mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(MixerSampleRate, 2))
            {
                ReadFully = true
            };
            _outputDevice = new WaveOutEvent
            {
                DesiredLatency = 100
            };
            _outputDevice.Init(_mixer);
            _effects = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["startup"] = GetPath("开业.wav"),
                ["purchase"] = GetPath("购入.wav"),
                ["shelf"] = GetPath("上架.wav"),
                ["Discard"] = GetPath("丢弃.wav"),
                ["business"] = GetPath("营业.wav"),
                ["result_a"] = GetPath("结果a.wav"),
                ["result_b"] = GetPath("结果b.wav"),
                ["result_c"] = GetPath("结果c.wav"),
                ["select_a"] = GetPath("选择a.wav"),
                ["select_b"] = GetPath("选择b.wav")
            };
        }

        public int Volume { get; private set; }

        public void PlayEffect(string name)
        {
            if (!_effects.TryGetValue(name, out var path) || !File.Exists(path))
            {
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    lock (_syncRoot)
                    {
                        EnsureMixerPlayback();
                        _mixer.AddMixerInput(CreateEffectInput(path));
                    }
                }
                catch
                {
                }
            });
        }

        public void PlayStartupSequenceBlocking()
        {
            if (!_effects.TryGetValue("startup", out var path) || !File.Exists(path))
            {
                return;
            }

            PlayOneShot(path);
        }

        public void PlayBackground(string fileName)
        {
            var path = GetPath(fileName);
            if (!File.Exists(path))
            {
                return;
            }

            lock (_syncRoot)
            {
                StopBackgroundPlayback();
                EnsureMixerPlayback();

                _backgroundResource = new LoopStream(CreateReader(path));
                _backgroundVolumeProvider = new VolumeSampleProvider(ConvertToMixerFormat(((WaveStream)_backgroundResource).ToSampleProvider()))
                {
                    Volume = Volume / 100f
                };
                _backgroundInput = _backgroundVolumeProvider;
                _mixer.AddMixerInput(_backgroundInput);
            }
        }

        public void AdjustVolume(int delta)
        {
            Volume = Math.Max(0, Math.Min(100, Volume + delta));
            lock (_syncRoot)
            {
                if (_backgroundVolumeProvider != null)
                {
                    _backgroundVolumeProvider.Volume = Volume / 100f;
                }
            }
        }

        public bool BackgroundExists(string fileName)
        {
            return File.Exists(GetPath(fileName));
        }

        public string GetPath(string fileName)
        {
            return Path.Combine(_soundDirectory, fileName ?? string.Empty);
        }

        private void PlayOneShot(string path)
        {
            try
            {
                using (var reader = CreateReader(path))
                using (var output = new WaveOutEvent())
                {
                    var timeoutAt = DateTime.UtcNow.Add(reader.TotalTime).Add(TimeSpan.FromSeconds(2));
                    output.Init(reader);
                    output.Play();
                    while (output.PlaybackState != PlaybackState.Stopped && DateTime.UtcNow < timeoutAt)
                    {
                        Thread.Sleep(50);
                    }

                    output.Stop();
                }
            }
            catch
            {
            }
        }

        private void EnsureMixerPlayback()
        {
            if (_outputDevice.PlaybackState != PlaybackState.Playing)
            {
                _outputDevice.Play();
            }
        }

        private ISampleProvider CreateEffectInput(string path)
        {
            var reader = CreateReader(path);
            var input = ConvertToMixerFormat(reader.ToSampleProvider());
            return new AutoDisposeSampleProvider(input, reader);
        }

        private static ISampleProvider ConvertToMixerFormat(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == 1)
            {
                input = new MonoToStereoSampleProvider(input);
            }

            if (input.WaveFormat.SampleRate != MixerSampleRate)
            {
                input = new WdlResamplingSampleProvider(input, MixerSampleRate);
            }

            if (input.WaveFormat.Channels == 1)
            {
                input = new MonoToStereoSampleProvider(input);
            }

            return input;
        }

        private static WaveStream CreateReader(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var header = new byte[4];
                var read = stream.Read(header, 0, header.Length);
                if (read == 4 &&
                    header[0] == (byte)'O' &&
                    header[1] == (byte)'g' &&
                    header[2] == (byte)'g' &&
                    header[3] == (byte)'S')
                {
                    return new VorbisWaveReader(path);
                }
            }

            var extension = Path.GetExtension(path)?.ToLowerInvariant();
            switch (extension)
            {
                case ".mp3":
                    return new MediaFoundationReader(path);
                case ".wav":
                default:
                    return new WaveFileReader(path);
            }
        }

        private void StopBackgroundPlayback()
        {
            if (_backgroundInput != null)
            {
                _mixer.RemoveMixerInput(_backgroundInput);
            }

            _backgroundVolumeProvider = null;
            _backgroundInput = null;
            _backgroundResource?.Dispose();
            _backgroundResource = null;
        }

        public void Dispose()
        {
            lock (_syncRoot)
            {
                StopBackgroundPlayback();
                _outputDevice.Stop();
                _outputDevice.Dispose();
            }
        }

        private sealed class LoopStream : WaveStream
        {
            private readonly WaveStream _sourceStream;

            public LoopStream(WaveStream sourceStream)
            {
                _sourceStream = sourceStream;
            }

            public override WaveFormat WaveFormat => _sourceStream.WaveFormat;

            public override long Length => long.MaxValue;

            public override long Position
            {
                get => _sourceStream.Position;
                set => _sourceStream.Position = value;
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                var totalBytesRead = 0;
                while (totalBytesRead < count)
                {
                    var bytesRead = _sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                    if (bytesRead == 0)
                    {
                        _sourceStream.Position = 0;
                        bytesRead = _sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                        if (bytesRead == 0)
                        {
                            break;
                        }
                    }

                    totalBytesRead += bytesRead;
                }

                return totalBytesRead;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _sourceStream.Dispose();
                }

                base.Dispose(disposing);
            }
        }

        private sealed class AutoDisposeSampleProvider : ISampleProvider
        {
            private readonly ISampleProvider _source;
            private readonly IDisposable _resource;
            private int _disposed;

            public AutoDisposeSampleProvider(ISampleProvider source, IDisposable resource)
            {
                _source = source;
                _resource = resource;
            }

            public WaveFormat WaveFormat => _source.WaveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                if (_disposed != 0)
                {
                    return 0;
                }

                var read = _source.Read(buffer, offset, count);
                if (read == 0)
                {
                    DisposeOnce();
                }

                return read;
            }

            private void DisposeOnce()
            {
                if (Interlocked.Exchange(ref _disposed, 1) == 0)
                {
                    _resource.Dispose();
                }
            }
        }
    }
}
