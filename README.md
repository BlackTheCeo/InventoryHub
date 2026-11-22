# InventoryHub - Full-Stack Integration Project

## Project Overview
InventoryHub is a full-stack inventory management application built with a Blazor WebAssembly front-end (`ClientApp`) and an ASP.NET Core Minimal API back-end (`ServerApp`).

## Reflective Summary: How Microsoft Copilot Assisted

1. **Generating and Refining Integration Code:**
   Microsoft Copilot accelerated scaffolding the Minimal API `/api/productlist` endpoint and the Blazor `FetchProducts.razor` component. It suggested using `HttpClient` inside `OnInitializedAsync()` and generated clean C# record types to model the inventory items cleanly.

2. **Debugging and Resolving Integration Issues:**
   During initial testing, cross-origin requests from the Blazor client were blocked by browser security policies, and route mismatches caused `404` errors. Copilot identified the missing CORS middleware and generated the `AddCors` / `UseCors` configuration in `Program.cs`, as well as `try-catch` and `CancellationTokenSource` handling for malformed responses or timeouts.

3. **Creating and Managing JSON Structures:**
   Copilot helped expand the flat product payload into a structured, industry-standard JSON format by introducing a nested `Category` object (`Id` and `Name`). It also ensured proper deserialization on the client side using `JsonSerializerOptions { PropertyNameCaseInsensitive = true }`.

4. **Optimizing Integration Code for Performance:**
   To eliminate redundant network calls and reduce server load, Copilot recommended implementing `IMemoryCache` on the back-end with a 5-minute expiration policy and adding a state-check guard (`if (products != null) return;`) on the front-end so cached data is reused across component renders.
