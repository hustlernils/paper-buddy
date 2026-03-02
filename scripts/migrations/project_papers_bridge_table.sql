CREATE TABLE IF NOT EXISTS project_papers (
    paper_id UUID REFERENCES papers(id) ON DELETE CASCADE,
    project_id UUID REFERENCES projects(id) ON DELETE CASCADE,
    PRIMARY KEY (project_id, paper_id)
);

CREATE INDEX idx_project_papers_paper_id ON project_papers (paper_id);