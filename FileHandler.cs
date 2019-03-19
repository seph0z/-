using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace BookList
{
    public class FileHandler : IFileHandler<Book>
    {
        private readonly DataContractJsonSerializer dataJsonSerializer =
            new DataContractJsonSerializer(typeof(List<Book>));
        private readonly string path = @"D:\JsonSaved.json";

        public List<Book> Load()
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (List<Book>)dataJsonSerializer.ReadObject(fs);
                }
            }
            return new List<Book>();
        }

        public void Save(List<Book> books)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                dataJsonSerializer.WriteObject(fs,books);
            }
        }
    }
}
