using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BooksPriceCompare
{
    public class Book
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public int Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class BooksViewModel
    {
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        public async Task GetBooksAsAsync()
        {
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");

                var items = JsonConvert.DeserializeObject<List<Book>>(json);

                foreach (var item in items)
                    Books.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}