using UnityEngine;
using Zenject;

namespace CameraTrajector.Client
{
    public sealed class RecordingsInstaller : MonoInstaller
    {
        [SerializeField] private RecordingsEntry _recordingEntry;

        public override void InstallBindings()
        {
            Container
                .Bind<IRecordings>()
                .To<RecordingsProperty>()
                .AsSingle();

            Container
                .BindFactory<RecordingsEntry, RecordingsEntry.Factory>()
                .FromComponentInNewPrefab(_recordingEntry)
                .AsSingle();
        }
    }
}