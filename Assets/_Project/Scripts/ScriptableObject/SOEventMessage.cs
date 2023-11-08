using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XXX.SO;
using XXX.SO.Event;

namespace WorldForKid.ConnectSocket
{
    [CreateAssetMenu(fileName = SOPathConstant.Name_Event + "message_", menuName = SOPathConstant.Event + "Messge" )]
    public class SOEventMessage : BaseSODataEvent<BaseMessage> { }
}
