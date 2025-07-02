# Udep Encuestas API

This repository contains a proof-of-concept .NET 9 Web API using an n-layer architecture. It demonstrates
integration with Dapper, JWT authentication with refresh tokens, and a simple domain based on the
`DBENCUESTAS_UDEP.sql` script.

## Structure

- `src/UdepEncuestas.Core` – domain models.
- `src/UdepEncuestas.Data` – data access layer using Dapper.
- `src/UdepEncuestas.Services` – business services and JWT generation.
- `src/UdepEncuestas.Api` – ASP.NET Core API exposing endpoints.

JWT tokens expire after one hour and a refresh endpoint issues new tokens.
Secrets such as the JWT signing key and connection string are read from configuration or environment variables.
