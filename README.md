## README.md

### Project Overview

This project is a backend service built with ASP.NET Core, designed to handle user authentication and message processing. The service allows users to securely send and retrieve messages, utilizing HMAC signatures to ensure the integrity and authenticity of the data exchanged.

### Features

- **User Verification**: Validates users based on their unique ID.
- **Message Handling**: Users can post and fetch messages with timestamps.
- **Security**: Implements HMAC signatures for secure communication between the client and server.

### Getting Started

**Build and Run**:
   ```bash
   dotnet build
   dotnet run
   ```

### API Overview

- **User Verification**: `/api/user/{id}` (GET)
- **Post a Message**: `/api/message` (POST)
- **Get Messages**: `/api/message` (GET)
 