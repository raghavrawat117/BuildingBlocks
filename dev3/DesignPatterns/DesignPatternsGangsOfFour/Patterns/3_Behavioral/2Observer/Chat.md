Excellent. The **Observer Pattern** is one of the most important Behavioral Patterns for C# developers because it's the foundation behind:

* C# Events and Event Handlers
* Message Brokers
* Reactive Programming
* UI Notifications
* Publish/Subscribe Systems

> **Key idea:**
>
> When one object changes state, all interested objects are automatically notified.

***

# Observer Pattern

## Definition

The Observer Pattern defines a one-to-many dependency between objects so that when one object changes state, all its dependents are notified automatically.

***

# Real-World Analogy

Think about a YouTube channel.

```text
YouTube Channel (Subject)
       |
       ├── Subscriber A
       ├── Subscriber B
       └── Subscriber C
```

When a new video is uploaded:

```text
Subscriber A notified
Subscriber B notified
Subscriber C notified
```

The channel doesn't need to know what each subscriber does after receiving the notification.

***

# Problem Statement

Suppose a stock price changes.

Without Observer:

```csharp
emailService.Send();

smsService.Send();

mobileApp.Update();

dashboard.Refresh();
```

Every time the stock changes, the stock class must explicitly call all consumers.

Problems:

❌ Tight coupling

❌ Hard to add new subscribers

❌ Violates Open/Closed Principle

***

# Solution

Create:

### Subject

```csharp
Stock
```

### Observers

```csharp
MobileApp
EmailAlert
Dashboard
```

The Stock notifies all registered observers automatically.

***

# Structure

```text
               IObserver
                   ▲
          ┌────────┼────────┐
          │        │        │
      Email    Mobile   Dashboard

                   ▲
                   |
               Stock
               (Subject)
```

***

# Console Application Example

## Step 1: Observer Interface

### IObserver.cs

```csharp
namespace ObserverPatternDemo
{
    public interface IObserver
    {
        void Update(decimal stockPrice);
    }
}
```

***

## Step 2: Subject Interface

### ISubject.cs

```csharp
namespace ObserverPatternDemo
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
```

***

## Step 3: Concrete Subject

### Stock.cs

```csharp
using System.Collections.Generic;

namespace ObserverPatternDemo
{
    public class Stock : ISubject
    {
        private readonly List<IObserver> _observers =
            new();

        private decimal _price;

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                Notify();
            }
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_price);
            }
        }
    }
}
```

***

## Step 4: Concrete Observers

### MobileApp.cs

```csharp
using System;

namespace ObserverPatternDemo
{
    public class MobileApp : IObserver
    {
        public void Update(decimal stockPrice)
        {
            Console.WriteLine(
                $"Mobile App Updated : ₹{stockPrice}");
        }
    }
}
```

***

### EmailAlert.cs

```csharp
using System;

namespace ObserverPatternDemo
{
    public class EmailAlert : IObserver
    {
        public void Update(decimal stockPrice)
        {
            Console.WriteLine(
                $"Email Alert Sent : ₹{stockPrice}");
        }
    }
}
```

***

### Dashboard.cs

```csharp
using System;

namespace ObserverPatternDemo
{
    public class Dashboard : IObserver
    {
        public void Update(decimal stockPrice)
        {
            Console.WriteLine(
                $"Dashboard Refreshed : ₹{stockPrice}");
        }
    }
}
```

***

## Step 5: Client

### Program.cs

```csharp
using System;

namespace ObserverPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();

            stock.Attach(new MobileApp());
            stock.Attach(new EmailAlert());
            stock.Attach(new Dashboard());

            stock.Price = 100;

            Console.WriteLine();

            stock.Price = 125;

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Mobile App Updated : ₹100
Email Alert Sent : ₹100
Dashboard Refreshed : ₹100

Mobile App Updated : ₹125
Email Alert Sent : ₹125
Dashboard Refreshed : ₹125
```

***

# Execution Flow

When this executes:

```csharp
stock.Price = 125;
```

Flow:

```text
Stock Price Changed
          |
          v
        Notify()
          |
   -------------------
   |       |         |
 Mobile  Email   Dashboard
```

Each observer receives the update.

***

# Dynamic Subscription

Observers can subscribe and unsubscribe at runtime.

