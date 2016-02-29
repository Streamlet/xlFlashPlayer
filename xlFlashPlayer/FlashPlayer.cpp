//------------------------------------------------------------------------------
//
//    Copyright (C) Streamlet. All rights reserved.
//
//    File Name:   FlashPlayer.cpp
//    Author:      Streamlet
//    Create Time: 2016-02-24
//    Description: 
//
//------------------------------------------------------------------------------


#include "FlashPlayer.h"
#include "SwfParser.h"
#include "resource.h"

#define CONTROL_PANEL_HEIGHT 25

enum
{
    ID_STATIC = -1,

    ID_CONTROLS = 100,

    ID_CONTROL_PANEL,
};

FlashPlayer::FlashPlayer()
{
    AppendMsgHandler(WM_CREATE, MsgHandler(this, &FlashPlayer::OnCreate));
    AppendMsgHandler(WM_ERASEBKGND, MsgHandler(this, &FlashPlayer::OnSize));
    AppendMsgHandler(WM_SIZE, MsgHandler(this, &FlashPlayer::OnSize));

    AppendNotifyMsgHandler(ID_CONTROL_PANEL, CPNM_OPEN, NotifyMsgHandler(this, &FlashPlayer::OnOpen));
    AppendNotifyMsgHandler(ID_CONTROL_PANEL, CPNM_PLAY, NotifyMsgHandler(this, &FlashPlayer::OnPlay));
    AppendNotifyMsgHandler(ID_CONTROL_PANEL, CPNM_PAUSE, NotifyMsgHandler(this, &FlashPlayer::OnPause));
    AppendNotifyMsgHandler(ID_CONTROL_PANEL, CPNM_STOP, NotifyMsgHandler(this, &FlashPlayer::OnStop));
    AppendNotifyMsgHandler(ID_CONTROL_PANEL, CPNM_GOTO_FRAME, NotifyMsgHandler(this, &FlashPlayer::OnGotoFrame));
    AppendNotifyMsgHandler(ID_CONTROL_PANEL, CPNM_ABOUT, NotifyMsgHandler(this, &FlashPlayer::OnAbout));
}

FlashPlayer::~FlashPlayer()
{

}

bool FlashPlayer::Create(HWND hParent, int x, int y, int nWidth, int nHeight, DWORD dwStyle)
{
    if (!xl::Windows::Window::Create(hParent, x, y, nWidth, nHeight, dwStyle, 0, L"xlFlashPlayer", L"溪流 Flash 播放器 v2.0", nullptr))
    {
        return false;
    }

    if (!CreateFlashPlayer(m_hWnd))
    {
        return false;
    }

    return true;
}

void FlashPlayer::Load(LPCTSTR lpFile, bool bCenterWindow)
{
    SwfInfo swf = {};

    if (!ParseSwfFile(lpFile, swf))
    {
        MessageBox(L"您所选的文件不是一个有效的 Flash 文件。", L"错误", MB_OK | MB_ICONEXCLAMATION);
        return;
    }

    if (swf.Signature[0] == L'C')
    {
        MessageBox(L"这是个压缩的 Flash 文件，暂时无法获取宽度和高度。请自行调整窗口大小以便观看。", L"提示", MB_OK | MB_ICONINFORMATION);
    }

    HRESULT hr = m_pFlashPlayer->put_Movie(::SysAllocString(lpFile));

    if (FAILED(hr))
    {
        MessageBox(L"播放文件失败。", L"错误", MB_OK | MB_ICONEXCLAMATION);
        return;
    }

    m_szFlash = xl::Size(swf.Width, swf.Height);

    m_ControlPanel.SetFrameInfo(swf.FrameRate, swf.FrameCount);
    ResizeForFlash();

    if (bCenterWindow)
    {
        CenterWindow();
    }

    m_strFile = lpFile;
    SetWindowText(lpFile);
}

void FlashPlayer::Unload()
{
    m_ControlPanel.SetFrameInfo(0, 0);
    m_pFlashPlayer->put_Movie(nullptr);
    m_strFile.Clear();
    m_szFlash = xl::Size(0, 0);
}

void FlashPlayer::Play()
{
    m_ControlPanel.SetPlaytatus(PLAY_STATUS_PLAYING);
    m_Timer.Set(1000, xl::Windows::TimerCallback(this, &FlashPlayer::OnTimer));
    m_pFlashPlayer->raw_Play();
}

void FlashPlayer::Pause()
{
    m_ControlPanel.SetPlaytatus(PLAY_STATUS_PAUSED);
    m_pFlashPlayer->raw_StopPlay();
    m_Timer.Kill();
}

