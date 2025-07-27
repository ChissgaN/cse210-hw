using System;
using System.Collections.Generic;

class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        return _country.Trim().ToLower() == "usa";
    }

    public string GetFormattedAddress()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}

class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string GetName() => _name;

    public Address GetAddress() => _address;

    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}

class Product
{
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string GetPackingInfo()
    {
        return $"{_name} (ID: {_productId})";
    }

    public double GetTotalCost()
    {
        return _price * _quantity;
    }
}

class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetTotalPrice()
    {
        double productTotal = 0;
        foreach (Product product in _products)
        {
            productTotal += product.GetTotalCost();
        }

        double shipping = _customer.LivesInUSA() ? 5 : 35;
        return productTotal + shipping;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in _products)
        {
            label += $"- {product.GetPackingInfo()}\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddress().GetFormattedAddress()}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P001", 800.00, 1));
        order1.AddProduct(new Product("Mouse", "P002", 25.00, 2));

        Address address2 = new Address("456 Elm Ave", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Sophie Dupont", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Keyboard", "P003", 50.00, 1));
        order2.AddProduct(new Product("Monitor", "P004", 200.00, 1));

        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():F2}\n");

        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():F2}");
    }
}
