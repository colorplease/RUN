using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfInView : MonoBehaviour
{
    [SerializeField]Renderer renderer;
    [SerializeField]CreatureAI creature;
   
   void Update()
   {
       if (renderer.isVisible)
       {
           creature.isViewable = true;
       }
       else
       {
           creature.isViewable = false;
       }
   }
}
