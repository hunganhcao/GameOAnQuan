using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MessageChatRoom : BaseMessage
{
    public MessageChatRoom(string msg)
    {
        code = Code.CHATROOM;
        message = JsonConvert.SerializeObject(new ModelChatAll() { Message = msg });
    }
    public static ModelChatAll FromJson(BaseMessage msg)
    {
        return JsonConvert.DeserializeObject<ModelChatAll>(msg.message);
    }
}
public class ModelChatAll
{
    public string Message;
}
