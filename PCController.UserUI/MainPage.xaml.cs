using PCController.UserUI.Models;

namespace PCController.UserUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private ImageButton MouseImageButton;
        private void ImageButton_Loaded(object sender, EventArgs e)
        {
            MouseImageButton = sender as ImageButton;
        }
        private void OnMousePanUpdated(object sender, PanUpdatedEventArgs e)
        {
            MouseImageButton.TranslationX = e.TotalX;
            MouseImageButton.TranslationY = e.TotalY;
        }

        private async void MouseOuterArea_Tapped(object sender, EventArgs e)
        {
            await MousePanLayoutAnimation();
            Console.WriteLine("鼠标右键单击");
        }

        private async void MouseOuterArea_Tapped2(object sender, EventArgs e)
        {
            await MousePanLayoutAnimation();
            Console.WriteLine("鼠标右键单双击");
        }

        private async void MousePan_Tapped2(object sender, EventArgs e)
        {
            await MousePanAnimation();
            Console.WriteLine("鼠标左键双击");
        }

        private async void MousePan_Tapped(object sender, EventArgs e)
        {
            await MousePanAnimation();
            Console.WriteLine("鼠标左键单击");
        }
        private async Task MousePanAnimation()
        {
            await MouseImageButton.ScaleTo(1.1, 50);
            await MouseImageButton.ScaleTo(1, 50);
        }
        private async Task MousePanLayoutAnimation()
        {
            await MousePanLayout.ScaleTo(1.1, 50);
            await MousePanLayout.ScaleTo(1, 50);
        }

        private HorizontalStackLayout MousePanLayout;
        private void HorizontalStackLayout_Loaded(object sender, EventArgs e)
        {
            MousePanLayout = sender as HorizontalStackLayout;
        }
    }
}