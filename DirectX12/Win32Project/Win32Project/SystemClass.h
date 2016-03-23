#pragma once

#pragma region PRE-PROCESSING DIRECTIVES
//We do this to speed up the build process, it reduces the size of the Win32 header files by excluding some of the less used APIs.
#define WIN32_LEAN_AND_MEAN
#pragma endregion

#pragma region INCLUDES
#include <windows.h>

#include"InputClass.h"
#include"GraphicsClass.h"
#pragma endregion


class SystemClass
{
public:
	SystemClass();
	SystemClass(const SystemClass&);
	~SystemClass();

	bool Initialize();
	void Shutdown();
	void Run();

	LRESULT CALLBACK MessageHandler(HWND, UINT, WPARAM, LPARAM);

private:
	bool Frame();
	void InitializeWindows(int&, int&);
	void ShutdownWindows();

	LPCWSTR m_applicationName;
	HINSTANCE m_hinstance;
	HWND m_hwnd;

	InputClass* m_Input;
	GraphicsClass* m_Graphics;
};

#pragma region FUNCTION PROTOTYPES
static LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
#pragma endregion

#pragma region GLOBALS
static SystemClass* ApplicationHandle = 0;
#pragma endregion

