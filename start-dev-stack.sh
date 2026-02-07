#!/usr/bin/env bash
set -e

echo "🚀 Starting postgres..."
docker compose -f ./infra/docker-compose.yml up -d

echo "🧠 Starting Ollama setup..."
bash ./infra/ollama.sh

echo "✅ Everything is up and running!"
