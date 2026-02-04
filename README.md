# PaperBuddy

Your digital writing buddy.

Build with React + shadcn/ui

# Setup

Start database and ollama:

``` bash
./start-stack.sh
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

### High Priority
- [ ] **Paper Content Analysis**:
  - [x] Implement AI-powered summarization
  - [ ] key point extraction
  - [ ] keyword tagging for uploaded PDFs.
- [ ] **Multi-Paper Chat**: Enable project-level chats that reference all associated papers (e.g., "Summarize findings from all papers in this project").
- [ ] **Citation Management**: Add auto-generation of citations (BibTeX, APA) and a reference tracker in the UI.

### Medium Priority
- [ ] **Advanced Search & Filtering**: Add full-text search across papers, filter by author/date/topic.
- [ ] **Annotation Tools**: Allow highlighting/annotating paper sections, linking to project notes.
- [ ] **Thesis Writing Tools**: AI-assisted outline generation and draft prompts based on project content.

### Low Priority
- [ ] **User Authentication**: Add login/signup to save projects across sessions.
- [ ] **Export Features**: Export project summaries, chats, or citations as PDF/Doc.
- [ ] **External Integrations**: Connect to arXiv/Semantic Scholar for paper fetching and metadata enrichment.
- [ ] **Bulk Operations**: Support uploading multiple papers at once, batch analysis.
