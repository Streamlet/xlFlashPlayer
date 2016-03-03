//------------------------------------------------------------------------------
//
//    Copyright (C) Streamlet. All rights reserved.
//
//    File Name:   FlashPlayer.h
//    Author:      Streamlet
//    Create Time: 2016-02-24
//    Description: 
//
//------------------------------------------------------------------------------

#ifndef __FLASHPLAYER_H_AA692A74_C4FC_4AB9_9124_8A4E821CA8BE_INCLUDED__
#define __FLASHPLAYER_H_AA692A74_C4FC_4AB9_9124_8A4E821CA8BE_INCLUDED__


#include <xl/Windows/COM/Objects/xlFlashPlayer.h>
#include <xl/Windows/GUI/xlDialog.h>
#include <xl/Common/Math/xlMathBase.h>
#include <xl/Common/String/xlString.h>
#include <xl/Windows/Timer/xlTimer.h>
#include "ControlPanel.h"

class FlashPlayer : public xl::Windows::FlashPlayer, public xl::Windows::Window
{
public:
    FlashPlayer();
    ~FlashPlayer();

public:
    bool Create(HWND hParent, int x, int y, int nWidth, int nHeight, DWORD dwStyle);
    void Load(LPCTSTR lpFile, bool bCenterWindow = true);
    void Unload();
    void Play();
    void Pause();
    void Stop();

private:
    void ResizeForFlash();
    void Relayout();

private:
    LRESULT OnCreate(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);
    LRESULT OnEraseBkgnd(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);
    LRESULT OnSize(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL &bHandled);
    void OnTimer(DWORD dwTime);

    LRESULT OnOpen(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled);
    LRESULT OnPlay(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled);
    LRESULT OnPause(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled);
    LRESULT OnStop(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled);
    LRESULT OnGotoFrame(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled);
    LRESULT OnAbout(HWND hWnd, LPNMHDR lpNMHDR, BOOL &bHandled);

private:
    ControlPanel m_ControlPanel;
    xl::Windows::Timer m_Timer;
    xl::String m_strFile;
    xl::Size m_szFlash;
};


#endif // #ifndef __FLASHPLAYER_H_AA692A74_C4FC_4AB9_9124_8A4E821CA8BE_INCLUDED__
