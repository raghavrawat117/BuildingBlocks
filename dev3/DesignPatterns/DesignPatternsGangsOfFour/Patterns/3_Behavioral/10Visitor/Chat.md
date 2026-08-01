Excellent. The **Visitor Pattern** is often considered one of the more advanced GoF patterns because it's not used as frequently as Strategy or Observer. However, it's extremely powerful when:

> You have a stable object structure, but you frequently need to add new operations to it.

***

# Visitor Pattern

## Definition

The Visitor Pattern lets you define new operations on a collection of objects without changing the classes of those objects.

Instead of adding methods to existing classes every time a new operation is needed, you create a new Visitor.

***

# Real-World Analogy

Imagine a hospital.

You have:

```text
Patient
Doctor
Nurse
```

Now different people need to interact with them:

```text
Billing Department
Auditor
Insurance Inspector
```

Without Visitor:

```csharp
Patient.GenerateBill()
Patient.Audit()
Patient.GenerateInsuranceReport()

Doctor.GenerateBill()
Doctor.Audit()
Doctor.GenerateInsuranceReport()
```

Classes keep growing.

With Visitor:

```text
Patient
Doctor
Nurse
```

remain unchanged while:

```text
BillingVisitor
AuditVisitor
InsuranceVisitor
```

contain the operations.

***

# Problem Statement

Suppose we have an employee management system.

Objects:

```text
Developer
Manager
```

Now management wants:

```text
Salary Report
Performance Report
Tax Report
```

Without Visitor, we'd modify every employee class each time.

***

# Solution

Separate:

## Object Structure

```text
Developer
Manager
```

from

## Operations

```text
SalaryVisitor
PerformanceVisitor
TaxVisitor
```

***

# Structure

```text
               IEmployee
                    ▲
           ┌────────┴────────┐
           │                 │
       Developer         Manager

                    ▲
                    |
               IVisitor
                    ▲
           ┌────────┴────────┐
           │                 │
      SalaryVisitor   TaxVisitor
```

***

# Console Application Example

## Step 1: Visitor Interface

### IVisitor.cs

```csharp
namespace VisitorPatternDemo
{
    public interface IVisitor
    {
        void Visit(Developer developer);
        void Visit(Manager manager);
    }
}
```

***

## Step 2: Element Interface

### IEmployee.cs

```csharp
namespace VisitorPatternDemo
{
    public interface IEmployee
    {
        void Accept(IVisitor visitor);
    }
}
```

***

## Step 3: Concrete Elements

### Developer.cs

```csharp
namespace VisitorPatternDemo
{
    public class Developer : IEmployee
    {
        public string Name { get; }

        public Developer(string name)
        {
            Name = name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
```

***

### Manager.cs

```csharp
namespace VisitorPatternDemo
{
    public class Manager : IEmployee
    {
        public string Name { get; }

        public Manager(string name)
        {
            Name = name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
```

***

## Step 4: Concrete Visitor

### SalaryReportVisitor.cs

```csharp
using System;

namespace VisitorPatternDemo
{
    public class SalaryReportVisitor : IVisitor
    {
        public void Visit(Developer developer)
        {
            Console.WriteLine(
                $"Developer {developer.Name} salary report generated.");
        }

        public void Visit(Manager manager)
        {
            Console.WriteLine(
                $"Manager {manager.Name} salary report generated.");
        }
    }
}
```

***

### TaxReportVisitor.cs

```csharp
using System;

namespace VisitorPatternDemo
{
    public class TaxReportVisitor : IVisitor
    {
        public void Visit(Developer developer)
        {
            Console.WriteLine(
                $"Developer {developer.Name} tax report generated.");
        }

        public void Visit(Manager manager)
        {
            Console.WriteLine(
                $"Manager {manager.Name} tax report generated.");
        }
    }
}
```

***

## Step 5: Client

### Program.cs

```csharp
using System;
using System.Collections.Generic;

namespace VisitorPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IEmployee> employees =
                new()
                {
                    new Developer("Raghav"),
                    new Manager("Amit")
                };

            IVisitor salaryVisitor =
                new SalaryReportVisitor();

            foreach (var employee in employees)
            {
                employee.Accept(salaryVisitor);
            }

            Console.WriteLine();

            IVisitor taxVisitor =
                new TaxReportVisitor();

            foreach (var employee in employees)
            {
                employee.Accept(taxVisitor);
            }

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Developer Raghav salary report generated.
Manager Amit salary report generated.

Developer Raghav tax report generated.
Manager Amit tax report generated.
```

