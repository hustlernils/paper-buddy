-- Migration to make title, authors, doi, and url columns nullable in papers table

ALTER TABLE papers ALTER COLUMN title DROP NOT NULL;
ALTER TABLE papers ALTER COLUMN authors DROP NOT NULL;
ALTER TABLE papers ALTER COLUMN doi DROP NOT NULL;
ALTER TABLE papers ALTER COLUMN url DROP NOT NULL;