#!/bin/bash

# Script to run a SQL migration against the PostgreSQL database running in Docker
# Usage: ./scripts/run-migration.sh <migration_file.sql>

if [ $# -ne 1 ]; then
    echo "Usage: $0 <migration_file.sql>"
    exit 1
fi

MIGRATION_FILE="./migrations/$1"

if [ ! -f "$MIGRATION_FILE" ]; then
    echo "Migration file $MIGRATION_FILE not found"
    exit 1
fi

# Run the migration using docker exec
cat "$MIGRATION_FILE" | docker exec -i postgres psql -U postgres -d papers

if [ $? -eq 0 ]; then
    echo "Migration $1 executed successfully"
else
    echo "Migration $1 failed"
    exit 1
fi