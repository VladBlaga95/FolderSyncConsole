# FolderSyncConsole

This project is a simple folder synchronization tool that maintains a full, identical copy of a source folder in a replica folder.

## Features

- One-way synchronization from source to replica.
- Periodic synchronization at specified intervals.
- Logging of file creation/copying/removal operations to a file and console output.
- Configuration through command line arguments.

## Getting Started

### Prerequisites

- .NET SDK installed
- Git (optional)

### Installation

```bash
# Clone the repository
https://github.com/VladBlaga95/FolderSyncConsole.git

# Build the project
dotnet build

# Usage

dotnet run -- sorucePath replicaPath interval logPath
example : dotnet run -- C:\SourceFolder C:\ReplicaFolder 60 C:\Logs\sync.log
