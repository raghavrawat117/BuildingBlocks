Excellent. The **Bridge Pattern** is often confused with Adapter because both use composition, but they solve completely different problems.

> **Adapter** makes existing incompatible classes work together.
>
> **Bridge** separates an abstraction from its implementation so both can evolve independently.

***

# Bridge Pattern

## Definition

The Bridge Pattern decouples an abstraction from its implementation, allowing the two hierarchies to vary independently.

***

# Problem Statement

Suppose you're building a notification system.

You have:

### Notification Types

* Email Notification
* SMS Notification
* Alert Notification

And Delivery Channels

* WhatsApp
* Teams
* Slack

If you use inheritance only:

```text
EmailWhatsAppNotification
EmailTeamsNotification
EmailSlackNotification

SmsWhatsAppNotification
SmsTeamsNotification
SmsSlackNotification

AlertWhatsAppNotification
AlertTeamsNotification
AlertSlackNotification
```

As more notification types and channels are added, classes explode.

This is called the **Cartesian Product Problem**.

***

# Solution

Separate:

### Abstraction

```text
Notification
```

from

### Implementation

```text
Message Sender
```

Then connect them using composition.

```text
Notification
      |
      v
 IMessageSender
```

***

# Structure

```text
            Notification
                 |
        -----------------
        |               |
 EmailNotification  AlertNotification

                 |
                 v

          IMessageSender
                 ^
        ------------------
        |                |
   TeamsSender     SlackSender
```

Both hierarchies can grow independently.

***

# Console Application Example

## Step 1: Implementor

### IMessageSender.cs

```csharp
namespace BridgePatternDemo
{
    public interface IMessageSender
    {
        void SendMessage(string message);
    }
}
```

***

## Step 2: Concrete Implementors

### EmailSender.cs

```csharp
using System;

namespace BridgePatternDemo
{
    public class EmailSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending Email: {message}");
        }
    }
}
```

### SmsSender.cs

```csharp
using System;

namespace BridgePatternDemo
{
    public class SmsSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
        }
    }
}
```

***

## Step 3: Abstraction

### Notification.cs

```csharp
namespace BridgePatternDemo
{
    public abstract class Notification
    {
        protected IMessageSender MessageSender;

        protected Notification(IMessageSender messageSender)
        {
            MessageSender = messageSender;
        }

        public abstract void Send(string message);
    }
}
```

Notice:

```csharp
Notification
     HAS-A
IMessageSender
```

This is the bridge.

***

## Step 4: Refined Abstractions

### AlertNotification.cs

```csharp
namespace BridgePatternDemo
{
    public class AlertNotification : Notification
    {
        public AlertNotification(
            IMessageSender messageSender)
            : base(messageSender)
        {
        }

        public override void Send(string message)
        {
            MessageSender.SendMessage(
                $"[ALERT] {message}");
        }
    }
}
```

***

### ReminderNotification.cs

```csharp
namespace BridgePatternDemo
{
    public class ReminderNotification : Notification
    {
        public ReminderNotification(
            IMessageSender messageSender)
            : base(messageSender)
        {
        }

        public override void Send(string message)
        {
            MessageSender.SendMessage(
                $"[REMINDER] {message}");
        }
    }
}
```

***

## Step 5: Client

### Program.cs

```csharp
using System;

namespace BridgePatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessageSender emailSender =
                new EmailSender();

            Notification alert =
                new AlertNotification(emailSender);

            alert.Send("Server is down!");

            IMessageSender smsSender =
                new SmsSender();

            Notification reminder =
                new ReminderNotification(smsSender);

            reminder.Send("Submit timesheet.");

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Sending Email: [ALERT] Server is down!

Sending SMS: [REMINDER] Submit timesheet.
```

***

# Why This Is Powerful

Without Bridge:

```text
AlertEmailNotification
AlertSmsNotification

ReminderEmailNotification
ReminderSmsNotification

PromotionEmailNotification
PromotionSmsNotification
```

If you add:

```text
WhatsAppSender
```

you must create many more classes.

***

With Bridge:

Add only:

```csharp
public class WhatsAppSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine(
            $"Sending WhatsApp: {message}");
    }
}
```

No existing notification classes change.

***

# Real-World Example

Imagine a report system.

## Report Types

```text
SalesReport
InventoryReport
EmployeeReport
```

## Export Types

```text
PDF
Excel
CSV
```

Without Bridge:

```text
SalesPdfReport
SalesExcelReport
SalesCsvReport

InventoryPdfReport
InventoryExcelReport
InventoryCsvReport
```

Huge class growth.

***

With Bridge:

### Abstraction

```csharp
Report
```

### Implementation

```csharp
IExporter
```

Concrete exporters:

```csharp
PdfExporter
ExcelExporter
CsvExporter
```

Now any report can use any exporter.

***

# UML (Simplified)

```text
           Notification
                 |
        -----------------
        |               |
      Alert         Reminder

                 |
                 v

         IMessageSender
           ^         ^
           |         |
      EmailSender  SmsSender
```

***

# Bridge vs Adapter

This is a favorite interview question.

## Adapter

Makes incompatible interfaces work together.

```text
Old System
     |
  Adapter
     |
New System
```

Existing code is adapted.

***

## Bridge

Separates two dimensions that may change independently.

```text
Abstraction
     |
  Bridge
     |
Implementation
```

Designed upfront.

***

# Bridge vs Strategy

Another common confusion.

### Strategy

Changes behavior/algorithm.

```csharp
IPaymentStrategy
```

Examples:

```text
Credit Card
UPI
PayPal
```

Focus:

✅ How something is done

***

### Bridge

Separates abstraction from implementation.

```csharp
Notification
     +
Sender
```

Focus:

✅ Independent hierarchies

***

# Real-World .NET Example

One commonly cited example is ADO.NET.

Abstraction:

```csharp
DbCommand
DbConnection
```

Implementations:

```csharp
SqlConnection
OracleConnection
MySqlConnection
```

Your application works against the abstraction while specific providers supply the implementation.

***

# Advantages

✅ Prevents class explosion

✅ Follows Composition Over Inheritance

✅ Supports Open/Closed Principle

✅ Abstraction and implementation evolve independently

✅ Easier maintenance

***

# Disadvantages

❌ More classes

❌ Slightly more complex to understand

❌ Can be overkill for small systems

***

# When to Use

Use Bridge when:

* Two dimensions can change independently.
* Inheritance is causing too many subclasses.
* You want composition instead of deep inheritance chains.

Examples:

* Notifications + Delivery Channels
* Reports + Export Formats
* Devices + Remote Controls
* Messages + Communication Providers
* Documents + Storage Providers

***

# Interview Question

### Q: What problem does the Bridge Pattern solve?

**Answer:**

The Bridge Pattern prevents a combinatorial explosion of subclasses by separating abstraction and implementation into independent hierarchies connected through composition.

Instead of:

```text
AlertEmailNotification
AlertSmsNotification
ReminderEmailNotification
ReminderSmsNotification
```

you create:

```text
Notification Hierarchy
        +
Sender Hierarchy
```

and combine them at runtime.

***

## Bridge Pattern in One Line

> **Separate an abstraction from its implementation so that both can vary independently.**

✅ **Structural Pattern #2 Complete: Bridge Pattern**

### Structural Patterns Progress

1. ✅ Adapter
2. ✅ Bridge
3. ⏭️ Composite
4. ⏭️ Decorator
5. ⏭️ Facade
6. ⏭️ Flyweight
7. ⏭️ Proxy

**Next Pattern: Composite Pattern**, which lets you treat individual objects and groups of objects uniformly, such as files and folders in a file system.
