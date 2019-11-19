using BlueScreenCategoriser;
using Utils;

namespace BlueScreenIO.IOInterface {
	public interface StorageInterface {
		m_Bool Move(string newPath);
		m_Bool Remove();
		m_Bool ChangeDetails(EpisodeInfo e);
		m_Bool ChangeTarget(string path);
	}
}