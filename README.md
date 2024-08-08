# Message API Server

## Overview

The Message API Server is a backend application built with ASP.NET Core. It provides secure user authentication using JWT and endpoints for fetching and adding messages.

## Purpose

The main purpose of this project is to demonstrate a secure message handling system where users can authenticate, fetch, and add messages. The system ensures that only authenticated users can interact with the message endpoints, providing a secure communication channel.

## Features

- **User Authentication**: Secure login with JWT tokens.
- **Message Handling**: Fetch and add messages.

## Running the Server

1. Clone the repository.
2. Ensure you have .NET Core installed.
3. Navigate to the project directory.
4. Run the following commands:
   ```sh
   dotnet restore
   dotnet run
