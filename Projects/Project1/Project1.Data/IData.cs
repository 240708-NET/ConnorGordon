using Project1.Models.Actor;
using System.Text.Json;

namespace Project1.Data {
    public interface IData {
        public List<Actor> ReadAllActors;
    }
}