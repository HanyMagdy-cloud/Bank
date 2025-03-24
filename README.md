# **Bank Management System API**

## **📌 Overview**

The **Bank Management System API** is a RESTful web service designed to handle various banking operations such as customer account management, fund transfers, loan management, and transaction history. The API is built using **ASP.NET Core** and **SQL Server** as the database.

---

## **🛠 Features**

✅ **Customer Management**: Create, update, and manage customer accounts.  
✅ **Account Handling**: Open new accounts, check balances, and manage different account types.  
✅ **Fund Transfers**: Secure money transfers between accounts.  
✅ **Loans**: Apply for and manage loans associated with customer accounts.  
✅ **Transaction History**: Track all account transactions, including deposits, withdrawals, and transfers.  
✅ **Authentication & Authorization**: Secure access using JWT tokens.

---

## **📂 Database Schema**

The database **BankAppData** consists of the following tables:

* **Accounts** – Stores account details (balance, type, created date).

* **AccountTypes** – Defines different types of accounts.

* **Cards** – Manages customer debit/credit cards.

* **Customers** – Stores customer personal details.

* **Dispositions** – Links customers to their accounts.

* **Loans** – Tracks customer loan details.

* **Transactions** – Logs all financial transactions.

## **🔐 Authentication & Security**

* Uses **JWT-based authentication**.

* Include a `Bearer Token` in requests for protected endpoints.

