using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JMatrix.Editor
{
    [SerializeField]
    public class ResourceVersionInfo
    {
        public ResourceVersionInfo(int length, int hashCode, int compressedLength, int compressedHashCode)
        {
            Length = length;
            HashCode = hashCode;
            CompressedLength = compressedLength;
            CompressedHashCode = compressedHashCode;
        }
        
        public int Length
        {
            get;
            private set;
        }

        public int HashCode
        {
            get;
            private set;
        }

        public int CompressedLength
        {
            get;
            private set;
        }

        public int CompressedHashCode
        {
            get;
            private set;
        }
    }
}
