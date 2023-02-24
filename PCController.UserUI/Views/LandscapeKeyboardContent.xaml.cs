using PCController.Core.Models;
using PCController.UserUI.Controllers;

namespace PCController.UserUI.Views;

public partial class LandscapeKeyboardContent : ContentView
{
	public LandscapeKeyboardContent()
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
        Down(key.Code);
    }

    private void Button_Released(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 1;
        var key = (sender as Button).BindingContext as Core.Models.KeyboardItem;
        Up(key.Code);
    }
    #endregion

    #region 带符号的数字键盘
    private void Button2_Pressed(object sender, EventArgs e)
    {
        var button = sender as Button;
        button.Opacity = 0.5;
        var key = button.BindingContext;
        Down(key as string);
    }
    private void Button2_Released(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0;
        var key = (sender as Button).BindingContext;
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
        Down(key as string);
    }
    private void Button3_Released(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 1;
        var key = (sender as Button).BindingContext;
        Up(key as string);
    }
    #endregion

    #region ImageButton
    private void Button4_Pressed(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        button.Opacity = 0.4;
        var key = button.BindingContext;
        Down(key as string);
    }

    private void Button4_Released(object sender, EventArgs e)
    {
        (sender as ImageButton).Opacity = 1;
        var key = (sender as ImageButton).BindingContext;
        Up(key as string);
    }
    #endregion

    #region 多媒体按键
    private void Button5_Pressed(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        button.Opacity = 0.4;
        var key = button.BindingContext as KeyboardItem;
        Down(key.Code);
    }

    private void Button5_Released(object sender, EventArgs e)
    {
        (sender as ImageButton).Opacity = 1;
        var key = (sender as ImageButton).BindingContext as KeyboardItem;
        Up(key.Code);
    }
    #endregion

    private void Down(string code)
    {
        KeyboardControl.Down(new Core.Models.KeyboardItem() { Code = code });
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        Console.WriteLine($"按下{code}");
    }
    private void Up(string code)
    {
        KeyboardControl.Up(new Core.Models.KeyboardItem() { Code = code });
        Console.WriteLine($"松开{code}");
    }

    private void Label_Loaded(object sender, EventArgs e)
    {
        (sender as Label).Text = "{";
    }
}