CREATE DATABASE papers;
CREATE EXTENSION IF NOT EXISTS vector; -- enable pgvector extension

-- tables

CREATE TABLE IF NOT EXISTS users (
    id UUID PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name1 TEXT NOT NULL,
    email TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS projects (
    id UUID PRIMARY KEY, 
    user_id UUID REFERENCES users(id),
    title TEXT NOT NULL,
    description TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS papers (
    id UUID PRIMARY KEY,
    title TEXT NOT NULL,
    authors TEXT NOT NULL,
    year INT,
    doi TEXT NOT NULL,
    url TEXT NOT NULL,
    uploaded_by UUID REFERENCES users(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS books (
    paper_id UUID PRIMARY KEY REFERENCES papers(id),
    publisher TEXT NOT NULL,
    isbn TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS articles (
    paper_id UUID PRIMARY KEY REFERENCES papers(id),
    journal TEXT NOT NULL,
    volume TEXT NOT NULL,
    number TEXT NOT NULL,
    pages TEXT NOT NULL,
    issn TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS inproceedings (
    paper_id UUID PRIMARY KEY REFERENCES papers(id),
    publisher TEXT NOT NULL,
    booktitle TEXT NOT NULL,
    editor TEXT NOT NULL,
    volume TEXT NOT NULL,
    number TEXT NOT NULL,
    pages TEXT NOT NULL,
    address TEXT NOT NULL,
    month INT NOT NULL
);

CREATE TABLE IF NOT EXISTS paper_data (
    id UUID PRIMARY KEY,
    paper_id UUID REFERENCES papers(id),
    data BYTEA,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS paper_embeddings (
    id UUID PRIMARY KEY,
    paper_id UUID REFERENCES papers(id),
    embedding VECTOR(1536),
    chunk_id TEXT, 
    text TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS notes (
    id UUID PRIMARY KEY,
    user_id UUID REFERENCES users(id),
    parent_type TEXT NOT NULL,  
    parent_id UUID,
    content TEXT NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS chats (
    id UUID PRIMARY KEY,
    parent_type TEXT NOT NULL, 
    parent_id UUID,
    user_id UUID REFERENCES users(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS chat_messages (
    id UUID PRIMARY KEY,
    chat_id UUID REFERENCES chats(id), 
    user_id UUID REFERENCES users(id),
    role TEXT NOT NULL, -- user or system
    content TEXT NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- indices

CREATE INDEX paper_embeddings_vector_idx
ON paper_embeddings
USING hnsw (embedding);

-- FK indices

CREATE INDEX idx_paper_data_paper_id ON paper_data(paper_id);
CREATE INDEX idx_projects_user_id ON projects(user_id);
CREATE INDEX idx_papers_uploaded_by ON papers(uploaded_by);
CREATE INDEX idx_notes_parent_id ON notes(parent_id);
CREATE INDEX idx_chats_parent_id ON chats(parent_id);
CREATE INDEX idx_chat_messages_chat_id ON chat_messages(chat_id);
CREATE INDEX idx_chat_messages_user_id ON chat_messages(user_id);

-- polymorphic queries indices

CREATE INDEX idx_notes_parent_type_id
ON notes(parent_type, parent_id);

CREATE INDEX ixs_chats_parent_type_id
ON chats(parent_type, parent_id);

-- unique index

CREATE UNIQUE INDEX idx_paper_embeddings_chunk 
ON paper_embeddings(paper_id, chunk_id);