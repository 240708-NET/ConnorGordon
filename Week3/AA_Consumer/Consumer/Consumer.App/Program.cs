using System;
using System.Net.Http;
using System.Text.Json;
using Consumer.Models;

namespace Consumer.App {
    public class Program {
        public static async Task Main(string[] args) {
            Console.WriteLine("Http client is starting...");

            HttpClient client = new HttpClient();
            string path = "https://jsonplaceholder.typicode.com/posts/";
            string response = await client.GetStringAsync(path);
            List<Post> postList = JsonSerializer.Deserialize<List<Post>>(response);
            List<List<Post>> userPostList = new List<List<Post>>();

            foreach(var post in postList) {
                Console.WriteLine(post.UserId);
                if (post.UserId > userPostList.Count) {
                    userPostList.Add(new List<Post>());
                }

                userPostList[post.UserId-1].Add(post);
            }

            for(int y = 0; y < userPostList.Count; y++) {
                PrintPostList(client, path, userPostList[y]);
            }
        }

        public static async void PrintPostList(HttpClient pClient, string pPath, List<Post> pList) {
            Console.WriteLine($"User {pList[0].UserId}");
            Console.WriteLine($"------------------------------");

            for(int x = 0; x < pList.Count; x++) {
                Console.WriteLine($"{pList[x].id} {pList[x].title}");
                Console.WriteLine($"{pList[x].body}");
                Console.WriteLine("");
            }
        }
    }
}