void FlashPlayer::Stop()
{
    m_ControlPanel.SetPlaytatus(PLAY_STATUS_STOPPED);
    m_pFlashPlayer->raw_StopPlay();
    m_pFlashPlayer->raw_GotoFrame(0);
    m_Timer.Kill();
}

void FlashPlayer::ResizeForFlash()
{
    RECT rcWindow = {};
    GetWindowRect(&rcWindow);
    RECT rcClient = {};
    GetClientRect(&rcClient);
    rcWindow.right = rcWindow.left + m_szFlash.Width() + (rcWindow.right - rcWindow.left) - (rcClient.right - rcClient.left);
    rcWindow.bottom = rcWindow.top + m_szFlash.Height() + CONTROL_PANEL_HEIGHT + (rcWindow.bottom - rcWindow.top) - (rcClient.bottom - rcClient.top);
    MoveWindow(&rcWindow);
}

void FlashPlayer::Relayout()
{
    RECT rcClient = {};
    GetClientRect(&rcClient);

    RECT rc = rcClient;
    rc.bottom -= CONTROL_PANEL_HEIGHT;
    InPlaceActive(m_hWnd, &rc);

    rc = rcClient;
    rc.top = rc.bottom - CONTROL_PANEL_HEIGHT;
    m_ControlPanel.MoveWindow(&rc);
}

LRESULT FlashPlayer::OnCreate(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    HICON hIcon = LoadIcon(GetModuleHandle(nullptr), MAKEINTRESOURCE(IDI_ICON_APP));
    SetIcon(hIcon);
    SetIcon(hIcon, FALSE);

    m_ControlPanel.Create(m_hWnd, 0, 0, 0, 0, WS_VISIBLE | WS_CHILD, 0, L"xlFlashController", nullptr, (HMENU)ID_CONTROL_PANEL);
    Relayout();
    return 0;
}

LRESULT FlashPlayer::OnEraseBkgnd(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    return TRUE;
}

LRESULT FlashPlayer::OnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    Relayout();
    return 0;
}

void FlashPlayer::OnTimer(DWORD dwTime)
{
    long lCurrentFrame = 0;
    m_pFlashPlayer->raw_CurrentFrame(&lCurrentFrame);

    m_ControlPanel.SetCurrentFrame(lCurrentFrame);
}

LRESULT FlashPlayer::OnOpen(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled)
{
    WCHAR szPath[MAX_PATH] = {};
    OPENFILENAME ofn =
    {
        sizeof(ofn),
        m_hWnd,
        nullptr,
        L"Shockwave Flash Object(*.swf)\0*.swf\0All Files(*.*)\0*.*\0",
        nullptr,
        0,
        0,
        szPath,
        MAX_PATH,
        nullptr,
        0,
        nullptr,
        nullptr,
        OFN_ENABLESIZING | OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_NOCHANGEDIR ,
        0,
        0,
        L"swf",
        0,
        nullptr,
        nullptr,
        nullptr,
        0,
        0,
    };

    if (!GetOpenFileName(&ofn))
    {
        return 0;
    }

    Stop();
    Unload();
    Load(szPath);
    Play();

    return 0;
}

LRESULT FlashPlayer::OnPlay(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled)
{
    m_pFlashPlayer->raw_Play();
    return 0;
}

LRESULT FlashPlayer::OnPause(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled)
{
    m_pFlashPlayer->raw_Stop();
    return 0;
}

LRESULT FlashPlayer::OnStop(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled)
{
    m_pFlashPlayer->raw_Stop();
    m_pFlashPlayer->raw_GotoFrame(0);
    return 0;
}

LRESULT FlashPlayer::OnGotoFrame(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled)
{
    NMGotoFrame *p = (NMGotoFrame *)lpNMHDR;

    bool bPlaying = m_ControlPanel.GetPlaytatus() == PLAY_STATUS_PLAYING;

    if (bPlaying)
    {
        m_pFlashPlayer->raw_Stop();
    }

    m_pFlashPlayer->raw_GotoFrame(p->dwFrame);

    if (bPlaying)
    {
        m_pFlashPlayer->raw_Play();
    }

    return 0;
}

LRESULT FlashPlayer::OnAbout(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled)
{
    MessageBox(L"溪流 Flash 播放器 v2.0\r\n"
               L"\r\n"
               L"\r\n"
               L"有任何建议意见欢迎来邮相告^_^\r\n"
               L"\r\n"
               L"http://www.streamlet.org/\r\n"
               L"streamlet@outlook.com\r\n",
        L"关于溪流 Flash 播放器",
        MB_OK | MB_ICONINFORMATION);
    return 0;
}
