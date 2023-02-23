using PCController.UserUI.Controllers;

namespace PCController.UserUI.Views;

public partial class PortraitKeyboardContent : ContentView
{
    public PortraitKeyboardContent()
	{
		InitializeComponent();
	}
    #region 符号
    private void Button_Pressed(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.4;
        var key = (sender as Button).BindingContext as Core.Models.KeyboardItem;
#if NET7_0_OR_GREATER
        ToolTipProperties.SetText(sender as Button, key.Name);
#endif
        Console.WriteLine(key.Name);
        Down(key.Code);
    }

    private void Button_Released(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 1;
        var key = (sender as Button).BindingContext as Core.Models.KeyboardItem;
        Console.WriteLine(key.Name);
        Up(key.Code);
    }
    #endregion

    #region 带符号的数字键盘
    private void Button2_Pressed(object sender, EventArgs e)
    {
        var button = sender as Button;
        button.Opacity = 0.5;
        var key = button.BindingContext;
        Console.WriteLine(key);
        Down(key as string);
    }
    private void Button2_Released(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0;
        var key = (sender as Button).BindingContext;
        Console.WriteLine(key);
        Up(key as string);
    }
    #endregion

    #region 字母按键
    private void Button3_Pressed(object sender, EventArgs e)
    {
        var button = sender as Button;
        button.Opacity = 0.5;
        var key = button.BindingContext;
#if NET7_0_OR_GREATER
        ToolTipProperties.SetText(button, button.Text);
#endif
        Console.WriteLine(key);
        Down(key as string);
    }
    private void Button3_Released(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 1;
        var key = (sender as Button).BindingContext;
        Console.WriteLine(key);
        Up(key as string);
    }
    #endregion

    #region ImageButton
    private void Button4_Pressed(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        button.Opacity = 0.4;
        var key = button.BindingContext;
        Console.WriteLine(key);
        Down(key as string);
    }

    private void Button4_Released(object sender, EventArgs e)
    {
        (sender as ImageButton).Opacity = 1;
        var key = (sender as ImageButton).BindingContext;
        Console.WriteLine(key);
        Up(key as string);
    }
    #endregion

    private void Down(string code)
    {
        KeyboardControl.Down(new Core.Models.KeyboardItem() { Code = code });
        //Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1));
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        Console.WriteLine($"按下{code}");
    }
    private void Up(string code)
    {
        KeyboardControl.Up(new Core.Models.KeyboardItem() { Code = code });
        Console.WriteLine($"松开{code}");
    }
}