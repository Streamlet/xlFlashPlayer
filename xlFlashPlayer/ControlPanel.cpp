//------------------------------------------------------------------------------
//
//    Copyright (C) YourName. All rights reserved.
//
//    File Name:   ControlPanel.cpp
//    Author:      YourName
//    Create Time: 2016-02-28
//    Description: 
//
//------------------------------------------------------------------------------


#include <xl/Windows/GUI/xlDPI.h>
#include "ControlPanel.h"
#include <stdio.h>

#define BUTTON_SIZE     XL_DPI_X(30)

#define CHAR_OPEN       L"1"
#define CHAR_PLAY       L"4"
#define CHAR_PAUSE      L";"
#define CHAR_STOP       L"<"
#define CHAR_ABOUT      L"i"

enum
{
    ID_STATIC   = -1,

    ID_CONTROLS = 100,

    ID_BUTTON_OPEN,
    ID_BUTTON_PLAY_PAUSE,
    ID_BUTTON_STOP,
    ID_BUTTON_ABOUT,

    ID_SLIDER,
};

ControlPanel::ControlPanel() : m_hHost(nullptr), m_hFontWingdings(nullptr), m_hFontWebdings(nullptr), m_ePlayStatus(PLAY_STATUS_STOPPED), m_nFrameRate(0), m_nFrameCount(0)
{
    AppendMsgHandler(WM_CREATE, MsgHandler(this, &ControlPanel::OnCreate));
    AppendMsgHandler(WM_ERASEBKGND, MsgHandler(this, &ControlPanel::OnEraseBkgnd));
    AppendMsgHandler(WM_SIZE, MsgHandler(this, &ControlPanel::OnSize));

    AppendCommandMsgHandler(ID_BUTTON_OPEN, CommandMsgHandler(this, &ControlPanel::OnButtonOpen));
    AppendCommandMsgHandler(ID_BUTTON_PLAY_PAUSE, CommandMsgHandler(this, &ControlPanel::OnButtonPlayPause));
    AppendCommandMsgHandler(ID_BUTTON_STOP, CommandMsgHandler(this, &ControlPanel::OnButtonStop));
    AppendCommandMsgHandler(ID_BUTTON_ABOUT, CommandMsgHandler(this, &ControlPanel::OnButtonAbout));

    AppendMsgHandler(WM_HSCROLL, MsgHandler(this, &ControlPanel::OnSliderScroll));

    m_hFontWingdings = CreateFont(XL_DPI_Y(-18), 0, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, DEFAULT_CHARSET, OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS, CLEARTYPE_QUALITY, DEFAULT_PITCH | FF_DONTCARE, L"Wingdings");
    m_hFontWebdings = CreateFont(XL_DPI_Y(-18), 0, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE, DEFAULT_CHARSET, OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS, CLEARTYPE_QUALITY, DEFAULT_PITCH | FF_DONTCARE, L"Webdings");
}

ControlPanel::~ControlPanel()
{
    DeleteObject(m_hFontWebdings);
    DeleteObject(m_hFontWingdings);
}

void ControlPanel::SetFrameInfo(int nFrameCount, int nFrameRate)
{
    m_nFrameRate = nFrameCount;
    m_nFrameCount = nFrameRate;

    m_tbSlider.SetRange(0, m_nFrameCount);
    m_btnPlayPause.EnableWindow(m_nFrameCount > 0);
    m_btnStop.EnableWindow(m_nFrameCount > 0);
    m_tbSlider.EnableWindow(m_nFrameCount > 0);
}

void ControlPanel::SetCurrentFrame(int nFrame)
{
    m_tbSlider.SetPos(nFrame);
    RefreshProgress();
}

PlayStatus ControlPanel::GetPlaytatus()
{
    return m_ePlayStatus;
}

void ControlPanel::SetPlaytatus(PlayStatus eStatus)
{
    m_ePlayStatus = eStatus;
    UpdatePlayStatus();
}

void ControlPanel::Relayout()
{
    RECT rcControl = {};
    GetClientRect(&rcControl);

    RECT rc = rcControl;
    rc.right = rc.left + BUTTON_SIZE;
    m_btnOpen.MoveWindow(&rc);

    rc.left = rc.right;
    rc.right = rc.left + BUTTON_SIZE;
    m_btnPlayPause.MoveWindow(&rc);

    rc.left = rc.right;
    rc.right = rc.left + BUTTON_SIZE;
    m_btnStop.MoveWindow(&rc);

    rc.top = rcControl.top + (rcControl.bottom - rcControl.top) / 8;
    rc.left = rc.right;
    rc.right = rcControl.right - BUTTON_SIZE - XL_DPI_X(64);
    m_tbSlider.MoveWindow(&rc);

    rc.left = rc.right;
    rc.right = rcControl.right - BUTTON_SIZE;
    rc.top = rcControl.top;
    rc.bottom = rc.top + (rcControl.bottom - rcControl.top) / 2;
    m_lblTimePlayed.MoveWindow(&rc);
    rc.top = rc.bottom;
    rc.bottom = rcControl.bottom;
    m_lblTimeRemain.MoveWindow(&rc);

    rc.top = rcControl.top;
    rc.left = rc.right;
    rc.right = rcControl.right;
    m_btnAbout.MoveWindow(&rc);
}

void ControlPanel::RefreshProgress()
{
    int nPos = m_tbSlider.GetPos();
    int nSecondPlayed = nPos * 256 / m_nFrameRate;
    WCHAR szTimePlayed[20] = {}, szTimeRemain[20] = {};
    swprintf_s(szTimePlayed, L"%02d:%02d:%02d", nSecondPlayed / 3600, nSecondPlayed % 3600 / 60, nSecondPlayed % 60);
    swprintf_s(szTimeRemain, L"%02d:%02d:%02d", m_nFrameCount / 3600, m_nFrameCount % 3600 / 60, m_nFrameCount % 60);
    m_lblTimePlayed.SetWindowText(szTimePlayed);
    m_lblTimeRemain.SetWindowText(szTimeRemain);
}

