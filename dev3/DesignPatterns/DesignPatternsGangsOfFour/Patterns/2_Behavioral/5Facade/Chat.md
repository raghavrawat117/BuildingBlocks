Excellent. The **Facade Pattern** is one of the most common patterns you'll encounter in enterprise applications because it helps simplify interactions with complex systems.

> **Key idea:**
>
> Hide complexity behind a simple interface.

***

# Facade Pattern

## Definition

The Facade Pattern provides a unified, simplified interface to a set of interfaces in a subsystem.

Instead of interacting with multiple complex classes directly, the client communicates with a single facade.

***

# Real-World Analogy

Think of booking a vacation through a travel agent.

Without a travel agent:

```text
Contact Airline
Contact Hotel
Arrange Transport
Arrange Insurance
Make Payments
```

You handle everything yourself.

With a travel agent:

```text
Travel Agent
     ↓
Handles everything
```

The travel agent acts as a **Facade**.

***

# Problem Statement

Suppose an e-commerce application needs to process an order.

Steps:

1. Check inventory
2. Process payment
3. Arrange shipping

Without a facade:

```csharp
inventory.CheckStock();
payment.ProcessPayment();
shipping.ScheduleDelivery();
```

Every client must know the entire workflow.

***

# Solution

Create an `OrderFacade` that coordinates everything.

```csharp
orderFacade.PlaceOrder();
```

The client only needs one method call.

***

# Structure

```text
        Client
           |
           v
      OrderFacade
           |
    ------------------
    |       |        |
Inventory Payment Shipping
```

***

# Console Application Example

## Step 1: Subsystems

### InventoryService.cs

```csharp
using System;

namespace FacadePatternDemo
{
    public class InventoryService
    {
        public bool IsInStock(string product)
        {
            Console.WriteLine(
                $"Checking inventory for {product}");

            return true;
        }
    }
}
```

***

### PaymentService.cs

```csharp
using System;

namespace FacadePatternDemo
{
    public class PaymentService
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine(
                $"Payment of ₹{amount} processed");
        }
    }
}
```

***

### ShippingService.cs

```csharp
using System;

namespace FacadePatternDemo
{
    public class ShippingService
    {
        public void ScheduleDelivery(string product)
        {
            Console.WriteLine(
                $"Delivery scheduled for {product}");
        }
    }
}
```

***

## Step 2: Facade

### OrderFacade.cs

```csharp
using System;

namespace FacadePatternDemo
{
    public class OrderFacade
    {
        private readonly InventoryService _inventoryService;
        private readonly PaymentService _paymentService;
        private readonly ShippingService _shippingService;

        public OrderFacade()
        {
            _inventoryService = new InventoryService();
            _paymentService = new PaymentService();
            _shippingService = new ShippingService();
        }

        public void PlaceOrder(
            string product,
            decimal amount)
        {
            if (_inventoryService.IsInStock(product))
            {
                _paymentService.ProcessPayment(amount);
                _shippingService.ScheduleDelivery(product);

                Console.WriteLine(
                    "Order completed successfully");
            }
        }
    }
}
```

***

## Step 3: Client

### Program.cs

```csharp
using System;

namespace FacadePatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderFacade orderFacade = new OrderFacade();

            orderFacade.PlaceOrder(
                "Laptop",
                75000);

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Checking inventory for Laptop
Payment of ₹75000 processed
Delivery scheduled for Laptop
Order completed successfully
```

***

# Without Facade

The client has to coordinate everything:

```csharp
var inventory = new InventoryService();
var payment = new PaymentService();
var shipping = new ShippingService();

if (inventory.IsInStock("Laptop"))
{
    payment.ProcessPayment(75000);
    shipping.ScheduleDelivery("Laptop");
}
```

Problems:

❌ Client knows too much

❌ Duplicate workflow logic

❌ Harder to maintain

***

# With Facade