```csharp
var email = new EmailAlert();

stock.Attach(email);

stock.Price = 100;
```

Output:

```text
Email Alert Sent : ₹100
```

Remove:

```csharp
stock.Detach(email);

stock.Price = 200;
```

No email notification is sent.

***

# Real-World Example: Order Status Notifications

## Subject

```csharp
Order
```

State:

```text
Created
Packed
Shipped
Delivered
```

Observers:

```text
Customer Notification
Warehouse System
Analytics System
Audit System
```

When order status changes:

```csharp
order.Status = "Shipped";
```

Every observer gets notified automatically.

***

# Observer Pattern Using C# Events

This is how you'll usually see Observer implemented in .NET.

## Publisher

```csharp
using System;

public class Stock
{
    public event Action<decimal> PriceChanged;

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;

            PriceChanged?.Invoke(_price);
        }
    }
}
```

***

## Subscribers

```csharp
public class MobileApp
{
    public void OnPriceChanged(decimal price)
    {
        Console.WriteLine(
            $"Mobile App Updated : {price}");
    }
}
```

***

## Client

```csharp
var stock = new Stock();

var app = new MobileApp();

stock.PriceChanged += app.OnPriceChanged;

stock.Price = 100;
```

This is essentially the Observer Pattern built into C#.

***

# Real-World .NET Examples

## Events

```csharp
button.Click += Button_Click;
```

Observer relationship:

```text
Button (Subject)
      |
      v
Button_Click (Observer)
```

***

## FileSystemWatcher

```csharp
watcher.Created += OnFileCreated;
```

When a file changes:

```text
Watcher
   ↓
Subscribers notified
```

***

## Message Queues

```text
Publisher
      ↓
Queue / Topic
      ↓
Consumers
```

Observer pattern at a larger scale.

***

# UML (Simplified)

```text
            IObserver
                ▲
      -----------------------
      |          |          |
   Mobile     Email    Dashboard

                ▲
                |
             Stock
```

***

# Observer vs Chain of Responsibility

A common interview question.

### Chain of Responsibility

One handler processes the request.

```text
Handler1
   ↓
Handler2
   ↓
Handler3
```

Request travels through the chain.

***

### Observer

All interested observers are notified.

```text
Subject
  ├── Observer1
  ├── Observer2
  └── Observer3
```

Everyone receives the update.

***

# Observer vs Mediator

### Observer

Communication is:

```text
One → Many
```

Example:

```text
Stock → Subscribers
```

***

### Mediator

Communication is:

```text
Many → One → Many
```

Example:

```text
Chat Users → Chat Room → Chat Users
```

***

# Advantages

✅ Loose coupling

✅ Easy to add new subscribers

✅ Supports runtime subscription

✅ Follows Open/Closed Principle

✅ Great for event-driven systems

***

# Disadvantages

❌ Too many observers can impact performance

❌ Notification order may matter

❌ Harder to debug event chains

❌ Risk of memory leaks if observers aren't unsubscribed

***

# When to Use

Use Observer when:

* Multiple objects need updates.
* Events occur frequently.
* Publisher and subscribers should be loosely coupled.
* Publish/Subscribe behavior is required.

Examples:

* Stock market apps
* Notifications
* UI events
* Order tracking
* Message queues
* Real-time dashboards

***

# Interview Question

### Q: How is the Observer Pattern implemented in .NET?

**Answer:**

Most commonly using **events and delegates**.

Example:

```csharp
public event Action<string> MessageReceived;
```

Subscribers register:

```csharp
service.MessageReceived += HandleMessage;
```

Publisher notifies:

```csharp
MessageReceived?.Invoke(message);
```

This is the .NET implementation of the Observer Pattern.

***

# Observer Pattern in One Line

> **Define a one-to-many relationship so that when one object changes state, all dependent objects are automatically notified.**

✅ **Behavioral Pattern #2 Complete: Observer Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ⏭️ Command
4. ⏭️ Interpreter
5. ⏭️ Iterator
6. ⏭️ Mediator
7. ⏭️ Memento
8. ⏭️ State
9. ⏭️ Strategy
10. ⏭️ Template Method
11. ⏭️ Visitor

**Next Pattern: Strategy Pattern**, which is one of the most frequently asked design patterns in .NET interviews and is heavily used with dependency injection.
