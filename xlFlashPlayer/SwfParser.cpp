//------------------------------------------------------------------------------
//
//    Copyright (C) Streamlet. All rights reserved.
//
//    File Name:   SwfParser.cpp
//    Author:      Streamlet
//    Create Time: 2016-02-26
//    Description: 
//
//------------------------------------------------------------------------------


#include <xl/Common/Meta/xlScopeExit.h>
#include <xl/Common/Math/xlBitValue.h>
#include <xl/Common/Memory/xlSmartPtr.h>
#include <Windows.h>
#include "SwfParser.h"


bool ParseSwfFile(const wchar_t *lpszFileName, SwfInfo &swf)
{
    HANDLE hFile = CreateFile(lpszFileName, GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);

    if (hFile == INVALID_HANDLE_VALUE)
    {
        return false;
    }

    XL_ON_BLOCK_EXIT(CloseHandle, hFile);

    DWORD dwRead = 0;

    if (!ReadFile(hFile, &swf, sizeof(SwfFixedHeader), &dwRead, nullptr) || dwRead != sizeof(SwfFixedHeader))
    {
        return false;
    }

    BYTE byNBits = 0;

    if (!ReadFile(hFile, &byNBits, sizeof(BYTE), &dwRead, nullptr) || dwRead != sizeof(BYTE))
    {
        return false;
    }

    int nBits = xl::BitValue::Eval(byNBits, 0, 5);
    int nTotalBytesForRect = (5 + nBits * 4 - 1) / 8 +1;
    xl::SharedArray<BYTE> pRect = new BYTE[nTotalBytesForRect];
    pRect[0] = byNBits;

    if (!ReadFile(hFile, &pRect[1], nTotalBytesForRect - 1, &dwRead, nullptr) || dwRead != nTotalBytesForRect - 1)
    {
        return false;
    }

    swf.Width = xl::BitValue::Eval<unsigned int, BYTE>(pRect.RawPointer(), 5 + nBits * 1, nBits) / 20;
    swf.Height = xl::BitValue::Eval<unsigned int, BYTE>(pRect.RawPointer(), 5 + nBits * 3, nBits) / 20;

    if (!ReadFile(hFile, &swf.FrameRate, sizeof(unsigned short) * 2, &dwRead, nullptr) || dwRead != sizeof(unsigned short) * 2)
    {
        return false;
    }

    return true;
}
