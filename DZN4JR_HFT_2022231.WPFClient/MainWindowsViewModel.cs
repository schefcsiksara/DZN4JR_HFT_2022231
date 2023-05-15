using DZN4JR_HFT_2022231.Models.Entities;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TRZJ1J_HFT_2022231.WpfClient;

namespace DZN4JR_HFT_2022231.WPFClient
{
    public class MainWindowsViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Paint> Paints { get; set; }
        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Customer> Customers { get; set; }

        private Paint selectedPaint;
        private Brand selectedBrand;
        private Customer selectedCustomer;

        public Paint SelectedPaint
        {
            get { return selectedPaint; }
            set
            {
                if (value != null)
                {
                    selectedPaint = new Paint()
                    {
                        Id = value.Id,
                        Color = value.Color,
                        Type = value.Type,
                        BasePrice = value.BasePrice,
                        Volume = value.Volume,
                        BrandId = value.BrandId,
                    };
                    OnPropertyChanged();
                    (DeletePaintCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (value != null)
                {
                    selectedBrand = new Brand()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        WholeSalerName = value.WholeSalerName,
                        Country = value.Country,
                        Address = value.Address,
                        Rating = value.Rating
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != null)
                {
                    selectedCustomer = new Customer()
                    {
                        Id = value.Id,
                        CustomerName = value.CustomerName,
                        Adress = value.Adress,
                        Email = value.Email,
                        RegularCustomer = value.RegularCustomer,
                        FavoritePaintId = value.FavoritePaintId,
                    };
                    OnPropertyChanged();
                    (DeleteCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand AddPaintCommand { get; set; }
        public ICommand UpdatePaintCommand { get; set; }
        public ICommand DeletePaintCommand { get; set; }

        public ICommand AddBrandCommand { get; set; }
        public ICommand UpdateBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }

        public ICommand AddCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }

        public MainWindowsViewModel()
        {
            Paints = new RestCollection<Paint>("http://localhost:49044/", "paint", "hub");
            Brands = new RestCollection<Brand>("http://localhost:49044/", "brand", "hub");
            Customers = new RestCollection<Customer>("http://localhost:49044/", "customer", "hub");


            AddPaintCommand = new RelayCommand(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(SelectedPaint.Color))
                    {
                        throw new ApplicationException("Color cannot be empty");
                    }

                    if (SelectedPaint.Color.Length > 50)
                    {
                        throw new ApplicationException("Color cannot be more than 50 characters");
                    }

                    if (SelectedPaint.Type.Length > 20)
                    {
                        throw new ApplicationException("Type cannot be more than 30 characters");
                    }

                    if (SelectedPaint.Volume.Length > 20)
                    {
                        throw new ApplicationException("Volume cannot be more than 30 characters");
                    }

                    Paints.Add(SelectedPaint);
                }
                catch (ApplicationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });
            UpdatePaintCommand = new RelayCommand(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(SelectedPaint.Color))
                    {
                        throw new ApplicationException("Color cannot be empty");
                    }

                    if (SelectedPaint.Color.Length > 50)
                    {
                        throw new ApplicationException("Color cannot be more than 50 characters");
                    }

                    if (SelectedPaint.Type.Length > 20)
                    {
                        throw new ApplicationException("Type cannot be more than 30 characters");
                    }

                    if (SelectedPaint.Volume.Length > 20)
                    {
                        throw new ApplicationException("Volume cannot be more than 30 characters");
                    }

                    Paints.Update(SelectedPaint);
                }
                catch (ApplicationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });


            AddBrandCommand = new RelayCommand(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(SelectedBrand.Name))
                    {
                        throw new ApplicationException("Brand name cannot be empty");
                    }

                    if (SelectedBrand.Name.Length > 30)
                    {
                        throw new ApplicationException("Brand name too long. Max characters: 30");
                    }

                    if (SelectedBrand.WholeSalerName.Length > 30)
                    {
                        throw new ApplicationException("WholeSalerName too long. Max characters: 30");
                    }

                    if (SelectedBrand.Country.Length > 30)
                    {
                        throw new ApplicationException("Country too long. Max characters: 30");
                    }

                    if (SelectedBrand.Address.Length > 30)
                    {
                        throw new ApplicationException("Address too long. Max characters: 30");
                    }

                    Brands.Add(SelectedBrand);
                }
                catch (ApplicationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });
            UpdateBrandCommand = new RelayCommand(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(SelectedBrand.Name))
                    {
                        throw new ApplicationException("Brand name cannot be empty");
                    }

                    if (SelectedBrand.Name.Length > 30)
                    {
                        throw new ApplicationException("Brand name too long. Max characters: 30");
                    }

                    if (SelectedBrand.WholeSalerName.Length > 30)
                    {
                        throw new ApplicationException("WholeSalerName too long. Max characters: 30");
                    }

                    if (SelectedBrand.Country.Length > 30)
                    {
                        throw new ApplicationException("Country too long. Max characters: 30");
                    }

                    if (SelectedBrand.Address.Length > 30)
                    {
                        throw new ApplicationException("Address too long. Max characters: 30");
                    }

                    Brands.Update(SelectedBrand);
                }
                catch (ApplicationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });



            AddCustomerCommand = new RelayCommand(() =>
            {
                try
                {
                    Customers.Add(SelectedCustomer);
                }
                catch (ApplicationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });

            UpdateCustomerCommand = new RelayCommand(() =>
            {
                try
                {
                    Customers.Update(SelectedCustomer);
                }
                catch (ApplicationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });


            DeletePaintCommand = new RelayCommand(() =>
            {
                try
                {
                    Paints.Delete(SelectedPaint.Id);
                }
                catch (ArgumentException ex)
                {
                    ErrorMessage = ex.Message;
                }
            });

            DeleteBrandCommand = new RelayCommand(() =>
            { Brands.Delete(SelectedBrand.Id); }, () => { return SelectedBrand != null; }
            );
            DeleteCustomerCommand = new RelayCommand(() =>
            { Customers.Delete(SelectedCustomer.Id); }, () => { return SelectedCustomer != null; }
            );

            SelectedPaint = new Paint();
            SelectedBrand = new Brand();
            SelectedCustomer = new Customer();
        }
    }
}
