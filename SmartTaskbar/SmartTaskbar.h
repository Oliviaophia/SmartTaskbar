#pragma once

APPBARDATA msgData;

WINDOWPLACEMENT placement;

HWND maxWindow;

POINT cursor;

bool tryShowBar;

bool isWin10;

bool IsCursorOverTaskbar();

DWORD uwpPID;

DWORD windowPID;

bool SetuwpPID();
