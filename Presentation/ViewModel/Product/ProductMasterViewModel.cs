using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class ProductMasterViewModel : IViewModel, IProductMasterViewModel
{
    public ICommand SwitchToUserMasterPage { get; set; }

    public ICommand SwitchToStateMasterPage { get; set; }

    public ICommand SwitchToEventMasterPage { get; set; }

    public ICommand CreateProduct { get; set; }

    public ICommand RemoveProduct { get; set; }

    private readonly IProductModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private ObservableCollection<IProductDetailViewModel> _products;

    public ObservableCollection<IProductDetailViewModel> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChanged(nameof(Products));
        }
    }

    private string _name;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private double _price;

    public double Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged(nameof(Price));
        }
    }

    private bool _isProductSelected;

    public bool IsProductSelected
    {
        get => _isProductSelected;
        set
        {
            IsProductDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

            _isProductSelected = value;
            OnPropertyChanged(nameof(IsProductSelected));
        }
    }

    private Visibility _isProductDetailVisible;

    public Visibility IsProductDetailVisible
    {
        get => _isProductDetailVisible;
        set
        {
            _isProductDetailVisible = value;
            OnPropertyChanged(nameof(IsProductDetailVisible));
        }
    }

    private IProductDetailViewModel _selectedDetailViewModel;

    public IProductDetailViewModel SelectedDetailViewModel
    {
        get => _selectedDetailViewModel;
        set
        {
            _selectedDetailViewModel = value;
            IsProductSelected = true;

            OnPropertyChanged(nameof(SelectedDetailViewModel));
        }
    }

    public ProductMasterViewModel(IProductModelOperation? model = null, IErrorInformer? informer = null)
    {
        SwitchToUserMasterPage = new SwitchViewCommand("UserMasterView");
        SwitchToStateMasterPage = new SwitchViewCommand("StateMasterView");
        SwitchToEventMasterPage = new SwitchViewCommand("EventMasterView");

        CreateProduct = new OnClickCommand(e => StoreProduct(), c => CanStoreProduct());
        RemoveProduct = new OnClickCommand(e => DeleteProduct());

        Products = new ObservableCollection<IProductDetailViewModel>();

        _modelOperation = model ?? IProductModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();

        IsProductSelected = false;

        Task.Run(this.LoadProducts);
    }

    private bool CanStoreProduct()
    {
        return !(
            string.IsNullOrWhiteSpace(this.Name) ||
            string.IsNullOrWhiteSpace(this.Price.ToString()) ||
            this.Price <= 0 
        );
    }

    private void StoreProduct()
    {
        Task.Run(async () =>
        {
            int lastId = await this._modelOperation.GetCountAsync() + 1;

            await this._modelOperation.AddAsync(lastId, Name, Price);

            LoadProducts();

            _informer.InformSuccess("Product added successfully!");

        });
    }

    private void DeleteProduct()
    {
        Task.Run(async () =>
        {
            try
            {
                await _modelOperation.DeleteAsync(SelectedDetailViewModel.Id);

                LoadProducts();

                _informer.InformSuccess("Product deleted successfully!");
            }
            catch (Exception e)
            {
                _informer.InformError("Error while deleting product! Remember to remove all associated states!");
            }
        });
    }

    private async void LoadProducts()
    {
        Dictionary<int, IProductModel> Products = await _modelOperation.GetAllAsync();

        Application.Current.Dispatcher.Invoke(() =>
        {
            _products.Clear();
            
            foreach (IProductModel p in Products.Values)
            {
                _products.Add(new ProductDetailViewModel(p.Id, p.Name, p.Price));
            }
        });

        OnPropertyChanged(nameof(Products));
    }
}
