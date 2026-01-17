#!/usr/bin/env bash
set -e

echo "🚀 Starting database..."
docker compose -f ./docker-compose.yml up -d

echo "🧠 Starting Ollama setup..."
bash ./infra/ollama.sh

echo "✅ Everything is up and running!"
