Excellent. We're now entering the **Behavioral Patterns** category.

The **Chain of Responsibility (CoR)** pattern is extremely common in modern .NET applications. You'll find it in:

* ASP.NET Core Middleware
* Exception Handling Pipelines
* Request Validation
* Approval Workflows
* Authentication & Authorization Pipelines

***

# Chain of Responsibility Pattern

## Definition

The Chain of Responsibility Pattern allows a request to pass through a chain of handlers until one of them handles it.

Each handler:

1. Receives the request.
2. Decides whether it can handle it.
3. If not, passes it to the next handler.

***

# Real-World Analogy

Think of an expense approval process.

```text
Employee Request
       |
       v
Team Lead
       |
       v
Manager
       |
       v
Director
```

Example:

```text
₹5,000   -> Team Lead approves
₹25,000  -> Manager approves
₹1,00,000 -> Director approves
```

Each approver either handles the request or forwards it.

***

# Problem Statement

Without CoR:

```csharp
if(amount <= 10000)
{
    TeamLeadApprove();
}
else if(amount <= 50000)
{
    ManagerApprove();
}
else
{
    DirectorApprove();
}
```

Problems:

❌ Long conditional statements

❌ Difficult to extend

❌ Violates Open/Closed Principle

***

# Solution Structure

```text
Request
   |
   v
TeamLead
   |
   v
Manager
   |
   v
Director
```

Each handler knows only its successor.

***

# Console Application Example

## Step 1: Handler

### ApprovalHandler.cs

```csharp
namespace ChainOfResponsibilityDemo
{
    public abstract class ApprovalHandler
    {
        protected ApprovalHandler NextHandler;

        public void SetNext(ApprovalHandler nextHandler)
        {
            NextHandler = nextHandler;
        }

        public abstract void Approve(decimal amount);
    }
}
```

***

## Step 2: Concrete Handlers

### TeamLead.cs

```csharp
using System;

namespace ChainOfResponsibilityDemo
{
    public class TeamLead : ApprovalHandler
    {
        public override void Approve(decimal amount)
        {
            if (amount <= 10000)
            {
                Console.WriteLine(
                    $"Team Lead approved ₹{amount}");
            }
            else
            {
                NextHandler?.Approve(amount);
            }
        }
    }
}
```

***

### Manager.cs

```csharp
using System;

namespace ChainOfResponsibilityDemo
{
    public class Manager : ApprovalHandler
    {
        public override void Approve(decimal amount)
        {
            if (amount <= 50000)
            {
                Console.WriteLine(
                    $"Manager approved ₹{amount}");
            }
            else
            {
                NextHandler?.Approve(amount);
            }
        }
    }
}
```

***

### Director.cs

```csharp
using System;

namespace ChainOfResponsibilityDemo
{
    public class Director : ApprovalHandler
    {
        public override void Approve(decimal amount)
        {
            if (amount <= 200000)
            {
                Console.WriteLine(
                    $"Director approved ₹{amount}");
            }
            else
            {
                Console.WriteLine(
                    "Amount exceeds approval limits.");
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

namespace ChainOfResponsibilityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ApprovalHandler teamLead = new TeamLead();
            ApprovalHandler manager = new Manager();
            ApprovalHandler director = new Director();

            teamLead.SetNext(manager);
            manager.SetNext(director);

            teamLead.Approve(5000);
            teamLead.Approve(25000);
            teamLead.Approve(150000);

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Team Lead approved ₹5000

Manager approved ₹25000

Director approved ₹150000
```

***

# Request Flow

For:

```csharp
teamLead.Approve(25000);
```

Execution:

```text
TeamLead
   ↓
Cannot approve
   ↓
Manager
   ↓
Approves
```

***

For:

```csharp
teamLead.Approve(150000);
```

Execution:

```text
TeamLead
   ↓
Manager
   ↓
Director
   ↓
Approves
```

***

