using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BooksPriceCompare
{
    public class AllegroBooksResponse
    {
        public string auctionName { get; set; }
        public string auctionNumber { get; set; }
        public string productPrice { get; set; }
        public string lowestPriceDelivery { get; set; }
        public string auctionImage { get; set; }
    }


    public class BooksViewModel
    {
        public ObservableCollection<AllegroBooksResponse> AllegroBooksResponses { get; set; }

        public async Task GetBooksAsAsync(String bookName)
        {
            try
            {
                AllegroBooksResponses = new ObservableCollection<AllegroBooksResponse>();
                var client = new HttpClient();
                var json = await client.GetStringAsync(
                    "http://book-finder-krisswoj.herokuapp.com/afp?code=" + bookTitleConverter(bookName));

                var items = JsonConvert.DeserializeObject<List<AllegroBooksResponse>>(json);

                foreach (var item in items)
                    AllegroBooksResponses.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private String bookTitleConverter(String bookName)
        {
            return bookName.Replace(" ", "+");
        }
    }
}