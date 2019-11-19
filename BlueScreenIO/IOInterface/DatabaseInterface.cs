using BlueScreenCategoriser;
using Utils;

namespace BlueScreenIO.IOInterface
{
    public class DatabaseInterface : StorageInterface {
        private string path;

        public DatabaseInterface(string path) {
            this.path = path;
        }

        public m_Bool Move(string newPath) {
            //todo
            throw new System.NotImplementedException();
        }

        public m_Bool Remove() {
            //todo
            throw new System.NotImplementedException();
        }

        public m_Bool ChangeDetails(EpisodeInfo e) {
            //todo
            throw new System.NotImplementedException();
        }

        public m_Bool ChangeTarget(string path) {
            //todo
            throw new System.NotImplementedException();
        }
    }
}