***

# Understanding `Accept()`

This is the heart of the Visitor pattern.

```csharp
employee.Accept(visitor);
```

For a Developer:

```csharp
visitor.Visit(this);
```

calls:

```csharp
Visit(Developer developer)
```

For a Manager:

```csharp
visitor.Visit(this);
```

calls:

```csharp
Visit(Manager manager)
```

This is called **Double Dispatch**.

***

# What is Double Dispatch?

Normally C# decides a method using:

```csharp
object.Method();
```

based on one runtime type.

Visitor uses:

```csharp
employee.Accept(visitor);
```

and then:

```csharp
visitor.Visit(employeeType);
```

So the operation depends on:

1. Visitor type
2. Element type

Example:

```text
SalaryVisitor + Developer
TaxVisitor + Developer
SalaryVisitor + Manager
TaxVisitor + Manager
```

Different methods execute.

***

# Real-World Example: File System

Elements:

```text
File
Folder
Image
```

Visitors:

```text
SizeCalculationVisitor
CompressionVisitor
VirusScanVisitor
```

Instead of adding methods repeatedly:

```csharp
file.GetSize();
file.ScanVirus();
file.Compress();
```

Create visitors:

```csharp
new VirusScanVisitor();
new CompressionVisitor();
```

***

# More Realistic Example

Suppose an e-commerce application has:

```text
PhysicalProduct
DigitalProduct
SubscriptionProduct
```

New requirements:

```text
Generate Invoice
Calculate GST
Generate Shipping Label
```

Without Visitor:

Every product class keeps changing.

With Visitor:

```text
InvoiceVisitor
TaxVisitor
ShippingVisitor
```

Products remain unchanged.

***

# UML (Simplified)

```text
                IVisitor
                    ▲
          --------------------
          |                  |
     SalaryVisitor     TaxVisitor


                IEmployee
                    ▲
          ------------------
          |                |
      Developer       Manager
```

***

# Visitor vs Strategy

A very common interview question.

## Strategy

Switches algorithms.

```text
UPI Payment
Credit Card Payment
PayPal Payment
```

Client selects one strategy.

***

## Visitor

Adds operations to existing objects.

```text
Salary Report
Tax Report
Performance Report
```

Objects stay unchanged.

***

# Visitor vs Command

## Command

Represents an action.

```csharp
CreateOrderCommand
DeleteOrderCommand
```

Focus:

✅ Request encapsulation

***

## Visitor

Represents an operation on an object structure.

```csharp
TaxVisitor
SalaryVisitor
```

Focus:

✅ Adding behavior

***

# Visitor vs Iterator

## Iterator

Traverses a structure.

```text
Next()
MoveNext()
Current
```

***

## Visitor

Performs operations on a structure.

```text
Accept(visitor)
```

***

# Advantages

✅ Add new operations easily

✅ Follows Open/Closed Principle for operations

✅ Keeps business logic separate

✅ Useful for reporting, auditing, exporting

✅ Reduces clutter in element classes

***

# Disadvantages

❌ Adding a new element is expensive

Example:

```csharp
Intern
```

requires changing:

```csharp
IVisitor
SalaryVisitor
TaxVisitor
AuditVisitor
Every Visitor
```

❌ Can be difficult to understand initially

❌ Heavy use of double dispatch

***

# When to Use

Use Visitor when:

* Object structure changes rarely.
* New operations are added frequently.
* Business logic should be separated from data structures.

Examples:

* Reporting systems
* Compilers/AST processing
* File systems
* Auditing
* Tax calculations
* Export engines

***

# Interview Question

### Q: When should you use Visitor Pattern?

**Answer:**

Use Visitor when you have a stable set of object types but expect to add many new operations over time.

Instead of modifying:

```csharp
Developer
Manager
```

every time a new feature is required, create:

```csharp
SalaryVisitor
TaxVisitor
AuditVisitor
```

This keeps the object model stable while allowing new behavior to be added.

***

# Visitor Pattern in One Line

> **Represent an operation as a separate object so new operations can be added to an object structure without modifying the objects themselves.**

✅ **Behavioral Pattern #10 Complete: Visitor Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ✅ Command
5. ✅ State
6. ✅ Template Method
7. ✅ Mediator
8. ✅ Iterator
9. ✅ Memento
10. ✅ Visitor
11. ⏭️ Interpreter

**Next Pattern: Interpreter Pattern**, the final GoF design pattern, used to define and evaluate a grammar or language (for example mathematical expressions, rule engines, filters, and query languages).
