Payment Solution: Luis Fernando Alvarez YAPE

This system consists of two microservices:

TransactionService: exposes endpoints to create and query transactions.
AntiFraudService: validates transactions according to anti-fraud rules and updates their status.
Communication between services is done via Kafka.

Architecture
plain text
+-------------------+         Kafka         +-------------------+
|                   |  TransactionCreated  |                   |
|  Transaction API  +--------------------->+  Anti-Fraud API   |
|                   |                      |                   |
+-------------------+                      +-------------------+
        |                                         |
        |   TransactionStatusUpdated               |
        +<----------------------------------------+
TransactionService publishes events for created transactions.
AntiFraudService consumes these events, applies rules, and publishes the updated status.

Anti-Fraud Rules
The service rejects transactions with a value > 2000.

The service rejects transactions if the user's daily total exceeds 20000.

In other cases, it approves the rest.

Local Execution
Clone the repository.

Build and start the services:

bash
docker-compose up --build
Available APIs:

TransactionService: 

AntiFraudService: 

Unit Tests
From the folder of each service:

bash
dotnet test
Main Endpoints
POST /transaction: Creates a transaction (initial status: pending).

GET /transaction/{id}: Queries the status of a transaction.

Notes
The database is in-memory to facilitate testing.