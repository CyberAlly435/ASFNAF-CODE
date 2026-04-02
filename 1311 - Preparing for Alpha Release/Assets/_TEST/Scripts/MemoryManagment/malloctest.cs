using Unity;
using Unity.Collections;
using Unity.Collections.LowLevel;

using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ASFNAF.LowLevel;

public unsafe struct EndoStruct
{
      public byte* animatronicName;
}

public unsafe class Endo
{
      /*public EndoStruct* SimulateEndo(AnimatronicManagment managment, string name)
      {
            EndoStruct* endoBase = managment.Alloc<EndoStruct>();
            endoBase.animatronicName = Encoding.UTF8.GetBytes(name);
      }*/
}