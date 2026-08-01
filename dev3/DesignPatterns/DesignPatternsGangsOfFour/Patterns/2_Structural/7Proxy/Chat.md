Excellent. The **Proxy Pattern** is the final GoF Structural Pattern. It's widely used in enterprise applications, ORMs, APIs, caching layers, security, and remote service access.

> **Key idea:**
>
> A Proxy acts as a substitute or placeholder for another object and controls access to it.

***

# Proxy Pattern

## Definition

The Proxy Pattern provides a surrogate (placeholder) object that controls access to another object.

The proxy has the same interface as the real object, allowing the client to interact with it transparently.

***

# Real-World Analogy

Think about a company CEO.

Employees usually don't communicate directly with the CEO.

Instead:

```text
Employee
    ↓
Executive Assistant
    ↓
CEO
```

The assistant:

* Filters requests
* Validates access
* Schedules meetings

The assistant acts as a **Proxy**.

***

# Problem Statement

Suppose accessing a report is expensive:

* Database calls
* Network requests
* File reading

Without a Proxy:

```csharp
report.Generate();
```

Every request performs the expensive operation.

You may want to:

* Cache results
* Check permissions
* Log access
* Delay object creation

without modifying the report class.

***

# Structure

```text
          IReport
             ▲
      ┌──────┴──────┐
      │             │
 RealReport   ReportProxy
```

The client works with the interface and doesn't know whether it's talking to the real object or the proxy.

***

# Console Application Example

## Step 1: Subject Interface

### IReport.cs

```csharp
namespace ProxyPatternDemo
{
    public interface IReport
    {
        void Display();
    }
}
```

***

## Step 2: Real Subject

### RealReport.cs

```csharp
using System;
using System.Threading;

namespace ProxyPatternDemo
{
    public class RealReport : IReport
    {
        public RealReport()
        {
            Console.WriteLine(
                "Loading report from database...");

            Thread.Sleep(2000);
        }

        public void Display()
        {
            Console.WriteLine(
                "Report displayed.");
        }
    }
}
```

Imagine loading the report is expensive.

***

## Step 3: Proxy

### ReportProxy.cs

```csharp
namespace ProxyPatternDemo
{
    public class ReportProxy : IReport
    {
        private RealReport _realReport;

        public void Display()
        {
            if (_realReport == null)
            {
                _realReport = new RealReport();
            }

            _realReport.Display();
        }
    }
}
```

Notice:

```csharp
if (_realReport == null)
{
    _realReport = new RealReport();
}
```

The real object is created only when needed.

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace ProxyPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IReport report =
                new ReportProxy();

            Console.WriteLine(
                "Proxy created.");

            Console.WriteLine(
                "Press any key to view report.");

            Console.ReadKey();

            report.Display();

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Proxy created.

Press any key to view report.

Loading report from database...

Report displayed.
```

Notice:

```csharp
new RealReport()
```

is delayed until:

```csharp
report.Display();
```

This is called **Lazy Loading**.

***

# Types of Proxy

There are several variations of the Proxy pattern.

***

# 1. Virtual Proxy

Controls expensive object creation.

Example:

```csharp
LargeImage
```

Load only when actually viewed.

```csharp
ImageProxy
```

creates the image when needed.

***

# 2. Protection Proxy

Controls access permissions.

Example:

```csharp
Admin Report
```

Only admins should access it.

### Real Subject

```csharp
public class PayrollReport
{
    public void View()
    {
        Console.WriteLine("Payroll Report");
    }
}
```

### Proxy

```csharp
public class PayrollProxy
{
    private readonly string _role;

    public PayrollProxy(string role)
    {
        _role = role;
    }

    public void View()
    {
        if (_role != "Admin")
        {
            Console.WriteLine("Access Denied");
            return;
        }

        Console.WriteLine("Payroll Report");
    }
}
```

***

# 3. Remote Proxy

Represents an object in another process or machine.

Example:

```text
Client
   ↓
Proxy
   ↓
Remote API
```

The proxy hides networking details.

Examples:

* WCF
* gRPC clients
* Web service clients

***

# 4. Caching Proxy

Stores previously retrieved results.

### Real Service

```csharp
public interface IProductService
{
    string GetProduct(int id);
}
```

### Proxy

```csharp
public class ProductCacheProxy
{
    private readonly Dictionary<int, string>
        _cache = new();

