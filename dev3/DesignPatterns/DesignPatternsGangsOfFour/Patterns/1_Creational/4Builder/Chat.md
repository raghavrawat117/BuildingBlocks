Excellent. The **Builder Pattern** is commonly taught after the Factory patterns because it tackles a different problem:

> Factory patterns focus on **which object to create**.
>
> Builder focuses on **how to create a complex object step-by-step**.

***

# Builder Pattern

## Definition

The Builder Pattern separates the construction of a complex object from its representation, allowing the same construction process to create different representations.

***

# Problem Statement

Imagine creating a `Computer` object.

Without Builder:

```csharp
var computer = new Computer(
    "Intel i9",
    "32GB",
    "1TB SSD",
    true,
    true,
    true,
    "RTX 4080");
```

Problems:

* Too many constructor parameters
* Hard to read
* Difficult to maintain
* Parameter order mistakes are common

This is called the **Telescoping Constructor Problem**.

***

# Solution

Build the object step by step:

```csharp
var computer = new ComputerBuilder()
                .SetCpu("Intel i9")
                .SetRam("32GB")
                .SetStorage("1TB SSD")
                .SetGraphicsCard("RTX 4080")
                .EnableBluetooth()
                .EnableWifi()
                .Build();
```

Much cleaner and easier to understand.

***

# Console Application Example

We'll build a custom gaming computer.

***

## Step 1: Product

### Computer.cs

```csharp
using System;

namespace BuilderPatternDemo
{
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string GraphicsCard { get; set; }

        public bool WifiEnabled { get; set; }
        public bool BluetoothEnabled { get; set; }

        public void ShowConfiguration()
        {
            Console.WriteLine("Computer Configuration");
            Console.WriteLine($"CPU: {CPU}");
            Console.WriteLine($"RAM: {RAM}");
            Console.WriteLine($"Storage: {Storage}");
            Console.WriteLine($"Graphics Card: {GraphicsCard}");
            Console.WriteLine($"WiFi: {WifiEnabled}");
            Console.WriteLine($"Bluetooth: {BluetoothEnabled}");
        }
    }
}
```

***

## Step 2: Builder

### ComputerBuilder.cs

```csharp
namespace BuilderPatternDemo
{
    public class ComputerBuilder
    {
        private readonly Computer _computer;

        public ComputerBuilder()
        {
            _computer = new Computer();
        }

        public ComputerBuilder SetCpu(string cpu)
        {
            _computer.CPU = cpu;
            return this;
        }

        public ComputerBuilder SetRam(string ram)
        {
            _computer.RAM = ram;
            return this;
        }

        public ComputerBuilder SetStorage(string storage)
        {
            _computer.Storage = storage;
            return this;
        }

        public ComputerBuilder SetGraphicsCard(string card)
        {
            _computer.GraphicsCard = card;
            return this;
        }

        public ComputerBuilder EnableWifi()
        {
            _computer.WifiEnabled = true;
            return this;
        }

        public ComputerBuilder EnableBluetooth()
        {
            _computer.BluetoothEnabled = true;
            return this;
        }

        public Computer Build()
        {
            return _computer;
        }
    }
}
```

***

## Step 3: Client

### Program.cs

```csharp
using System;

namespace BuilderPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Computer gamingPc = new ComputerBuilder()
                                .SetCpu("Intel i9")
                                .SetRam("32GB")
                                .SetStorage("1TB SSD")
                                .SetGraphicsCard("RTX 4080")
                                .EnableWifi()
                                .EnableBluetooth()
                                .Build();

            gamingPc.ShowConfiguration();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Computer Configuration

CPU: Intel i9
RAM: 32GB
Storage: 1TB SSD
Graphics Card: RTX 4080
WiFi: True
Bluetooth: True
```

***

# Classic GoF Builder Version

The textbook Builder pattern introduces a **Director**.

## Builder Interface

```csharp
public interface IComputerBuilder
{
    void BuildCpu();
    void BuildRam();
    void BuildStorage();
    Computer GetComputer();
}
```

***

## Concrete Builder

```csharp
public class GamingComputerBuilder : IComputerBuilder
{
    private readonly Computer _computer = new();

    public void BuildCpu()
    {
        _computer.CPU = "Intel i9";
    }

    public void BuildRam()
    {
        _computer.RAM = "32GB";
    }

    public void BuildStorage()
    {
        _computer.Storage = "1TB SSD";
    }

    public Computer GetComputer()
    {
        return _computer;
    }
}
```

***

## Director

```csharp
public class ComputerDirector
{
    public void Construct(IComputerBuilder builder)
    {
        builder.BuildCpu();
        builder.BuildRam();
        builder.BuildStorage();
    }
}
```

***

## Client

```csharp
var builder = new GamingComputerBuilder();

var director = new ComputerDirector();

director.Construct(builder);

Computer computer = builder.GetComputer();
```

***

# Real-World Example in .NET

A familiar .NET example is:

```csharp
var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices(...)
               .ConfigureLogging(...)
               .Build();
```

Another example:

```csharp
var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables()
                        .Build();
```

These use the Builder pattern extensively.

***

# Builder vs Factory

### Factory

Creates an object in one step.

```csharp
var car = CarFactory.CreateSportsCar();
```

### Builder

Creates an object incrementally.

```csharp
var car = new CarBuilder()
            .SetEngine("V8")
            .SetColor("Red")
            .AddSunroof()
            .Build();
```

***

# When to Use

Use Builder when:

* An object has many optional properties.
* Construction requires multiple steps.
* Constructors become too large.
* You want a fluent API.

Examples:

* Computer configuration
* House construction
* API request builders
* Database query builders
* ASP.NET Core HostBuilder

***

# Advantages

✅ Readable object creation

✅ Avoids long constructors

✅ Supports fluent APIs

✅ Step-by-step construction

✅ Easy to add optional fields

***

# Disadvantages

❌ More classes

❌ May be unnecessary for simple objects

❌ Can increase code size

***

# Interview Question

### Q: When would you choose Builder over Factory?

**Answer:**

Use a **Factory** when object creation is simple and the primary concern is selecting the correct type.

```csharp
IPayment payment = PaymentFactory.Create("CreditCard");
```

Use a **Builder** when the object is complex and requires many configuration steps.

```csharp
var report = new ReportBuilder()
                .SetTitle("Sales")
                .SetHeader()
                .SetFooter()
                .AddCharts()
                .Build();
```

***

## Builder Pattern in One Line

> **Factory chooses the object type; Builder constructs the object step-by-step.**

✅ **Design Pattern #4 Complete: Builder Pattern**

Next in the Creational Patterns sequence would be **Prototype Pattern**, which focuses on creating new objects by cloning existing ones rather than building them from scratch.
