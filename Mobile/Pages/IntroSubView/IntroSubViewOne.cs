using Microsoft.Extensions.DependencyInjection;
using Mobile.ViewModel;

namespace Mobile.Pages.IntroSubView;

public class IntroSubViewOne : ContentView
{
    private readonly TasksViewModel _vm;

    public IntroSubViewOne(TasksViewModel vm)
    {
        _vm = vm;
        var stack = new StackLayout { Orientation = StackOrientation.Vertical };
        

        var btn = new Button
        {
            Text = "Start",
        };

        btn.Clicked += MyButton_Clicked;

        stack.Children.Add(btn);


        void MyButton_Clicked(object sender, EventArgs e)
        {
            if (Application.Current != null) Application.Current.MainPage = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TasksPage>(); // pobraæ z sp
        }

        Content = stack;
        _vm = vm;
    }

}