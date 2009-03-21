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

        /// <summary>
        /// 该 Flash 文件是否是压缩的。Flash 文件有两种格式，压缩的和非压缩的。
        /// </summary>
        public bool IsCompressed
        {
            get
            {
                return _isCompressed;
            }
        }

        /// <summary>
        /// 该 Flash 文件的版本(创建该文件的 Flash 程序的版本)。
        /// </summary>
        public int Version
        {
            get 
            { 
                return _version; 
            }
        }

        /// <summary>
        /// 该 Flash 文件长度。如果是压缩的 Flash 文件，这个数据表示未压缩时的长度。
        /// </summary>
        public int UncompressedFileLength
        {
            get
            {
                return _uncompressedFileLength;
            }
        }

        /// <summary>
        /// 该 Flash 的原始宽度。如果是压缩的 Flash 文件，这项将始终为 0。
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// 该 Flash 的原始高度。如果是压缩的 Flash 文件，这项将始终为 0。
        /// </summary>
        public int Height
        {
            get 
            {
                return _height; 
            }
        }

        /// <summary>
        /// 该 Flash 的帧速。如果是压缩的 Flash 文件，这项将始终为 0.0。
        /// </summary>
        public double FrameRate
        {
            get
            {
                return _frameRate; 
            }
        }

        /// <summary>
        /// 该 Flash 的总帧数。如果是压缩的 Flash 文件，这项将始终为 0。
        /// </summary>
        public int FrameCount
        {
            get { return _frameCount; }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public SwfParser() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="swfFilePath">要打开的 Flash 文件的路径</param>
        public SwfParser(string swfFilePath)
        { 
            ParseSwfHeader(swfFilePath);
        }

        /// <summary>
        /// 解析 Flash 文件头
        /// </summary>
        /// <param name="swfFilePath">要打开的 Flash 文件的路径</param>
        /// <returns>如果解析成功，返回 true；如果格式不正确，返回 false。
        /// 若打不开文件或其它问题，则由下一层抛出异常(即未对此类异常做任何处理)。</returns>
        public bool ParseSwfHeader(string swfFilePath)
        {
            // 初始化数据成员
            _isCompressed = false;
            _version = 0;
            _uncompressedFileLength = 0;
            _width = 0;
            _height = 0;
            _frameRate = 0;
            _frameCount = 0;

            // 下面开始处理文件
            // 关于 SWF 文件格式，详细的说明请参考官方文档：
            // http://aw.awflasher.com/SWFFormat/flash_fileformat_specification.pdf

            FileStream fs = new FileStream(swfFilePath, FileMode.Open);

            // 处理文件标记
            // FWS(未压缩的) 或 CWS(压缩的)
            byte[] flag = new byte[3];
            fs.Read(flag, 0, flag.Length);
            if ((flag[0] != 0x46 && flag[0] != 0x43) || flag[1] != 0x57 || flag[2] != 0x53)
            {
                // 标记不正确，不是正确的 Flash 文件。
                return false;
            }

            // 处理版本信息
            _version = fs.ReadByte();

            // 处理文件长度信息
            byte[] len = new byte[4];
            fs.Read(len, 0, len.Length);
            for (int i = 3; i >= 0; i--)
            {
                _uncompressedFileLength *= 256;
                _uncompressedFileLength += len[i];
            }

            // 处理后续信息
            if (flag[0] == 0x43)    // 压缩的 Flash，后续信息不做处理
            {
                _isCompressed = true;
            }
            else    // 未压缩的 Flash
            {
                // 处理宽度和高度信息
                byte rect0 = (byte)fs.ReadByte();
                int nbits = (rect0 >> 3);   // 前 5 位的值表示后面的每个宽度高度信息等所占的位数
                // 取出后续的字节
                byte[] rect = new byte[(nbits * 4 + 5) / 8 + ((nbits * 4 + 5) % 8 > 0 ? 1 : 0)];
                rect[0] = rect0;
                fs.Read(rect, 1, rect.Length - 1);
                BitsValue bits = new BitsValue();
                foreach (byte b in rect)
                {
                    bits.Append(b);
                }
                // 第 0 到 4 位是上面的 nbits
                // 第 5 到 5 + nbits - 1 位是 Xmin，总是为 0
                // 第 5 + nbits 到 5 + nbits * 2 - 1 位是 Xmax，即宽度
                _width = (int)bits.UIntValue(5 + nbits * 1, nbits) / 20;
                // 第 5 + nbits * 2 到 5 + nbits * 3 - 1 位是 Ymin，总是为 0
                // 第 5 + nbits * 3 到 5 + nbits * 3 - 1 位是 Ymax，即高度
                _height = (int)bits.UIntValue(5 + nbits * 3, nbits) / 20;

                // 处理帧速信息
                byte[] rate = new byte[2];
                fs.Read(rate, 0, rate.Length);
                _frameRate = ((uint)rate[1] + (uint)rate[0] / 256.0);

                // 处理总帧数信息
                byte[] count = new byte[2];
                fs.Read(count, 0, count.Length);
                _frameCount = count[1] * 256 + count[0];
            }

            fs.Close();

            return true;
        }
    }
}
