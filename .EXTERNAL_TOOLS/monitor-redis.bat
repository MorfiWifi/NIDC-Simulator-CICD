@echo off
mode con: cols=%1 lines=%2
python redis-monitor.py