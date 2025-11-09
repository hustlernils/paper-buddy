#!/usr/bin/env bash
set -e

# -------------------------------------------------
# CONFIG
# -------------------------------------------------
EMBED_MODEL="nomic-embed-text"
SUMMARY_MODEL="llama3.1"
OLLAMA_HOST="http://localhost:11434"

# -------------------------------------------------
# FUNCTIONS
# -------------------------------------------------

function check_ollama_installed() {
  if ! command -v ollama &> /dev/null; then
    echo "❌ Ollama is not installed. Please install it from https://ollama.ai/download"
    exit 1
  fi
}

function start_ollama() {
  if ! pgrep -x "ollama" > /dev/null; then
    echo "🚀 Starting Ollama..."
    nohup ollama serve > /dev/null 2>&1 &
    sleep 3
  else
    echo "✅ Ollama server already running."
  fi
}

function check_model() {
  local MODEL_NAME=$1
  if ! ollama list | grep -q "$MODEL_NAME"; then
    echo "⬇️ Pulling model: $MODEL_NAME ..."
    ollama pull "$MODEL_NAME"
  else
    echo "✅ Model '$MODEL_NAME' already available."
  fi
}

echo "🔧 Setting up Ollama models..."

check_ollama_installed
start_ollama
check_model "$EMBED_MODEL"
check_model "$SUMMARY_MODEL"

echo "✅ Models are ready!"
echo
echo "🎉 Setup complete! Ollama is running and ready for embeddings + summaries."



