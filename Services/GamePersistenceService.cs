using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using YouthMeadowGeneralStore.Configuration;
using YouthMeadowGeneralStore.Models;

namespace YouthMeadowGeneralStore.Services
{
    public sealed class GamePersistenceService
    {
        private readonly string _savePath;
        private readonly SaveEncryptionService _encryptionService;
        private readonly DataContractJsonSerializer _serializer;

        public GamePersistenceService(string baseDirectory)
        {
            _savePath = Path.Combine(baseDirectory, GameAppConfig.SaveFileName);
            _encryptionService = new SaveEncryptionService(GameAppConfig.SaveEncryptionKey);
            _serializer = new DataContractJsonSerializer(typeof(GameSaveData));
        }

        public bool SaveExists()
        {
            return File.Exists(_savePath);
        }

        public GameSaveData Load()
        {
            var encrypted = File.ReadAllText(_savePath, Encoding.UTF8);
            var json = _encryptionService.Decrypt(encrypted);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (GameSaveData)_serializer.ReadObject(stream);
            }
        }

        public void Save(GameSaveData saveData)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.WriteObject(stream, saveData);
                var json = Encoding.UTF8.GetString(stream.ToArray());
                var encrypted = _encryptionService.Encrypt(json);
                File.WriteAllText(_savePath, encrypted, Encoding.UTF8);
            }
        }

        public void DeleteSaveIfExists()
        {
            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
            }
        }
    }
}
