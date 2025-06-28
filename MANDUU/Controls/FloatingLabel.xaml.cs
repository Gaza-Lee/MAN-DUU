namespace MANDUU.Controls;

public partial class FloatingLabel : ContentView
{
	public FloatingLabel()
	{
		InitializeComponent();
	}

    #region Bindable Properties

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(FloatingLabel),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: OnTextChangedStatic);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(FloatingLabel),
            string.Empty,
            propertyChanged: OnPlaceholderChanged);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    #endregion

    #region PropertyChanged Callbacks

    private static void OnTextChangedStatic(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (FloatingLabel)bindable;
        var newText = (string)newValue;
        control.InputEntry.Text = newText;
        control.UpdateFloatingLabel(newText);
    }

    private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (FloatingLabel)bindable;
        control.FloatingTextLabel.Text = (string)newValue;
    }

    #endregion

    #region Entry Event Handlers

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Text = e.NewTextValue;
        UpdateFloatingLabel(e.NewTextValue);
    }

    private void OnFocused(object sender, FocusEventArgs e)
    {
        AnimateFloatingLabel(up: true);
    }

    private void OnUnfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InputEntry.Text))
            AnimateFloatingLabel(up: false);
    }

    #endregion

    #region Animation Logic

    private void UpdateFloatingLabel(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            AnimateFloatingLabel(false);
        else
            AnimateFloatingLabel(true);
    }

    private void AnimateFloatingLabel(bool up)
    {
        double targetY = up ? 0 : 20;
        double targetFontSize = up ? 12 : 14;

        FloatingTextLabel.TranslateTo(0, targetY, 150, Easing.CubicOut);
        Device.StartTimer(TimeSpan.FromMilliseconds(150), () =>
        {
            FloatingTextLabel.FontSize = targetFontSize;
            return false; // don't repeat
        });
    }

    #endregion
}