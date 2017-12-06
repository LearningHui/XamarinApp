using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TLSLib;
using Xamarin.Forms;

namespace XamarinApp
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async  void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            //var Request = WebRequest.CreateHttp("http://www.zoupenghui.com");
            //var str = await NetworkRequest.CreateHttp("http://www.zoupenghui.com").GetAsync();
            //var content = Encoding.UTF8.GetString(str);
            //Debug.Write(content);
            string htmlContent = await NetworkRequest.CreateHttp("http://www.zoupenghui.com").GetAsync<string>();
            Debug.Write(htmlContent);

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}