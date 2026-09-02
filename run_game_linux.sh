#!/bin/bash
echo "=== Starting Cyber Bank Infiltration Linux Executable ==="
python3 -m http.server 8080 --directory /home/bleufire/cyber-bank-hacker > /dev/null 2>&1 &
SERVER_PID=$!
sleep 1

if which xdg-open > /dev/null; then
    xdg-open http://localhost:8080/cyber_hacker_game.html
elif which google-chrome > /dev/null; then
    google-chrome --app=http://localhost:8080/cyber_hacker_game.html
elif which firefox > /dev/null; then
    firefox http://localhost:8080/cyber_hacker_game.html
fi

echo "Game launched on Linux! Server PID: $SERVER_PID"
