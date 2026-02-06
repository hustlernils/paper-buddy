# Paper Q&A Chat Feature (RAG) — Technical Specification

## Overview

Implement a Retrieval-Augmented Generation (RAG) system that allows users to ask questions about academic papers stored in the database.

The system will:

1. Split papers into chunks
2. Generate vector embeddings for each chunk
3. Store embeddings in PostgreSQL (pgvector)
4. On user query:
    - Embed the question
    - Retrieve relevant chunks via vector similarity
    - Send retrieved chunks + question to LLM
    - Return grounded answer

---

## Architecture

### Paper Upload
- Chunk Text
- Generate Embeddings
- Store in Postgres (pgvector)

### User Question
- Embed Question
- Vector Search
- Retrieve Top Chunks
- Prompt LLM
- Return Answer
## Dependencies

### Backend

- Npgsql
- Dapper
- pgvector PostgreSQL extension
- OpenAI SDK or HTTP client

### Models

- Chat: llama3.1
- Embeddings: nomic-embed-text

###  Database setup

- Ensure pg_vector is enabled.
- Table: paper_embeddings

**Index**:

## Paper ingestion flow

1. Chunk paper text
2. Create embeddings for chunks
3. Save embeddings in database

## Chat Flow

1. Embed user question
2. Vector similarity search
3. Construct prompt
4. Send chunk text returned by vector search as context to LLM
