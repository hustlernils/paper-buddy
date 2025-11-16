# Paper Buddy - Architecture Overview

# Purpose

A personal application for summarizing scientific papers. It allows uploading PDF papers, automatically extracts text, generates summaries and embeddings, and stores them for retrieval and semantic search.

### Core Application Components

#### **Web App / API Server**

**Technology:** ASP.NET WebApi with minimal endpoints

**Responsibilities:**

- Accepts PDF uploads.
- Enqueues background jobs for PDF processing.
- Returns processed summaries to the UI.
- Handles semantic search requests.

#### **In-Memory Event Bus**

**Technology:** C#, Channels

**Responsibilities:**

- Accept and enqueue Jobs
- Deliver jobs asynchronously to the background worker(s).
- Decouple the API from the worker, allowing the worker to process jobs at its own pace.

#### **Background Worker**

**Technology:** .NET BackgroundWorker

**Responsibilities:**

- Consumes jobs from the queue.
- Reads PDF metadata and contents from the database.
- Extracts text from PDFs.
- Calls the LLM service to generate:
  - Summaries
  - Description / metadata
  - Vector embeddings
- Stores extracted text and summaries back into the relational database.
- Stores vector embeddings into the vector database.

### Data Storage

#### **Relational Database**

**Technology:** PostgreSQL

**Responsibilities:**

- Stores paper metadata (title, author, upload timestamp).
- Stores the uploaded PDF file itself (as a `bytea` column).
- Stores extracted text from the PDF.
- Stores the generated summary and description.

#### **Vector Database**

**Technology:** PGVector

**Responsibilities:**

- Stores vector embeddings for semantic search.
- Enables similarity search across papers.


# C4 Container Diagram

```mermaid
C4Container
title Paper Buddy- Container Architecture

Person(user, "User", "Me uploading a scientific paper")

System_Boundary(boundary, "Paper-Buddy"){
    Container(api, "Web App / API Server", "C#", "Handles uploads, triggers background jobs")
    Container(channel, "In-memory Event Bus / Message Broker", "C# Channels", "Handles background job events asynchronously between API and Worker")
    Container(worker, "Background Worker", "WorkerService", "Processes PDFs, extracts text, generates summaries & embeddings")
    ContainerDb(relationalDb, "Application Database", "PostgreSQL", "Stores paper data and summaries")
    ContainerDb(vectorDb, "Vector Database", "PGVector", "Stores vector embeddings for semantic search")
}

System_Ext(llm, "LLM / Embedding Service", "Ollama/LLama3.1/nomic-embed-text", "Used for summary + embedding generation")

Rel(user, api, "Uploads PDF / requests summaries")
Rel(api, channel, "Publishes job events")
Rel(channel, worker, "Worker subscribes to events")
Rel(worker, relationalDb, "Reads PDF")
Rel(worker, llm, "Generates summaries and embeddings")
Rel(worker, relationalDb, "Stores text + summary + metadata")
Rel(worker, vectorDb, "Stores vector embeddings")
Rel(api, relationalDb, "Reads summary for UI")
Rel(api, vectorDb, "Semantic search")

UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

# Sequence Diagram

```mermaid
sequenceDiagram
    %% Participants
    participant User
    participant API as API / Web Server
    participant Queue@{ "type" : "queue" }
    participant W as Worker
    participant LLM as LLM / Embedding Service
    participant RelationalDb@{ "type" : "database" }
    participant VectorDb@{ "type" : "database" }

    %% Upload flow
    User ->> API: Upload PDF
    activate API
    API ->> RelationalDb: Store PDF file
    API ->> Queue: Enqueue job (process PDF)
    API -->> User: Accepted
    deactivate API

    %% Background worker picks up job
    Queue -->> W: Deliver job
    activate W

    %% PDF processing
    W ->> RelationalDb: Download PDF
    W ->> W: Extract text from PDF

    %% LLM / embedding
    W ->> LLM: Request summary & description
    activate LLM
    LLM -->> W: Return summary + description
    deactivate LLM

    W ->> LLM: Request embeddings for text and summary
    activate LLM
    LLM -->> W: Return embeddings
    deactivate LLM

    %% Store results
    W ->> RelationalDb: Save metadata + extracted text + summary
    W ->> VectorDb: Save embeddings

    deactivate W

    %% Return to user via API when ready
    User ->> API: Request summary / status
    activate API
    API ->> RelationalDb: Read summary & metadata
    API -->> User: Display summary, status, etc.
    deactivate API

    %% Optional: semantic search
    User ->> API: Search by query (semantic)
    activate API
    API ->> VectorDb: Query similar embeddings
    VectorDb -->> API: Return matching paper IDs
    API ->> RelationalDb: Fetch metadata for matched papers
    API -->> User: Return search results
    deactivate API
```