# More Realistic Example: Support Ticket System

Imagine customer support levels.

```text
Level 1 Support
     ↓
Level 2 Support
     ↓
Engineering Team
```

Simple issues:

```text
Password Reset
```

handled by Level 1.

Complex issues:

```text
Server Crash
```

escalated to Engineering.

***

## Handler Interface

```csharp
public abstract class SupportHandler
{
    protected SupportHandler Next;

    public void SetNext(SupportHandler next)
    {
        Next = next;
    }

    public abstract void Handle(string issue);
}
```

***

## Level 1

```csharp
public class Level1Support : SupportHandler
{
    public override void Handle(string issue)
    {
        if (issue == "Password Reset")
        {
            Console.WriteLine(
                "Handled by Level 1");
        }
        else
        {
            Next?.Handle(issue);
        }
    }
}
```

***

## Engineering

```csharp
public class EngineeringSupport : SupportHandler
{
    public override void Handle(string issue)
    {
        Console.WriteLine(
            "Handled by Engineering");
    }
}
```

***

# ASP.NET Core Middleware Example

This is arguably the best real-world example of CoR.

Request Pipeline:

```text
HTTP Request
     ↓
Authentication Middleware
     ↓
Authorization Middleware
     ↓
Logging Middleware
     ↓
Controller
```

Each middleware:

```csharp
await next();
```

passes execution to the next handler.

Example:

```csharp
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
```

This is essentially a Chain of Responsibility.

***

# UML (Simplified)

```text
            Handler
               ▲
      -------------------
      |        |        |
 TeamLead  Manager  Director

TeamLead -> Manager -> Director
```

***

# Chain of Responsibility vs Strategy

A common interview question.

### Strategy

Chooses one algorithm.

```text
Payment Strategy
 ├── UPI
 ├── Credit Card
 └── PayPal
```

Only one strategy executes.

***

### Chain of Responsibility

Multiple handlers may participate.

```text
Handler1
   ↓
Handler2
   ↓
Handler3
```

Request travels through a chain.

***

# Chain of Responsibility vs Command

### Command

Encapsulates an action.

```csharp
command.Execute();
```

Focus:

✅ Action

***

### Chain of Responsibility

Routes a request.

```csharp
handler.Handle(request);
```

Focus:

✅ Processing pipeline

***

# Advantages

✅ Reduces coupling

✅ Easily add/remove handlers

✅ Follows Open/Closed Principle

✅ Supports dynamic chains

✅ Cleaner than large if/else blocks

***

# Disadvantages

❌ Hard to debug long chains

❌ Request may go unhandled

❌ Execution path may not be obvious

❌ Performance overhead for very long chains

***

# When to Use

Use Chain of Responsibility when:

* Multiple objects can handle a request.
* The handler is determined at runtime.
* You want flexible processing pipelines.
* You want to avoid large conditional logic.

Examples:

* Approval workflows
* Middleware pipelines
* Authentication checks
* Validation pipelines
* Logging chains
* Customer support escalation

***

# Interview Question

### Q: What happens if no handler processes the request?

**Answer:**

The request reaches the end of the chain and remains unhandled.

Example:

```csharp
Director
   ↓
No Next Handler
```

You can either:

* Ignore the request
* Throw an exception
* Return an error response

depending on business requirements.

***

# Chain of Responsibility Pattern in One Line

> **Pass a request through a chain of handlers until one of them processes it.**

✅ **Behavioral Pattern #1 Complete: Chain of Responsibility**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ⏭️ Command
3. ⏭️ Interpreter
4. ⏭️ Iterator
5. ⏭️ Mediator
6. ⏭️ Memento
7. ⏭️ Observer
8. ⏭️ State
9. ⏭️ Strategy
10. ⏭️ Template Method
11. ⏭️ Visitor

A practical next step is **Observer Pattern**, because it's the foundation behind events, event handlers, messaging systems, and reactive programming in .NET.
