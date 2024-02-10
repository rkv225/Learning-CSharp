using System;

// in C# a generic delegate called System.EventHandler<> i.e.
// public delegate void EventHandler<TEventArgs> (object source, TEventArgs e)
public class PriceChangedEventArgs : System.EventArgs
{
    public readonly decimal LastPrice;
    public readonly decimal NewPrice;
    public PriceChangedEventArgs (decimal lastPrice, decimal newPrice)
    {
    LastPrice = lastPrice;
    NewPrice = newPrice;
    }
}
public class Stock  
{
    string symbol;
    decimal price;
    public Stock(string symbol) => this.symbol = symbol;
    public event EventHandler<PriceChangedEventArgs> PriceChanged;
    protected virtual void OnPriceChanged (PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke (this, e);
    }
    public decimal Price
    {
        get 
        {
            return price;
        }
        set
        {
            if(price == value) // exit if nothing has changed
                return;
            decimal oldPrice = price;
            price = value;
            OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
        }
    }
}
class Program
{
    static void stock_PriceChanged (object sender, PriceChangedEventArgs e)
    {
        if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
            Console.WriteLine ("Alert, 10% stock price increase!");
    }
    public static void Main(string[] args)
    {
        Stock stock = new Stock ("THPW");
        stock.Price = 27.10M;
        // Register with the PriceChanged event
        stock.PriceChanged += stock_PriceChanged;
        stock.Price = 31.59M;
    }
}