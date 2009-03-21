using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Streamlet.xlFlashPlayer
{
    public class SwfParser
    {
        bool _isCompressed = false;
        int _version = 0;
        int _uncompressedFileLength = 0;
        int _width = 0;
        int _height = 0;
        double _frameRate = 0;
        int _frameCount = 0;

        public bool IsCompressed
        {
            get { return _isCompressed; }
        }

        public int Version
        {
            get { return _version; }
        }

        public int UncompressedFileLength
        {
            get { return _uncompressedFileLength; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public double FrameRate
        {
            get { return _frameRate; }
        }

        public int FrameCount
        {
            get { return _frameCount; }
        }

        public SwfParser() { }

        public SwfParser(string swfFilePath)
        { 
            ParseFile(swfFilePath);
        }

        public bool ParseFile(string swfFilePath)
        {
            _isCompressed = false;
            _version = 0;
            _uncompressedFileLength = 0;
            _width = 0;
            _height = 0;
            _frameRate = 0;
            _frameCount = 0;

            FileStream fs = new FileStream(swfFilePath, FileMode.Open);

            byte[] flag = new byte[3];
            fs.Read(flag, 0, flag.Length);
            if ((flag[0] != 0x46 && flag[0] != 0x43) || flag[1] != 0x57 || flag[2] != 0x53)
            {
                return false;
            }

            _version = fs.ReadByte();

            byte[] len = new byte[4];
            fs.Read(len, 0, len.Length);
            for (int i = 3; i >= 0; i--)
            {
                _uncompressedFileLength *= 256;
                _uncompressedFileLength += len[i];
            }

            if (flag[0] == 0x43)
            {
                _isCompressed = true;
            }
            else
            {
                byte rect0 = (byte)fs.ReadByte();
                int nbits = (rect0 >> 3);
                byte[] rect = new byte[(nbits * 4 + 5) / 8 + ((nbits * 4 + 5) % 8 > 0 ? 1 : 0)];
                rect[0] = rect0;
                fs.Read(rect, 1, rect.Length - 1);
                Bits bits = new Bits();
                foreach (byte b in rect)
                {
                    bits.Append(b);
                }
                _width = (int)bits.UIntValue(5 + nbits * 1, nbits) / 20;
                _height = (int)bits.UIntValue(5 + nbits * 3, nbits) / 20;

                byte[] rate = new byte[2];
                fs.Read(rate, 0, rate.Length);
                _frameRate = rate[1] * 256 + rate[0];

                byte[] count = new byte[2];
                fs.Read(count, 0, count.Length);
                _frameCount = count[1] * 256 + count[0];
            }

            fs.Close();

            return true;
        }
    }
}
