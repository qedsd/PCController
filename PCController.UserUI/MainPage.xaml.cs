namespace PCController.UserUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ImageButton_Pressed(object sender, EventArgs e)
        {
            (sender as ImageButton).WidthRequest = 120;
            (sender as ImageButton).HeightRequest = 120;
        }

        private void ImageButton_Released(object sender, EventArgs e)
        {
            (sender as ImageButton).WidthRequest = 100;
            (sender as ImageButton).HeightRequest = 100;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("1", "1","1");
        }

        private void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            //DisplayAlert("2", "2", "2");
        }

        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            Console.WriteLine("111");
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Console.WriteLine(e.TotalX);
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}