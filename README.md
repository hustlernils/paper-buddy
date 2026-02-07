# PaperBuddy

Your digital writing buddy.

Build with React + shadcn/ui

# Setup

Start database, api and ollama:

``` bash
./start-stack.sh
```

Start only database and ollama:

``` bash
./start-dev-stack.sh
```

Start frontend

``` bash
cd /src/frontend
```

``` bash
deno task dev
```

# Database Migrations

Migration scripts are found under `./script/migrations`. To run a migration use:

```bash
cd scripts
```

```bash
bash ./run-migration.sh <migration_file.sql>
```

## Roadmap & TODO List

### Main Features
- [ ] **Paper Content Analysis**:
  - [x] Implement AI-powered summarization
  - [ ] Save paper contents as embeddings for similarity search
- [ ] **Multi-Paper Chat**: Enable project-level chats that reference all associated papers (e.g., "Summarize findings from all papers in this project").
- [ ] **Citation Management**: Add auto-generation of citations (BibTeX, APA) and a reference tracker in the UI.
- [ ] **Thesis Writing Tools**: AI-assisted outline generation and draft prompts based on project content.

### Nice to haves:
- [ ] **User Authentication**: Add login/signup to save projects across sessions.
- [ ] **External Integrations**: Connect to arXiv/Semantic Scholar for paper fetching and metadata enrichment.
