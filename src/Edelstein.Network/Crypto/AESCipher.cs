using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Edelstein.Network.Crypto
{
    public class AESCipher
    {
        public const short Version = 95;
        private static readonly byte[] UserKey = {0x13, 0x08, 0x06, 0xb4, 0x1b, 0x0f, 0x33, 0x52};

        private static readonly SymmetricAlgorithm Cipher = new AesManaged
        {
            KeySize = 256,
            Key = ExpandKey(UserKey),
            Mode = CipherMode.ECB
        };

        private static byte[] ExpandKey(IReadOnlyList<byte> key)
        {
            var res = new byte[key.Count * 4];
            for (var i = 0; i < key.Count; i++)
                res[i * 4] = key[i];
            return res;
        }

        public static byte[] Transform(IEnumerable<byte> input, uint pSrc)
        {
            var buffer = input.ToArray();
            var remaining = buffer.Length;
            var length = 0x5B0;
            var start = 0;

            var srcExp = new byte[sizeof(int) * 4];
            var srcBytes = BitConverter.GetBytes(pSrc);

            while (remaining > 0)
            {
                for (var i = 0; i < srcExp.Length; ++i)
                    srcExp[i] = srcBytes[i % 4];

                if (remaining < length)
                    length = remaining;

                for (var i = start; i < start + length; ++i)
                {
                    var sub = i - start;

                    if (sub % srcExp.Length == 0)
                    {
                        using (var encryptor = Cipher.CreateEncryptor())
                        {
                            var result = encryptor.TransformFinalBlock(srcExp, 0, srcExp.Length);
                            Array.Copy(result, srcExp, srcExp.Length);
                        }
                    }

                    buffer[i] ^= srcExp[sub % srcExp.Length];
                }

                start += length;
                remaining -= length;
                length = 0x5B4;
            }

            return buffer;
        }
    }
}