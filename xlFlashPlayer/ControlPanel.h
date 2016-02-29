//------------------------------------------------------------------------------
//
//    Copyright (C) Streamlet. All rights reserved.
//
//    File Name:   ControlPanel.h
//    Author:      Streamlet
//    Create Time: 2016-02-28
//    Description: 
//
//------------------------------------------------------------------------------

#ifndef __CONTROLPANEL_H_FC642284_B365_411D_A850_E40506F1CB44_INCLUDED__
#define __CONTROLPANEL_H_FC642284_B365_411D_A850_E40506F1CB44_INCLUDED__


#include <xl/Windows/GUI/xlWindow.h>
#include <xl/Windows/GUI/xlStdStatic.h>
#include <xl/Windows/GUI/xlStdButton.h>
#include <xl/Windows/GUI/xlStdTrackBar.h>

enum
{
    CPNM_OPEN = 1,
    CPNM_PLAY,
    CPNM_PAUSE,
    CPNM_STOP,
    CPNM_GOTO_FRAME,
    CPNM_ABOUT,
};

enum PlayStatus
{
    PLAY_STATUS_STOPPED = 0,
    PLAY_STATUS_PLAYING,
    PLAY_STATUS_PAUSED,
};

struct NMGotoFrame
{
    NMHDR hdr;
    DWORD dwFrame;
};

class ControlPanel : public xl::Windows::Window
{
public:
    ControlPanel();
    ~ControlPanel();

public:
    void SetFrameInfo(int nFrameCount, int nFrameRate);
    void SetCurrentFrame(int nFrame);
    PlayStatus GetPlaytatus();
    void SetPlaytatus(PlayStatus eStatus);

private:
    void Relayout();
    void RefreshProgress();
    void UpdatePlayStatus();

private:
    LRESULT OnCreate(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);
    LRESULT OnEraseBkgnd(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);
    LRESULT OnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);

    LRESULT OnButtonOpen(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled);
    LRESULT OnButtonPlayPause(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled);
    LRESULT OnButtonStop(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled);
    LRESULT OnButtonAbout(HWND hWnd, WORD wID, WORD wCode, HWND hControl, BOOL &bHandled);

    LRESULT OnSliderScroll(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);

private:
    HWND m_hHost;
    HFONT m_hFontWingdings;
    HFONT m_hFontWebdings;

    PlayStatus m_ePlayStatus;
    int m_nFrameCount;
    int m_nFrameRate;

    xl::Windows::StdButton m_btnOpen;
    xl::Windows::StdButton m_btnPlayPause;
    xl::Windows::StdButton m_btnStop;
    xl::Windows::StdButton m_btnAbout;
    xl::Windows::StdStatic m_lblTimePlayed;
    xl::Windows::StdStatic m_lblTimeRemain;
    xl::Windows::StdTrackBar m_tbSlider;
};

#endif // #ifndef __CONTROLPANEL_H_FC642284_B365_411D_A850_E40506F1CB44_INCLUDED__
