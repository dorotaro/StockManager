## STOCK MANAGEMENT SYSTEM CHALLENGE

### Task Overview

You need to create a simple Stock Management System in .NET Framework 4.5 that manages a list of stock items (financial instruments), each identified by an ISIN. The system must allow:

- Adding new stock items.
- Updating existing items’ price or quantity by ISIN.
- Viewing all stock items.
- Querying stock items below a certain quantity threshold.
- Using LINQ to search or filter items in-memory.
- Optional: Using a database (SQLite) for storage instead of in-memory.

#### Two Variants

Please produce two code variants of your solution:

- Hand-Coded Variant – Written entirely by you.
- AI-Assisted Variant – You may use any AI coding tool (e.g., ChatGPT, Copilot) to generate or refactor code.
Choose which variant to develop first—the order is up to you. In your submission, please label each variant clearly so we can compare them.

### Requirements

#### Core Functionality
- StockItem: Fields should include Name, ISIN, Price, and Quantity.
- Add Stock: Prevent duplicates by ISIN.
- Update Stock: Handle scenarios where the stock does not exist.
- List All Stocks: Display current data for each item.
- Query Below Threshold: Show any stock with Quantity < threshold.
- Search: Implement at least one LINQ operation to search by partial name or exact ISIN.

#### Database Integration (Optional)
- Implement a repository interface (e.g., IStockRepository) that can be fulfilled by either an in-memory store or a SQLite store.
- For SQLite, provide a connection string and minimal instructions to run the SQL script for table creation.

#### Deliverables
- Source Code (two variants).
- README explaining how to run each variant.
- Test Cases or example console input/output.