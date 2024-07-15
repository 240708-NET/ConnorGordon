using AA_FileIO.Models;
using System.Text.Json;

namespace AA_FileIO.Repo {
    public interface IRepository {
        public void ReadAndWriteWithFile(string pPath);
        public void StreamReaderReadLine(string pPath);
        public void StreamReadToEnd(string pPath);

        public List<Duck> ReadDucksFromFile(string pPath);
        public void SaveDuck(string pPath, Duck pDuck);
        public void SaveAllDucks(string pPath, List<Duck> pDuck);
        public List<Duck> LoadAllDucks(string pPath);
    }
}