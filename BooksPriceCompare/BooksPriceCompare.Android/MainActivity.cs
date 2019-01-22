using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using System.Linq;
using Android.Content;
using Android.Net;

namespace BooksPriceCompare.Droid
{
    [Activity(Label = "BooksPriceCompare",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        BooksViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Toolbar);

            _viewModel = new BooksViewModel();
            Button button = FindViewById<Button>(Resource.Id.myButton);
            var list = FindViewById<ListView>(Resource.Id.listView1);

            button.Click += async delegate
            {
                button.Enabled = false;
                var bookName = FindViewById<EditText>(Resource.Id.bookTitle).Text;


                await _viewModel.GetBooksAsAsync(bookName);

                list.Adapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleListItem1,
                    Android.Resource.Id.Text1,
                    _viewModel.AllegroBooksResponses.Select(m => $"Nazwa aukcji: {m.auctionName}\n" +
                                                                 $"Numer aukcji: {m.auctionNumber}\n" +
                                                                 $"Cena produktu: {m.productPrice} zł\n" +
                                                                 $"Cena dostawy: {m.lowestPriceDelivery}\n"
                    ).ToArray());

                button.Enabled = true;
            };
        }
    }
}