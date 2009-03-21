using System;
using System.Collections.Generic;
using System.Text;

namespace Streamlet.xlFlashPlayer
{
    public class Bits
    {
        List<uint> bits = new List<uint>();
        int _len;

        public int Length
        {
            get { return _len; }
        }

        public Bits()
        {
            _len = 0;
            bits.Add(0u);
        }

        private int EmptyBits
        {
            get { return bits.Count * 32 - _len; }
        }

        public void Append(bool bitData)
        {
            if (EmptyBits == 0u)
            {
                bits.Add(0);
            }
            
            if (bitData)
            {
                int index = _len / 32;
                int offset = _len % 32;
                bits[index] |= (0x80000000 >> offset);
            }

            _len++;
        }

        public void Append(byte byteData)
        {
            if (EmptyBits == 0)
            {
                bits.Add(0u);
            }            

            uint data = byteData;

            int index = _len / 32;
            int offset = _len % 32;

            if (offset <= 24)
            {
                bits[index] |= (data << (24 - offset));
            }
            else
            {
                bits[index] |= (data >> (offset - 24));
                bits.Add(0u);
                bits[index + 1] |= (data << (32 - (offset - 24)));
            }

            _len += 8;
        }

        public void Append(uint uintData)
        {
            if (EmptyBits == 0)
            {
                bits.Add(uintData);
            }
            else
            {
                int index = _len / 32;
                int offset = _len % 32;
                bits[index] |= (uintData >> offset);
                bits.Add(0u);
                bits[index + 1] |= (uintData << (32 - offset));
            }
            _len += 32;
        }

        public bool BitValue(int bitPos)
        {
            if (bitPos < 0 || bitPos >= _len)
            {
                throw new IndexOutOfRangeException();
            }

            int index = bitPos / 32;
            int offset = bitPos % 32;

            if ((bits[index] & (0x80000000 >> offset)) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

            ret |= (bits[index] << offset);
            if (offset + len > 32)
            {
                ret |= (bits[index + 1] >> (32 - offset));
            }

            ret >>= 32 - len;

            return ret;
        }
    }
}
