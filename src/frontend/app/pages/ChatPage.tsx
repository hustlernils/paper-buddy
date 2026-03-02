import { useParams } from "react-router-dom";
import { useChats } from "../hooks/useChats";
import { useEffect } from "react";
import { Chat } from "../components/chat/Chat";

export const ChatPage = () =>
{
  const { chatId } = useParams<{chatId: string}>();

  const { activeChat, setActiveChat, chatMessages, isSendingMessage, sendChatMessage} = useChats();
    
  useEffect(() =>
  {
    if(chatId)
    {
      setActiveChat(chatId)
    }  
  })
    
  return (
    <>
      {activeChat && <Chat id={activeChat} messages={chatMessages} isSendingMessage={isSendingMessage} onSubmit={sendChatMessage}></Chat>}          
    </>
  )
}