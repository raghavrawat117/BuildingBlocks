Excellent. The **Adapter Pattern** is usually the first Structural Pattern because it's easy to understand and extremely common in real-world development.

# Adapter Pattern

## Definition

The Adapter Pattern allows two incompatible interfaces to work together.

It acts as a **bridge** or **translator** between an existing class and the client that expects a different interface.

***

# Real-World Analogy

Think of a mobile charger adapter.

```text
Wall Socket (India)
        ↓
     Adapter
        ↓
US Charger Plug
```

The charger wasn't designed for the socket directly, but the adapter makes them compatible.

***

# Problem Statement

Suppose your application expects all payment providers to implement:

```csharp
IPaymentProcessor
```

But you need to integrate a third-party payment service that has a completely different interface.

Without an Adapter, your application cannot directly use that service.

***

# Scenario

Your application expects:

```csharp
ProcessPayment(decimal amount);
```

But the third-party library provides:

```csharp
MakePayment(decimal amount);
```

Interfaces don't match.

***

# Solution Structure

```text
Client
   |
   v
IPaymentProcessor
   |
   v
PaymentAdapter
   |
   v
ThirdPartyPaymentGateway
```

The adapter converts one interface into another.

***

# Console Application Example

## Step 1: Target Interface

This is the interface your application expects.

### IPaymentProcessor.cs

```csharp
namespace AdapterPatternDemo
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
    }
}
```

***

## Step 2: Adaptee

This is the third-party class.

### ThirdPartyPaymentGateway.cs

```csharp
using System;

namespace AdapterPatternDemo
{
    public class ThirdPartyPaymentGateway
    {
        public void MakePayment(decimal amount)
        {
            Console.WriteLine(
                $"Payment of ₹{amount} processed via Third-Party Gateway");
        }
    }
}
```

Notice:

```csharp
MakePayment()
```

instead of

```csharp
ProcessPayment()
```

***

## Step 3: Adapter

### PaymentAdapter.cs

```csharp
namespace AdapterPatternDemo
{
    public class PaymentAdapter : IPaymentProcessor
    {
        private readonly ThirdPartyPaymentGateway _gateway;

        public PaymentAdapter(
            ThirdPartyPaymentGateway gateway)
        {
            _gateway = gateway;
        }

        public void ProcessPayment(decimal amount)
        {
            _gateway.MakePayment(amount);
        }
    }
}
```

The adapter translates:

```text
ProcessPayment()
        ↓
MakePayment()
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace AdapterPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ThirdPartyPaymentGateway gateway =
                new ThirdPartyPaymentGateway();

            IPaymentProcessor paymentProcessor =
                new PaymentAdapter(gateway);

            paymentProcessor.ProcessPayment(2500);

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Payment of ₹2500 processed via Third-Party Gateway
```

***

# Why Not Call the Third-Party Class Directly?

Without Adapter:

```csharp
var gateway = new ThirdPartyPaymentGateway();

gateway.MakePayment(2500);
```

Now your application is tightly coupled to the third-party API.

If the vendor changes:

```csharp
MakePayment()
```

to

```csharp
ExecutePayment()
```

you must update every client.

With an Adapter:

```csharp
paymentProcessor.ProcessPayment(2500);
```

Only the adapter changes.

***

# More Realistic Example

Suppose a payroll system expects:

```csharp
IEmployeeDataProvider
```

### Target Interface

```csharp
public interface IEmployeeDataProvider
{
    string GetEmployeeName();
}
```

***

### Legacy System

```csharp
public class LegacyEmployeeSystem
{
    public string FetchEmployee()
    {
        return "Raghav";
    }
}
```

***

### Adapter

```csharp
public class EmployeeAdapter : IEmployeeDataProvider
{
    private readonly LegacyEmployeeSystem _legacySystem;

    public EmployeeAdapter(
        LegacyEmployeeSystem legacySystem)
    {
        _legacySystem = legacySystem;
    }

    public string GetEmployeeName()
    {
        return _legacySystem.FetchEmployee();
    }
}
```

***

### Client

```csharp
IEmployeeDataProvider provider =
    new EmployeeAdapter(
        new LegacyEmployeeSystem());

Console.WriteLine(provider.GetEmployeeName());
```

Output:

```text
Raghav
```

***

# UML (Simplified)

```text
            Client
               |
               v
      IPaymentProcessor
               ^
               |
      PaymentAdapter
               |
               v
 ThirdPartyPaymentGateway
```

***

# Class Adapter vs Object Adapter

## 1. Object Adapter (Most Common)

Uses composition.

```csharp
public class PaymentAdapter
{
    private readonly ThirdPartyPaymentGateway _gateway;
}
```

Advantages:

✅ Flexible

✅ Supports dependency injection

✅ Preferred in C#

***

## 2. Class Adapter

Uses inheritance.

```csharp
public class PaymentAdapter :
    ThirdPartyPaymentGateway,
    IPaymentProcessor
{
}
```

Less common in C# because multiple class inheritance is not supported.

***

# Real-World .NET Examples

## StreamReader

```csharp
StreamReader reader =
    new StreamReader(fileStream);
```

`StreamReader` adapts a raw `Stream` into a text-reading interface.

***

## ILogger Adapters

In many applications:

```csharp
ILogger logger
```

may internally wrap:

* Serilog
* NLog
* Application Insights

through adapters.

***

## Legacy System Integration

Very common when:

* Migrating old applications
* Consuming SOAP services
* Integrating external vendor SDKs
* Wrapping legacy databases

***

# Advantages

✅ Reuses existing code

✅ Works with third-party libraries

✅ Reduces client changes

✅ Supports Single Responsibility Principle

✅ Improves maintainability

***

# Disadvantages

❌ Adds extra classes

❌ Can increase complexity

❌ Too many adapters may make the architecture harder to follow

***

# When to Use

Use Adapter when:

* Interfaces are incompatible.
* You cannot change an existing class.
* Integrating third-party libraries.
* Integrating legacy systems.
* Standardizing multiple external APIs.

Examples:

* Payment gateways
* Logging frameworks
* Legacy applications
* File format converters
* External APIs

***

# Adapter vs Facade

Many juniors confuse these.

### Adapter

Makes incompatible interfaces compatible.

```text
Expected Interface
        ↓
     Adapter
        ↓
Existing Interface
```

Focus:

✅ Compatibility

***

### Facade

Provides a simplified interface to a complex subsystem.

```text
Client
   ↓
 Facade
   ↓
Many Subsystems
```

Focus:

✅ Simplicity

***

# Interview Question

### Q: What's the difference between Adapter and Wrapper?

**Answer:**

An Adapter is a type of wrapper specifically used to convert one interface into another.

```csharp
ProcessPayment()
        ↓
Adapter
        ↓
MakePayment()
```

The primary goal is compatibility between existing components.

***

## Adapter Pattern in One Line

> **Convert the interface of an existing class into an interface that the client expects.**

✅ **Structural Pattern #1 Complete: Adapter Pattern**

### Structural Patterns Progress

1. ✅ Adapter
2. ⏭️ Bridge
3. ⏭️ Composite
4. ⏭️ Decorator
5. ⏭️ Facade
6. ⏭️ Flyweight
7. ⏭️ Proxy

**Next Pattern:** **Bridge Pattern**, which is often confused with Adapter, but solves a completely different problem: preventing a class hierarchy from exploding due to multiple dimensions of variation.
