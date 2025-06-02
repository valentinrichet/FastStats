# Project Overview

## Goal

The objective of this project was to explore and experiment with the following technologies:

- **Aspire** (utilizing **PostgreSQL** and **Redis**)
- **Minimal API**
- **TestContainers** library
- **BenchmarkDotNet**

## Tools Used

- **Rider**
- **GitHub Copilot Agent**
- **Gemini Pro 2.5**

## Summary

This project served as a hands-on learning experience, and working with these technologies proved to be both insightful and engaging.

## Note

Migrations can be added using the following command (using AppHost project):

```bash
dotnet ef migrations add "Initialization" \
  --project FastStats.Infrastructure \
  --startup-project FastStats.AppHost \
  --output-dir Persistence/Migrations
