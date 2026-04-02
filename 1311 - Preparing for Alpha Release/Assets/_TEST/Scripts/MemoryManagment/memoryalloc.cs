using Unity;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

using System;
using System.Linq;
using System.IO;

namespace ASFNAF.LowLevel;

public unsafe class AnimatronicManagment : IDisposable
{
      string errMsgs = "./errors.txt";

      byte* buffer;
      int offset;
      readonly int capacity;

      /// <summary>
      /// Aloca um pedaço de memória com base no tamanho em bytes na criação deste objeto.
      /// Determinando o deslocament inicial e também definindo a sua capacidade.
      /// </summary>
      /// <param name="malloc">TAMANHO DA ALOCAÇÃO EM BYTES</param>
      public AnimatronicManagment(int malloc)
      {
            buffer = (byte*)UnsafeUtility.Malloc(malloc, 8, Allocator.Persistent);
            offset = 0;
            capacity = malloc;
      }

      public T* Alloc<T>(int count = 1) where T : unmanaged
      {
            int size = UnsafeUtility.SizeOf<T>() * count;

            if (offset + size > capacity)
                  throw new Exception(File.ReadLines(errMsgs).ElementAt(0));

            T* pointer = (T*)(buffer + offset);
            offset += size;

            return pointer;
      }

      public void Reset() => offset = 0;

      // DESTRÓI TODA A CLASSE:
      public void Dispose()
      {
            if (buffer != null)
            {
                  UnsafeUtility.Free(buffer, Allocator.Persistent);
                  buffer = null;
            }
      }
}