using System;
using System.Threading;

public class BookingSlotStatusEventArgs : EventArgs
{
    public bool ReservationComplete { get; set; }
}

public class BookingSlot
{
    public event EventHandler BookingExpired;

    private bool reservationComplete = false;
    private bool reserved = false;
    private int timeoutDuration = 1; // Timeout duration in minutes

    public void Reserve()
    {
        reserved = true;
        StartTimer(timeoutDuration, ReleaseReservation);
    }
    private void StartTimer(int duration, Action callback)
    {
        Timer timer = null;
        timer = new Timer(_ =>
        {
            callback();
            timer?.Dispose(); // Dispose the timer once it's no longer needed
        }, null, TimeSpan.FromMinutes(duration), TimeSpan.Zero);
    }
    private void ReleaseReservation()
    {
        reserved = false;
        OnBookingExpired();
    }
    protected virtual void OnBookingExpired()
    {
        BookingExpired?.Invoke(this, new BookingSlotStatusEventArgs(){ ReservationComplete = reservationComplete });
    }
    public void ConfirmBooking()
    {
        // check if the slot is still reserved
        // we can have an additional check of userid
        if (reserved)
        {
            // Complete booking process
            reservationComplete = true;
            Console.WriteLine("Booking confirmed.");
            ReleaseReservation();
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BookingSlot slot = new BookingSlot();
        slot.BookingExpired += HandleBookingExpired;

        slot.Reserve();

        // Simulating user completing payment within timeout duration
        Console.WriteLine("Press Enter to complete booking");
        Console.ReadLine();
        slot.ConfirmBooking();
    }

    private static void HandleBookingExpired(object sender, EventArgs e)
    {
        BookingSlotStatusEventArgs arg = (BookingSlotStatusEventArgs)e;
        if(!arg.ReservationComplete)
            Console.WriteLine("Booking slot expired.");
    }
}
