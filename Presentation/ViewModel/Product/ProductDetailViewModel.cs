using System.Threading.Tasks;
using System.Windows.Input;
using Presentation.Model.API;

namespace Presentation.ViewModel;

internal class ProductDetailViewModel : IViewModel, IProductDetailViewModel
{
    public ICommand UpdateProduct { get; set; }

    private readonly IProductModelOperation _modelOperation;

    private readonly IErrorInformer _informer;

    private int _id;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
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

    public ProductDetailViewModel(IProductModelOperation? model = null, IErrorInformer? informer = null)
    {
        UpdateProduct = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? IProductModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    public ProductDetailViewModel(int id, string name, double price, IProductModelOperation? model = null, IErrorInformer? informer = null)
    {
        Id = id;
        Name = name;
        Price = price;

        UpdateProduct = new OnClickCommand(e => Update(), c => CanUpdate());

        _modelOperation = model ?? IProductModelOperation.CreateModelOperation();
        _informer = informer ?? new PopupErrorInformer();
    }

    private void Update()
    {
        Task.Run(() =>
        {
            _modelOperation.UpdateAsync(Id, Name, Price);

            _informer.InformSuccess("Product successfully updated!");
        });
    }

    private bool CanUpdate()
    {
        return !(
            string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Price.ToString()) ||
            Price == 0
                    );
    }
}
