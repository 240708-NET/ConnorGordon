using AA_FileIO.Models;
using System.Text.Json;

namespace AA_FileIO.Repo {
    public class Serialization : IRepository {
        public void ReadAndWriteWithFile(string pPath) {
            
        }

        public void StreamReaderReadLine(string pPath) {
            
        }

        public void StreamReadToEnd(string pPath) {
            
        }

        public List<Duck> ReadDucksFromFile(string pPath) {
            return new List<Duck>();
        }
    
        public void SaveDuck(string pPath, Duck pDuck) {
            //var serializar = new JsonSerializer();
            string sDuck = JsonSerializer.Serialize(pDuck);
            File.WriteAllText(pPath, sDuck);
        }

        public void SaveAllDucks(string pPath, List<Duck> pDucks) {
            string sDucks = JsonSerializer.Serialize(pDucks);
            File.WriteAllText(pPath, sDucks);
        }

        public List<Duck> LoadAllDucks(string pPath) {
            string json = File.ReadAllText(pPath);
            return JsonSerializer.Deserialize<List<Duck>>(json);
        }
    }
}