ALTER TABLE paper_embeddings ALTER COLUMN embedding TYPE VECTOR(768); -- only works if colomn is empty 
ALTER TABLE paper_embeddings RENAME COLUMN "text" TO content;