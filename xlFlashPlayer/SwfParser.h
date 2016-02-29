//------------------------------------------------------------------------------
//
//    Copyright (C) Streamlet. All rights reserved.
//
//    File Name:   SwfParser.h
//    Author:      Streamlet
//    Create Time: 2016-02-26
//    Description: 
//
//------------------------------------------------------------------------------

#ifndef __SWFPARSER_H_75C7BF1F_C5DF_4844_B42D_453A2C7A729C_INCLUDED__
#define __SWFPARSER_H_75C7BF1F_C5DF_4844_B42D_453A2C7A729C_INCLUDED__


struct SwfFixedHeader
{
    char Signature[3];          // UI8 Signature byte :
                                // [0]: 'F' indicates uncompressed, 'C' indicates compressed(SWF 6 and later only)
                                // [1]: always 'W'
                                // [2]: always 'S'
    unsigned char Version;      // Single byte file version(for example, 0x06 for SWF 6)
    unsigned int FileLength;    // Length of entire file in bytes
};

struct SwfInfo : public SwfFixedHeader
{
    unsigned int Width;         // Frame width in pixel
    unsigned int Height;        // Frame height in pixel
    unsigned short FrameRate;   // Frame delay in 8.8 fixed number of frames per second
    unsigned short FrameCount;  // Total number of frames in file
};

bool ParseSwfFile(const wchar_t *lpszFileName, SwfInfo &swf);

#endif // #ifndef __SWFPARSER_H_75C7BF1F_C5DF_4844_B42D_453A2C7A729C_INCLUDED__
