
# CQRS With Projection

Sample .Net 6 CQRS Implementation with Onion Architecture, DDD and Outbox Pattern.

## CommandProject (API)

Command Project. Creates event and saves it to Outbox table.

 - Onion Architecture
 - DDD
 - Outbox Pattern
 - EntityFramework Core
 - MediatR
 - Postgresql

## BackgroundJob (Worker Service)

Reads CommandProject's Outbox table and sends messages to broker.

- MassTransit
- Dapper

## Projector (Worker Service)

Consumes messages and updates Redis database.

- MassTransit
- Redis

## QueryProject (API)

Query Project.

- MediatR
- Redis

