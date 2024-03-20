using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using System.Collections.ObjectModel;

namespace Mobile.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
    private readonly LocalDbContext localDbContext;
    private ObservableCollection<Item> Items { get; set; }
    [ObservableProperty]
    private string _name = "Test here";


    public MainPageViewModel(LocalDbContext db)
    {
        localDbContext = db;
        Items = new ObservableCollection<Item>();
    }

    [RelayCommand]
    async Task ChangeText()
    {
        var itm = await localDbContext.Items.Include(i => i.History).ToListAsync();

        //Name = itm.First().History.First().Done.ToString();
        Name = LocalDbContext.GetPath("moj.db");
    }
}