void ControlPanel::UpdatePlayStatus()
{
    m_btnPlayPause.SetWindowText(m_ePlayStatus == PLAY_STATUS_PLAYING ? CHAR_PAUSE : CHAR_PLAY);

    if (m_ePlayStatus == PLAY_STATUS_STOPPED)
    {
        m_tbSlider.SetPos(0);
    }

    m_btnStop.EnableWindow(m_ePlayStatus != PLAY_STATUS_STOPPED);
}

LRESULT ControlPanel::OnCreate(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    m_hHost = GetParent();

    m_lblTimePlayed.Create(m_hWnd, ID_STATIC, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE | SS_CENTER);
    m_lblTimeRemain.Create(m_hWnd, ID_STATIC, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE | SS_CENTER);
    m_tbSlider.Create(m_hWnd, ID_SLIDER, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE | TBS_NOTICKS | TBS_FIXEDLENGTH);
    m_btnOpen.Create(m_hWnd, ID_BUTTON_OPEN, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE);
    m_btnPlayPause.Create(m_hWnd, ID_BUTTON_PLAY_PAUSE, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE);
    m_btnStop.Create(m_hWnd, ID_BUTTON_STOP, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE);
    m_btnAbout.Create(m_hWnd, ID_BUTTON_ABOUT, 0, 0, 0, 0, WS_CHILD | WS_VISIBLE);

    m_btnOpen.SetFont(m_hFontWingdings);
    m_btnPlayPause.SetFont(m_hFontWebdings);
    m_btnStop.SetFont(m_hFontWebdings);
    m_btnAbout.SetFont(m_hFontWebdings);

    m_btnOpen.SetWindowText(CHAR_OPEN);
    m_btnPlayPause.SetWindowText(CHAR_PLAY);
    m_btnStop.SetWindowText(CHAR_STOP);
    m_btnAbout.SetWindowText(CHAR_ABOUT);

    m_lblTimePlayed.SetWindowText(L"00:00:00");
    m_lblTimeRemain.SetWindowText(L"00:00:00");

    m_btnPlayPause.EnableWindow(FALSE);
    m_btnStop.EnableWindow(FALSE);
    m_tbSlider.EnableWindow(FALSE);

    Relayout();

    return 0;
}

LRESULT ControlPanel::OnEraseBkgnd(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    HDC hDC = (HDC)wParam;

    RECT rect;
    GetClientRect(&rect);

    SetDCBrushColor(hDC, GetSysColor(COLOR_3DFACE));
    HBRUSH hBrush = (HBRUSH)GetStockObject(DC_BRUSH);

    FillRect(hDC, &rect, hBrush);

    return TRUE;
}

LRESULT ControlPanel::OnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    Relayout();
    return 0;
}

LRESULT ControlPanel::OnButtonOpen(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled)
{
    WORD wSelfID = GetDlgCtrlID(m_hWnd);
    NMHDR n = { m_hWnd, wSelfID, CPNM_OPEN };
    ::SendMessage(m_hHost, WM_NOTIFY, wSelfID, (LPARAM)&n);

    return 0;
}

LRESULT ControlPanel::OnButtonPlayPause(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled)
{
    PlayStatus eNewStatus = m_ePlayStatus == PLAY_STATUS_PLAYING ? PLAY_STATUS_PAUSED : PLAY_STATUS_PLAYING;

    if (eNewStatus == m_ePlayStatus)
    {
        return 0;
    }

    WORD wSelfID = GetDlgCtrlID(m_hWnd);
    NMHDR n = { m_hWnd, wSelfID, (UINT)(eNewStatus == PLAY_STATUS_PAUSED ? CPNM_PAUSE : CPNM_PLAY) };
    ::SendMessage(m_hHost, WM_NOTIFY, wSelfID, (LPARAM)&n);

    m_ePlayStatus = eNewStatus;
    UpdatePlayStatus();

    return 0;
}

LRESULT ControlPanel::OnButtonStop(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled)
{
    PlayStatus eNewStatus = m_ePlayStatus == PLAY_STATUS_PLAYING ? PLAY_STATUS_PAUSED : PLAY_STATUS_PLAYING;

    if (m_ePlayStatus == PLAY_STATUS_STOPPED)
    {
        return 0;
    }

    WORD wSelfID = GetDlgCtrlID(m_hWnd);
    NMHDR n = { m_hWnd, wSelfID, CPNM_STOP };
    ::SendMessage(m_hHost, WM_NOTIFY, wSelfID, (LPARAM)&n);

    m_ePlayStatus = PLAY_STATUS_STOPPED;
    UpdatePlayStatus();

    return 0;
}

LRESULT ControlPanel::OnButtonAbout(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled)
{
    WORD wSelfID = GetDlgCtrlID(m_hWnd);
    NMHDR n = { m_hWnd, wSelfID, CPNM_ABOUT };
    ::SendMessage(m_hHost, WM_NOTIFY, wSelfID, (LPARAM)&n);

    return 0;
}

LRESULT ControlPanel::OnSliderScroll(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled)
{
    RefreshProgress();

    int nPos = m_tbSlider.GetPos();
        WORD wSelfID = GetDlgCtrlID(m_hWnd);
    NMGotoFrame n = { { m_hWnd, wSelfID, CPNM_GOTO_FRAME }, (DWORD)nPos };
    ::SendMessage(m_hHost, WM_NOTIFY, wSelfID, (LPARAM)&n);

    return 0;
}
