//------------------------------------------------------------------------------
//
//    Copyright (C) Streamlet. All rights reserved.
//
//    File Name:   Main.cpp
//    Author:      Streamlet
//    Create Time: 2016-01-03
//    Description: 
//
//------------------------------------------------------------------------------


#include <Windows.h>
#include <tchar.h>
#include "FlashPlayer.h"

int APIENTRY _tWinMain(_In_ HINSTANCE     hInstance,
    _In_opt_ HINSTANCE hPrevInstance,
    _In_ LPTSTR        lpCmdLine,
    _In_ int           nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);

    FlashPlayer fp;

    fp.AppendMsgHandler(WM_DESTROY, [](HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled) -> LRESULT
    {
        PostQuitMessage(0);
        return FALSE;
    });

    if (!fp.Create(nullptr, 0, 0, 400, 400, WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SIZEBOX))
    {
        MessageBox(nullptr, L"�����ʼ������\r\n\r\n��������Ҫ Flash Player ActiveX �ؼ�֧�֣��������ϵͳ��û�а�װ�ÿؼ������Ȱ�װ��", L"����", MB_OK | MB_ICONEXCLAMATION);
        return 0;
    }

    fp.CenterWindow();
    fp.UpdateWindow();
    fp.ShowWindow(nCmdShow);

    int argc = 0;
    LPWSTR *argv = CommandLineToArgvW(::GetCommandLineW(), &argc);

    if (argv == nullptr)
    {
        return 0;
    }

    if (argc >= 2)
    {
        fp.Load(argv[1]);
        fp.Play();
    }

    LocalFree(argv);

    MSG msg = {};

    while (GetMessage(&msg, nullptr, 0, 0))
    {
        if (!TranslateAccelerator(msg.hwnd, nullptr, &msg))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    }

    fp.DestroyFlashPlayer();

    return (int)msg.wParam;
}
