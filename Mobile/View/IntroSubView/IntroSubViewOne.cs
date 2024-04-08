namespace Mobile.View.IntroSubView;

public class IntroSubViewOne : ContentView
{
	public IntroSubViewOne()
	{
        var stack = new StackLayout { Orientation = StackOrientation.Vertical};
        //stack.Children.Add(new Image
        //    {
        //        Source = "bg1.jpg",
        //        Aspect = Aspect.AspectFill
        //    }
        //);

        var btn = new Button
        {
            Text = "Start",
        };

        btn.Clicked += MyButton_Clicked;

        stack.Children.Add(btn);
       

        void MyButton_Clicked(object sender, EventArgs e)
        {
            if (Application.Current != null) Application.Current.MainPage = new MainTabbedPage();
        }

        Content = stack;
    }

}