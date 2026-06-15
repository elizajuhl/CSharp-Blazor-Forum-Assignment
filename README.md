# C# Blazor Forum Assignment

This is a 3rd semester student assignment from my Software Engineering studies.

The goal of the assignment was to build a small forum application inspired by Reddit, using C# and Blazor. The project was part of the DNP course and represents my earlier learning stage with .NET, Blazor, layered architecture, Web API, and simple data storage.

## Project overview

The application is a simple forum-style system where users can create and view posts.

The original assignment requirements included:

* user registration, login, and logout
* creating a new post when logged in
* viewing an overview of all posts
* opening a post to view the full content

## Technologies used

* C#
* .NET
* Blazor Server
* ASP.NET Web API
* JSON file storage
* HTTP client services
* Layered project structure

## Project structure

* `BlazorAppReddit` – Blazor user interface
* `BusinessLogicWithRestApi` – Web API and backend logic
* `Domain` – model classes and data access contracts
* `FileData` – JSON-based data access
* `HttpServices` – HTTP client service layer
* `AssignmentOneDNP.sln` – solution file

## Assignment scope

This repository includes a forum application with a Blazor UI, JSON-based storage, and a Web API / HTTP client layer.

It does not include the later Entity Framework Core and SQLite database version at this stage.

## Note

This is a 3rd semester school assignment and does not necessarily reflect how I would structure or implement the same type of application today.

I keep it here as a small learning project and as part of my programming development history.
