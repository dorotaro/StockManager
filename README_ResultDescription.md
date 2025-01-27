## STOCK MANAGEMENT APP

### Prerequisites
- .NET Framework 4.5
- .NET 7 SDK (C#11 is used), download [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

### Running the app
1. Select the appropriate Run/Debug Configuration - `StockManager.csproj` or `StockManagerWithAI.csproj`
2. Make sure the app builds and no dependencies missing
3. Open `Program.cs` 
   <br>3.1. edit this piece (line 13) `var dbFilePath = @"C:\repos\StockManager\Stocks4.db;"` so that it contains a path for local DB to be created
   <br>3.2. afterwards, feel free to comment out whole `#region Local Database Creation flow` to skip these prompts on each app re-run
4. Run or Debug

### Notes
- TODOs left over - did not cover them all, some would need some external input (f.e. data type for Stock Quantity), other just need more looking into (f.e. whether keeping the connection alive is ok for this implementation, performance-wise etc)
- Much more polishing and beautification possible

### Framework matters
- Overall, as .NET Framework 4.5 app built from scratch, there might be some misconfiguration - please allow some room for that
- Would need to double-check framework compatibility, whether all dependencies are available, whether the C# version is ok etc etc; especially before running this on a remote machine/server
- Works perfectly as is on my machine though :)


### Two Variants
- Hand-Coded Variant – `StockManager.csproj` - worked on this one first and longest, still did some googling; this is the one that has been tested most thoroughly, the DB files (`Stocks.db`, `Stocks2.db`, `Stocks3.db`) in `Misc` solution folder have all been created, and updated by testing this app
- AI-Assisted Variant – `StockManagerWithAI.csproj` - used AI tools for refactoring repository and service implementations, mostly cleanup, removed code duplication


#### Database Integration (Optional)
- Implement a repository interface (e.g., IStockRepository) that can be fulfilled by either an in-memory store or a SQLite store.
- For SQLite, provide a connection string and minimal instructions to run the SQL script for table creation.

#### Deliverables
- ✅ _Source Code (two variants)._ 
- ✅ _README explaining how to run each variant._
- ✔️ _Test Cases or example console input/output._
  - Not adding any unit tests - initially created some with AI tools, however did not want to spend more time (reviewing and fixing the tests) on this, therefore I'd rather not add any unreviewed code here
  - As per Console input/output, went through quite many of those - did not want to spend too much time on documenting each one separately, therefore You have to take my word for it - the DB files (`Stocks.db`, `Stocks2.db`, `Stocks3.db`) in `Misc` solution folder have all been created, and updated by testing the `StockManager.csproj` app
  - Also, the Console is quite intuitive (in my opinion at least), therefore anyone can take it for a spin
