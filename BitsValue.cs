using System;
using System.Collections.Generic;
using System.Text;

namespace Streamlet.xlFlashPlayer
{
    /// <summary>
    /// 处理“二进制位构成的数”的类。
    /// 即，撇开“字节”等概念，直接处理二进制位，
    /// 可以往数里加入一个位，加入一个字节，加入一个双字，
    /// 之后可以读出其中任一位，或者取出其中某几位。
    /// </summary>
    public class BitsValue
    {
        /// <summary>
        /// 存储数据。以 uint 为载体。
        /// 如果当前的总位数在 32 位之内，里面将只有 1 个 uint；
        /// 如果总位数大于 32 位，又在 64 位之内，里面将有 2 个 uint；
        /// 如此，按需加入一个个 uint。
        /// </summary>
        List<uint> _bits = new List<uint>();

        /// <summary>
        /// 标记数中当前的总位数
        /// </summary>
        int _len;

        /// <summary>
        /// 总的有效二进制位位数
        /// </summary>
        public int Length
        {
            get
            { 
                return _len;
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public BitsValue()
        {
            _len = 0;
        }

        /// <summary>
        /// 空闲的位的数目
        /// </summary>
        private int EmptyBits
        {
            get { return _bits.Count * 32 - _len; }
        }

        /// <summary>
        /// 向数中加入 1 个位
        /// </summary>
        /// <param name="bitData">要加入的位。如果该位为 1，请置为 true，否则为 false。</param>
        public void Append(bool bitData)
        {
            if (EmptyBits == 0u)
            {
                _bits.Add(0);
            }
            
            if (bitData)
            {
                int index = _len / 32;
                int offset = _len % 32;
                _bits[index] |= (0x80000000 >> offset);
            }

            _len++;
        }

        /// <summary>
        /// 向数中加入 8 个位(1 字节)
        /// </summary>
        /// <param name="byteData">要加入的字节</param>
        public void Append(byte byteData)
        {
            if (EmptyBits == 0)
            {
                _bits.Add(0u);
            }            

            uint data = byteData;

            int index = _len / 32;
            int offset = _len % 32;

            if (offset <= 24)
            {
                _bits[index] |= (data << (24 - offset));
            }
            else
            {
                _bits[index] |= (data >> (offset - 24));
                _bits.Add(0u);
                _bits[index + 1] |= (data << (32 - (offset - 24)));
            }

            _len += 8;
        }

        /// <summary>
        /// 向数中加入 32 个位(1 双字)
        /// </summary>
        /// <param name="uintData">要加入的双字节数据</param>
        public void Append(uint uintData)
        {
            if (EmptyBits == 0)
            {
                _bits.Add(uintData);
            }
            else
            {
                int index = _len / 32;
                int offset = _len % 32;
                _bits[index] |= (uintData >> offset);
                _bits.Add(0u);
                _bits[index + 1] |= (uintData << (32 - offset));
            }
            _len += 32;
        }

        /// <summary>
        /// 取出数中某个位的值
        /// </summary>
        /// <param name="bitPos">要取的位的位置。从左向右数，最左边为第 0 位。</param>
        /// <returns>如果目标位是 1，返回 true，否则返回 false。</returns>
        public bool BitValue(int bitPos)
        {
            if (bitPos < 0 || bitPos >= _len)
            {
                throw new IndexOutOfRangeException();
            }

            int index = bitPos / 32;
            int offset = bitPos % 32;

            if ((_bits[index] & (0x80000000 >> offset)) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取出数中某几个连续位
        /// </summary>
        /// <param name="start">开始位置。从左向右数，最左边为第 0 位。</param>
        /// <param name="len">要取出的位数</param>
        /// <returns>返回一个 uint，指定的几个位在它的右边，左边多出的位将被填 0.</returns>
        public uint UIntValue(int start, int len)
        {
            if (start < 0 || start + len >= this._len)
            {
                throw new IndexOutOfRangeException();
            }

            if (len <= 0 || len > 32)
            {
                throw new Exception();
            }

            uint ret = 0u;

            int index = start / 32;
            int offset = start % 32;

            ret |= (_bits[index] << offset);
            if (offset + len > 32)
            {
                ret |= (_bits[index + 1] >> (32 - offset));
            }

            ret >>= 32 - len;

            return ret;
        }
    }
}