    private readonly ProductService _service =
        new();

    public string GetProduct(int id)
    {
        if (!_cache.ContainsKey(id))
        {
            _cache[id] =
                _service.GetProduct(id);
        }

        return _cache[id];
    }
}
```

Result:

```text
First Call  -> Database
Second Call -> Cache
```

***

# UML (Simplified)

```text
             IReport
                ▲
        ┌───────┴────────┐
        │                │
    RealReport     ReportProxy
```

The client interacts with:

```csharp
IReport
```

and remains unaware of the proxy.

***

# Real-World .NET Examples

## Entity Framework Lazy Loading

```csharp
var order = context.Orders.First();

var customer = order.Customer;
```

The `Customer` object may not be loaded initially.

EF creates a proxy object that fetches the data only when accessed.

This is one of the most common Proxy examples in modern .NET.

***

## API Clients

```csharp
var client = new ProductServiceProxy();
```

The client appears local.

Internally it:

* Makes HTTP calls
* Serializes requests
* Handles authentication

The proxy hides the complexity.

***

## Authentication Middleware

Many security implementations use proxies to verify permissions before forwarding requests to protected resources.

***

# Proxy vs Decorator

This is the most common interview question.

## Decorator

Adds behavior.

```text
Coffee
   ↓
MilkDecorator
```

Purpose:

✅ Enhancement

***

Example:

```csharp
loggingService =
    new LoggingDecorator(service);
```

Adds logging.

***

## Proxy

Controls access.

```text
Client
   ↓
Proxy
   ↓
Real Object
```

Purpose:

✅ Access control

✅ Caching

✅ Lazy loading

✅ Security

***

# Proxy vs Adapter

## Adapter

Changes an interface.

```text
Old API
   ↓
Adapter
   ↓
New API
```

Purpose:

✅ Compatibility

***

## Proxy

Keeps the same interface.

```text
IReport
   ↑
Proxy
   ↑
Real Report
```

Purpose:

✅ Controlled access

***

# Proxy vs Facade

## Facade

Simplifies a subsystem.

```text
Client
   ↓
Facade
   ↓
Many Classes
```

Purpose:

✅ Simplicity

***

## Proxy

Represents one object.

```text
Client
   ↓
Proxy
   ↓
One Real Object
```

Purpose:

✅ Access control

***

# Advantages

✅ Supports lazy loading

✅ Improves security

✅ Enables caching

✅ Provides access control

✅ Hides remote communication

✅ Follows Open/Closed Principle

***

# Disadvantages

❌ Additional layer of abstraction

❌ More classes

❌ Can introduce slight performance overhead

❌ Debugging may become harder

***

# When to Use

Use Proxy when:

* Object creation is expensive.
* Access must be controlled.
* Security checks are required.
* Remote communication should be hidden.
* Results should be cached.

Examples:

* Entity Framework Lazy Loading
* API Clients
* Database Access
* Security Wrappers
* Caching Layers
* Virtual Images/Documents

***

# Interview Question

### Q: What is the difference between Proxy and Decorator if both have the same interface?

**Answer:**

Both wrap another object and usually implement the same interface.

**Decorator** focuses on adding functionality:

```csharp
LoggingDecorator
CachingDecorator
RetryDecorator
```

**Proxy** focuses on controlling access:

```csharp
SecurityProxy
RemoteProxy
VirtualProxy
```

A handy rule:

> **Decorator adds behavior. Proxy controls access.**

***

# Proxy Pattern in One Line

> **Provide a placeholder or representative object that controls access to another object.**

✅ **Structural Patterns Completed**

1. ✅ Adapter
2. ✅ Bridge
3. ✅ Composite
4. ✅ Decorator
5. ✅ Facade
6. ✅ Flyweight
7. ✅ Proxy

### GoF Progress

**Creational Patterns**

* ✅ Singleton
* ✅ Factory Method
* ✅ Abstract Factory
* ✅ Builder
* ✅ Prototype

**Structural Patterns**

* ✅ Adapter
* ✅ Bridge
* ✅ Composite
* ✅ Decorator
* ✅ Facade
* ✅ Flyweight
* ✅ Proxy

The next category is **Behavioral Patterns**, typically starting with **Chain of Responsibility**, followed by Observer, Strategy, Command, State, Template Method, and others.