```csharp
var orderFacade = new OrderFacade();

orderFacade.PlaceOrder(
    "Laptop",
    75000);
```

Benefits:

✅ Simpler client code

✅ Centralized workflow

✅ Easier maintenance

***

# Real-World Example: Home Theater

### Subsystems

```csharp
Projector
SoundSystem
DVDPlayer
Lights
```

Without Facade:

```csharp
lights.Dim();
projector.On();
soundSystem.On();
dvdPlayer.Play();
```

***

### HomeTheaterFacade

```csharp
homeTheater.WatchMovie();
```

Implementation:

```csharp
public void WatchMovie()
{
    lights.Dim();
    projector.On();
    soundSystem.On();
    dvdPlayer.Play();
}
```

***

# UML (Simplified)

```text
            Client
               |
               v
         OrderFacade
               |
    -----------------------
    |         |           |
Inventory  Payment   Shipping
 Service   Service    Service
```

***

# Real-World .NET Examples

## Entity Framework DbContext

```csharp
dbContext.Users.Add(user);

dbContext.SaveChanges();
```

Internally EF handles:

* Change tracking
* SQL generation
* Transactions
* Database connections

`DbContext` acts as a facade over many subsystems.

***

## ASP.NET Core Identity

```csharp
await _userManager.CreateAsync(user);
```

Behind the scenes:

* Validation
* Password hashing
* Persistence
* Security checks

The `UserManager` acts like a facade.

***

## HttpClient

```csharp
await httpClient.GetAsync(url);
```

Internally:

* Connection management
* Sockets
* Protocol handling
* Buffering

The API abstracts significant complexity.

***

# Facade vs Adapter

A classic interview question.

## Adapter

Makes incompatible interfaces compatible.

```text
Client
   ↓
Adapter
   ↓
Legacy System
```

Purpose:

✅ Compatibility

***

## Facade

Provides a simpler interface.

```text
Client
   ↓
Facade
   ↓
Complex Subsystem
```

Purpose:

✅ Simplicity

***

# Facade vs Decorator

## Decorator

Adds behavior.

```text
Coffee
  ↓
MilkDecorator
  ↓
SugarDecorator
```

Purpose:

✅ Enhancement

***

## Facade

Simplifies usage.

```text
Client
  ↓
Facade
  ↓
Multiple Services
```

Purpose:

✅ Simplification

***

# Advantages

✅ Hides subsystem complexity

✅ Reduces coupling

✅ Improves readability

✅ Centralizes workflow logic

✅ Easier maintenance

✅ Better encapsulation

***

# Disadvantages

❌ Can become a "god class" if it grows too much

❌ May hide useful subsystem functionality

❌ Additional abstraction layer

***

# When to Use

Use Facade when:

* A subsystem is complex.
* Most clients follow the same workflow.
* You want a simple entry point.
* You want to reduce coupling between layers.

Examples:

* Order processing
* Payment processing
* Home automation
* Report generation
* Database access layers
* Third-party SDK wrappers

***

# Interview Question

### Q: Does Facade add new functionality?

**Answer:**

No.

A Facade does not add behavior or modify subsystem functionality.

It simply provides a simpler interface for using existing functionality.

For example:

```csharp
orderFacade.PlaceOrder();
```

Internally it still calls:

```csharp
InventoryService
PaymentService
ShippingService
```

The facade coordinates them but doesn't change what they do.

***

# Facade Pattern in One Line

> **Provide a simple, unified interface to a complex subsystem.**

✅ **Structural Pattern #5 Complete: Facade Pattern**

### Structural Patterns Progress

1. ✅ Adapter
2. ✅ Bridge
3. ✅ Composite
4. ✅ Decorator
5. ✅ Facade
6. ⏭️ Flyweight
7. ⏭️ Proxy

**Next Pattern: Flyweight Pattern**, which focuses on reducing memory usage by sharing common object state across many objects.
